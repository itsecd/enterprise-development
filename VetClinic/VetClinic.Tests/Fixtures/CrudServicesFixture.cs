using VetClinic.Application.Services;
using VetClinic.Infrastructure.SqlServer.Repositories;

namespace VetClinic.Tests.Fixtures;

/// <summary>
/// Набор CRUD-сервисов поверх отдельной базы
/// </summary>
public sealed record CrudServices(
    TestDatabase Database,
    OwnerService Owners,
    PetService Pets,
    AppointmentService Appointments,
    BreedService Breeds,
    VetService Vets);

/// <summary>
/// Собирает CRUD-сервисы. Тесты CRUD меняют данные, поэтому каждый тест получает свою базу
/// </summary>
public sealed class CrudServicesFixture : IAsyncDisposable
{
    private readonly List<TestDatabase> _databases = [];

    /// <summary>
    /// Создать набор сервисов поверх новой базы с начальными данными
    /// </summary>
    public CrudServices Create()
    {
        var database = new TestDatabase();
        _databases.Add(database);
        var context = database.Context;

        var ownerRepo = new OwnerRepository(context);
        var petRepo = new PetRepository(context);
        var vetRepo = new VetRepository(context);
        var appointmentRepo = new AppointmentRepository(context);
        var breedRepo = new BreedRepository(context);
        var specializationRepo = new SpecializationRepository(context);

        return new CrudServices(
            database,
            new OwnerService(ownerRepo),
            new PetService(petRepo, ownerRepo, breedRepo),
            new AppointmentService(appointmentRepo, petRepo, vetRepo),
            new BreedService(breedRepo),
            new VetService(vetRepo, specializationRepo));
    }

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        foreach (var database in _databases)
        {
            await database.DisposeAsync();
        }
    }
}
