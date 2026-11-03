using VetClinic.Application.Services;
using VetClinic.Contracts.Dto.Pets;
using VetClinic.Domain.Seeds;
using VetClinic.Infrastructure.InMemory.Repositories;
using VetClinic.Shared;
using VetClinic.Tests.Fixtures;

namespace VetClinic.Tests;

/// <summary>
/// Тесты CRUD-сервисов: связи между сущностями и каскадное удаление
/// </summary>
public class CrudServiceTests(CrudServicesFixture fixture) : IClassFixture<CrudServicesFixture>
{
    /// <summary>
    /// Для каждого теста создаются свежие данные, потому что тесты их изменяют
    /// </summary>
    private CrudServices NewServices() => fixture.Create();

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
        var s = NewServices();

        var created = await s.Pets.CreateAsync(NewPet(ownerId: 4));

        Assert.Equal(12, created.Id);
        Assert.Equal(4, created.OwnerId);
        Assert.Contains(s.Seed.Owners[3].Pets, p => p.Id == created.Id);
    }

    /// <summary>
    /// При смене владельца питомец переходит из одного списка в другой
    /// </summary>
    [Fact]
    public async Task UpdatePet_MovesPetBetweenOwners()
    {
        var s = NewServices();
        var created = await s.Pets.CreateAsync(NewPet(ownerId: 4));

        await s.Pets.UpdateAsync(created.Id, NewPet(ownerId: 5));

        Assert.DoesNotContain(s.Seed.Owners[3].Pets, p => p.Id == created.Id);
        Assert.Contains(s.Seed.Owners[4].Pets, p => p.Id == created.Id);
    }

    /// <summary>
    /// Изменение несуществующей записи возвращает null, контроллер превращает его в 404
    /// </summary>
    [Fact]
    public async Task UpdatePet_NotFound_ReturnsNull()
    {
        var result = await NewServices().Pets.UpdateAsync(999, NewPet(ownerId: 1));

        Assert.Null(result);
    }

    /// <summary>
    /// Ссылка на несуществующего владельца приводит к KeyNotFoundException
    /// </summary>
    [Fact]
    public async Task CreatePet_UnknownOwner_Throws()
    {
        var s = NewServices();

        await Assert.ThrowsAsync<KeyNotFoundException>(() => s.Pets.CreateAsync(NewPet(ownerId: 999)));
    }

    /// <summary>
    /// Удаление владельца удаляет его питомцев и их записи на прием
    /// </summary>
    [Fact]
    public async Task DeleteOwner_CascadesToPetsAndAppointments()
    {
        var s = NewServices();

        await s.Owners.DeleteAsync(1);

        Assert.Null(await s.Owners.GetByIdAsync(1));
        Assert.Equal(9, (await s.Pets.GetAllAsync()).Count);
        Assert.Equal(13, (await s.Appointments.GetAllAsync()).Count);
    }

    /// <summary>
    /// Удаление врача удаляет его записи на прием
    /// </summary>
    [Fact]
    public async Task DeleteVet_CascadesToAppointments()
    {
        var s = NewServices();

        await s.Vets.DeleteAsync(6);

        Assert.DoesNotContain(await s.Appointments.GetAllAsync(), a => a.VetId == 6);
    }

    /// <summary>
    /// Удаление породы не удаляет питомцев, а сбрасывает у них породу
    /// </summary>
    [Fact]
    public async Task DeleteBreed_SetsPetBreedToNull()
    {
        var s = NewServices();
        var petsBefore = (await s.Pets.GetAllAsync()).Count;

        await s.Breeds.DeleteAsync(1);

        var pets = await s.Pets.GetAllAsync();
        Assert.Equal(petsBefore, pets.Count);
        Assert.DoesNotContain(pets, p => p.BreedId == 1);
    }

    /// <summary>
    /// Повторное удаление не приводит к ошибке
    /// </summary>
    [Fact]
    public async Task DeletePet_Twice_DoesNotThrow()
    {
        var s = NewServices();
        var created = await s.Pets.CreateAsync(NewPet(ownerId: 4));

        await s.Pets.DeleteAsync(created.Id);
        await s.Pets.DeleteAsync(created.Id);

        Assert.Null(await s.Pets.GetByIdAsync(created.Id));
    }
}
