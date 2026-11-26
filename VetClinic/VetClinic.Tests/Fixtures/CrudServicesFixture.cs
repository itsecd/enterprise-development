using VetClinic.Application.Services;
using VetClinic.Domain.Seeds;
using VetClinic.Infrastructure.InMemory;
using VetClinic.Infrastructure.InMemory.Repositories;

namespace VetClinic.Tests.Fixtures;

/// <summary>
/// Собирает CRUD-сервисы поверх репозиториев в памяти. Тесты меняют данные, поэтому каждый тест получает свой набор
/// </summary>
public sealed class CrudServicesFixture
{
    /// <summary>
    /// Создать набор сервисов поверх новых начальных данных
    /// </summary>
    public CrudServices Create()
    {
        var seed = new VetClinicSeed();
        var transaction = new InMemoryClinicTransaction();

        return new CrudServices(
            seed,
            new OwnerService(new OwnerRepository(seed)),
            new PetService(new PetRepository(seed), new OwnerRepository(seed), new BreedRepository(seed), transaction),
            new AppointmentService(new AppointmentRepository(seed), new PetRepository(seed), new VetRepository(seed)),
            new BreedService(new BreedRepository(seed), new PetRepository(seed), transaction),
            new VetService(new VetRepository(seed), new SpecializationRepository(seed)));
    }
}
