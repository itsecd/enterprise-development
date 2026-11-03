using VetClinic.Application.Services;
using VetClinic.Contracts.Dto.Pets;
using VetClinic.Domain.Seeds;
using VetClinic.Infrastructure.InMemory.Repositories;
using VetClinic.Shared;

namespace VetClinic.Tests;

/// <summary>
/// Тесты CRUD-сервисов: связи между сущностями и каскадное удаление
/// </summary>
public class CrudServiceTests
{
    private readonly VetClinicSeed _seed = new();
    private readonly OwnerService _owners;
    private readonly PetService _pets;
    private readonly AppointmentService _appointments;
    private readonly BreedService _breeds;
    private readonly VetService _vets;

    /// <summary>
    /// Для каждого теста создаются свежие данные, потому что тесты их изменяют
    /// </summary>
    public CrudServiceTests()
    {
        var ownerRepo = new OwnerRepository(_seed);
        var petRepo = new PetRepository(_seed);
        var vetRepo = new VetRepository(_seed);
        var appointmentRepo = new AppointmentRepository(_seed);
        var breedRepo = new BreedRepository(_seed);
        var specializationRepo = new SpecializationRepository(_seed);

        _owners = new OwnerService(ownerRepo, petRepo, appointmentRepo);
        _pets = new PetService(petRepo, ownerRepo, breedRepo, appointmentRepo);
        _appointments = new AppointmentService(appointmentRepo, petRepo, vetRepo);
        _breeds = new BreedService(breedRepo, petRepo);
        _vets = new VetService(vetRepo, specializationRepo, appointmentRepo);
    }

    private static PetEditDto NewPet(int ownerId, int? breedId = 1) => new()
    {
        Nickname = "Тест",
        Species = AnimalSpecies.Dog,
        BreedId = breedId,
        BirthDate = new DateTime(2020, 1, 1),
        Weight = 10,
        OwnerId = ownerId
    };

    /// <summary>
    /// Созданный питомец получает новый идентификатор и попадает в список питомцев владельца
    /// </summary>
    [Fact]
    public async Task CreatePet_AddsPetToOwner()
    {
        var created = await _pets.CreateAsync(NewPet(ownerId: 4));

        Assert.Equal(12, created.Id);
        Assert.Equal(4, created.OwnerId);
        Assert.Contains(_seed.Owners[3].Pets, p => p.Id == created.Id);
    }

    /// <summary>
    /// При смене владельца питомец переходит из одного списка в другой
    /// </summary>
    [Fact]
    public async Task UpdatePet_MovesPetBetweenOwners()
    {
        var created = await _pets.CreateAsync(NewPet(ownerId: 4));

        await _pets.UpdateAsync(created.Id, NewPet(ownerId: 5));

        Assert.DoesNotContain(_seed.Owners[3].Pets, p => p.Id == created.Id);
        Assert.Contains(_seed.Owners[4].Pets, p => p.Id == created.Id);
    }

    /// <summary>
    /// Изменение несуществующей записи возвращает null, контроллер превращает его в 404
    /// </summary>
    [Fact]
    public async Task UpdatePet_NotFound_ReturnsNull()
    {
        var result = await _pets.UpdateAsync(999, NewPet(ownerId: 1));

        Assert.Null(result);
    }

    /// <summary>
    /// Ссылка на несуществующего владельца приводит к KeyNotFoundException
    /// </summary>
    [Fact]
    public async Task CreatePet_UnknownOwner_Throws()
    {
        await Assert.ThrowsAsync<KeyNotFoundException>(() => _pets.CreateAsync(NewPet(ownerId: 999)));
    }

    /// <summary>
    /// Удаление владельца удаляет его питомцев и их записи на прием
    /// </summary>
    [Fact]
    public async Task DeleteOwner_CascadesToPetsAndAppointments()
    {
        await _owners.DeleteAsync(1);

        Assert.Null(await _owners.GetByIdAsync(1));
        Assert.Equal(9, (await _pets.GetAllAsync()).Count);
        Assert.Equal(13, (await _appointments.GetAllAsync()).Count);
    }

    /// <summary>
    /// Удаление врача удаляет его записи на прием
    /// </summary>
    [Fact]
    public async Task DeleteVet_CascadesToAppointments()
    {
        await _vets.DeleteAsync(6);

        Assert.DoesNotContain(await _appointments.GetAllAsync(), a => a.VetId == 6);
    }

    /// <summary>
    /// Удаление породы не удаляет питомцев, а сбрасывает у них породу
    /// </summary>
    [Fact]
    public async Task DeleteBreed_SetsPetBreedToNull()
    {
        var petsBefore = (await _pets.GetAllAsync()).Count;

        await _breeds.DeleteAsync(1);

        var pets = await _pets.GetAllAsync();
        Assert.Equal(petsBefore, pets.Count);
        Assert.DoesNotContain(pets, p => p.BreedId == 1);
    }

    /// <summary>
    /// Повторное удаление не приводит к ошибке
    /// </summary>
    [Fact]
    public async Task DeletePet_Twice_DoesNotThrow()
    {
        var created = await _pets.CreateAsync(NewPet(ownerId: 4));

        await _pets.DeleteAsync(created.Id);
        await _pets.DeleteAsync(created.Id);

        Assert.Null(await _pets.GetByIdAsync(created.Id));
    }
}
