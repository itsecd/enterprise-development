using VetClinic.Application.Exceptions;
using VetClinic.Contracts.Dto.Breeds;
using VetClinic.Contracts.Dto.Pets;
using VetClinic.Contracts.Dto.Vets;
using VetClinic.Shared;
using VetClinic.Tests.Fixtures;

namespace VetClinic.Tests;

/// <summary>
/// Тесты CRUD-сервисов: связи между сущностями и правила предметной области
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

    private static VetEditDto NewVet(int? specializationId) => new()
    {
        PassportNumber = "4501 000777",
        FullName = "Тестов Тест Тестович",
        BirthYear = 1990,
        SpecializationId = specializationId,
        WorkExperienceYears = 5
    };

    /// <summary>
    /// Созданный питомец получает новый идентификатор и попадает в список питомцев владельца
    /// </summary>
    [Fact]
    public async Task CreatePet_AddsPetToOwner()
    {
        var s = NewServices();

        var created = await s.Pets.CreateAsync(NewPet(ownerId: 4), TestContext.Current.CancellationToken);

        Assert.True(created.Id > 0);
        Assert.Equal(4, created.OwnerId);
        Assert.Contains(await s.Pets.GetAllAsync(TestContext.Current.CancellationToken), p => p.Id == created.Id && p.OwnerId == 4);
    }

    /// <summary>
    /// При смене владельца у питомца меняется ссылка на владельца
    /// </summary>
    [Fact]
    public async Task UpdatePet_MovesPetBetweenOwners()
    {
        var s = NewServices();
        var cancellationToken = TestContext.Current.CancellationToken;
        var created = await s.Pets.CreateAsync(NewPet(ownerId: 4), cancellationToken);

        await s.Pets.UpdateAsync(created.Id, NewPet(ownerId: 5), cancellationToken);

        var moved = s.Seed.Pets.Single(pet => pet.Id == created.Id);
        Assert.Equal(5, moved.OwnerId);
        Assert.Equal(5, moved.Owner!.Id);
    }

    /// <summary>
    /// Питомец без породы сохраняется с пустой ссылкой на породу
    /// </summary>
    [Fact]
    public async Task UpdatePet_BreedIdNull_ClearsBreed()
    {
        var s = NewServices();
        var cancellationToken = TestContext.Current.CancellationToken;
        var created = await s.Pets.CreateAsync(NewPet(ownerId: 4, breedId: 1), cancellationToken);

        await s.Pets.UpdateAsync(created.Id, NewPet(ownerId: 4, breedId: null), cancellationToken);

        var stored = s.Seed.Pets.Single(pet => pet.Id == created.Id);
        Assert.Null(stored.BreedId);
        Assert.Null(stored.Breed);
    }

    /// <summary>
    /// Врач без специализации сохраняется с пустой ссылкой на специализацию
    /// </summary>
    [Fact]
    public async Task UpdateVet_SpecializationIdNull_ClearsSpecialization()
    {
        var s = NewServices();
        var cancellationToken = TestContext.Current.CancellationToken;
        var created = await s.Vets.CreateAsync(NewVet(specializationId: 1), cancellationToken);

        await s.Vets.UpdateAsync(created.Id, NewVet(specializationId: null), cancellationToken);

        var stored = s.Seed.Vets.Single(vet => vet.Id == created.Id);
        Assert.Null(stored.SpecializationId);
        Assert.Null(stored.Specialization);
    }

    /// <summary>
    /// Изменение несуществующей записи возвращает null, контроллер превращает его в 404
    /// </summary>
    [Fact]
    public async Task UpdatePet_NotFound_ReturnsNull()
    {
        var result = await NewServices().Pets.UpdateAsync(999, NewPet(ownerId: 1), TestContext.Current.CancellationToken);

        Assert.Null(result);
    }

    /// <summary>
    /// Ссылка на несуществующего владельца приводит к KeyNotFoundException
    /// </summary>
    [Fact]
    public async Task CreatePet_UnknownOwner_Throws()
    {
        var s = NewServices();

        await Assert.ThrowsAsync<KeyNotFoundException>(() => s.Pets.CreateAsync(NewPet(ownerId: 999), TestContext.Current.CancellationToken));
    }

    /// <summary>
    /// Повторное удаление не приводит к ошибке
    /// </summary>
    [Fact]
    public async Task DeletePet_Twice_DoesNotThrow()
    {
        var s = NewServices();
        var created = await s.Pets.CreateAsync(NewPet(ownerId: 4), TestContext.Current.CancellationToken);

        await s.Pets.DeleteAsync(created.Id, TestContext.Current.CancellationToken);
        await s.Pets.DeleteAsync(created.Id, TestContext.Current.CancellationToken);

        Assert.Null(await s.Pets.GetByIdAsync(created.Id, TestContext.Current.CancellationToken));
    }

    /// <summary>
    /// Питомца нельзя создать с породой другого вида: «Персидская» относится к кошкам
    /// </summary>
    [Fact]
    public async Task CreatePet_BreedOfOtherSpecies_Throws()
    {
        var s = NewServices();

        await Assert.ThrowsAsync<InvalidPetBreedException>(() => s.Pets.CreateAsync(NewPet(ownerId: 4, breedId: 4), TestContext.Current.CancellationToken));
    }

    /// <summary>
    /// При изменении питомца вид и порода тоже должны совпадать
    /// </summary>
    [Fact]
    public async Task UpdatePet_BreedOfOtherSpecies_Throws()
    {
        var s = NewServices();
        var created = await s.Pets.CreateAsync(NewPet(ownerId: 4), TestContext.Current.CancellationToken);

        await Assert.ThrowsAsync<InvalidPetBreedException>(() => s.Pets.UpdateAsync(created.Id, NewPet(ownerId: 4, breedId: 4), TestContext.Current.CancellationToken));
    }

    /// <summary>
    /// Вид породы нельзя сменить, если к ней привязаны питомцы: «Лабрадор» есть у Рекса и Лаки
    /// </summary>
    [Fact]
    public async Task UpdateBreed_SpeciesWithPetsAttached_Throws()
    {
        var s = NewServices();
        var breed = new BreedEditDto { Name = "Лабрадор", Species = AnimalSpecies.Cat };

        await Assert.ThrowsAsync<ConflictException>(() => s.Breeds.UpdateAsync(1, breed, TestContext.Current.CancellationToken));
    }

    /// <summary>
    /// У породы без питомцев вид меняется
    /// </summary>
    [Fact]
    public async Task UpdateBreed_SpeciesWithoutPets_ChangesSpecies()
    {
        var s = NewServices();
        var created = await s.Breeds.CreateAsync(new BreedEditDto { Name = "Новая", Species = AnimalSpecies.Dog }, TestContext.Current.CancellationToken);

        var updated = await s.Breeds.UpdateAsync(created.Id, new BreedEditDto { Name = "Новая", Species = AnimalSpecies.Cat }, TestContext.Current.CancellationToken);

        Assert.Equal(AnimalSpecies.Cat, updated!.Species);
    }
}
