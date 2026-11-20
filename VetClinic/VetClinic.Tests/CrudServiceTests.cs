using Microsoft.EntityFrameworkCore;
using VetClinic.Contracts.Dto.Pets;
using VetClinic.Contracts.Dto.Vets;
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

        var created = await s.Pets.CreateAsync(NewPet(ownerId: 4), TestContext.Current.CancellationToken);

        Assert.Equal(12, created.Id);
        Assert.Equal(4, created.OwnerId);
        Assert.Contains(await s.Pets.GetAllAsync(TestContext.Current.CancellationToken), p => p.Id == created.Id && p.OwnerId == 4);
    }

    /// <summary>
    /// При смене владельца питомец переходит из одного списка в другой
    /// </summary>
    [Fact]
    public async Task UpdatePet_MovesPetBetweenOwners()
    {
        var s = NewServices();
        var created = await s.Pets.CreateAsync(NewPet(ownerId: 4), TestContext.Current.CancellationToken);

        await s.Pets.UpdateAsync(created.Id, NewPet(ownerId: 5), TestContext.Current.CancellationToken);

        var moved = await s.Pets.GetByIdAsync(created.Id, TestContext.Current.CancellationToken);
        Assert.Equal(5, moved!.OwnerId);
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
    /// Удаление владельца удаляет его питомцев и их записи на прием
    /// </summary>
    [Fact]
    public async Task DeleteOwner_CascadesToPetsAndAppointments()
    {
        var s = NewServices();

        await s.Owners.DeleteAsync(1, TestContext.Current.CancellationToken);

        Assert.Null(await s.Owners.GetByIdAsync(1, TestContext.Current.CancellationToken));
        Assert.Equal(9, (await s.Pets.GetAllAsync(TestContext.Current.CancellationToken)).Count);
        Assert.Equal(13, (await s.Appointments.GetAllAsync(TestContext.Current.CancellationToken)).Count);
    }

    /// <summary>
    /// Удаление врача удаляет его записи на прием
    /// </summary>
    [Fact]
    public async Task DeleteVet_CascadesToAppointments()
    {
        var s = NewServices();

        await s.Vets.DeleteAsync(6, TestContext.Current.CancellationToken);

        Assert.DoesNotContain(await s.Appointments.GetAllAsync(TestContext.Current.CancellationToken), a => a.VetId == 6);
    }

    /// <summary>
    /// Удаление породы не удаляет питомцев, а сбрасывает у них породу
    /// </summary>
    [Fact]
    public async Task DeleteBreed_SetsPetBreedToNull()
    {
        var s = NewServices();
        var petsBefore = (await s.Pets.GetAllAsync(TestContext.Current.CancellationToken)).Count;

        await s.Breeds.DeleteAsync(1, TestContext.Current.CancellationToken);

        var pets = await s.Pets.GetAllAsync(TestContext.Current.CancellationToken);
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
        var created = await s.Pets.CreateAsync(NewPet(ownerId: 4), TestContext.Current.CancellationToken);

        await s.Pets.DeleteAsync(created.Id, TestContext.Current.CancellationToken);
        await s.Pets.DeleteAsync(created.Id, TestContext.Current.CancellationToken);

        Assert.Null(await s.Pets.GetByIdAsync(created.Id, TestContext.Current.CancellationToken));
    }

    /// <summary>
    /// Номер паспорта врача уникален, повтор отклоняет база
    /// </summary>
    [Fact]
    public async Task CreateVet_DuplicatePassport_Throws()
    {
        var s = NewServices();
        var vet = new VetEditDto
        {
            PassportNumber = "4501 000001",
            FullName = "Тестов Тест Тестович",
            BirthYear = 1990,
            WorkExperienceYears = 5
        };

        await Assert.ThrowsAsync<DbUpdateException>(() => s.Vets.CreateAsync(vet, TestContext.Current.CancellationToken));
    }

    /// <summary>
    /// Питомца нельзя создать с породой другого вида: «Персидская» относится к кошкам
    /// </summary>
    [Fact]
    public async Task CreatePet_BreedOfOtherSpecies_Throws()
    {
        var s = NewServices();

        await Assert.ThrowsAsync<ArgumentException>(() => s.Pets.CreateAsync(NewPet(ownerId: 4, breedId: 4), TestContext.Current.CancellationToken));
    }

    /// <summary>
    /// При изменении питомца вид и порода тоже должны совпадать
    /// </summary>
    [Fact]
    public async Task UpdatePet_BreedOfOtherSpecies_Throws()
    {
        var s = NewServices();
        var created = await s.Pets.CreateAsync(NewPet(ownerId: 4), TestContext.Current.CancellationToken);

        await Assert.ThrowsAsync<ArgumentException>(() => s.Pets.UpdateAsync(created.Id, NewPet(ownerId: 4, breedId: 4), TestContext.Current.CancellationToken));
    }
}
