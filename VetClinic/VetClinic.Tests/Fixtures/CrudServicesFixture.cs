using VetClinic.Application.Services;
using VetClinic.Domain.Seeds;
using VetClinic.Infrastructure.InMemory.Repositories;

namespace VetClinic.Tests.Fixtures;

/// <summary>
/// Набор CRUD-сервисов вместе с начальными данными
/// </summary>
public sealed record CrudServices(
    VetClinicSeed Seed,
    OwnerService Owners,
    PetService Pets,
    AppointmentService Appointments,
    BreedService Breeds,
    VetService Vets);

/// <summary>
/// Собирает CRUD-сервисы с репозиториями.
/// Тесты CRUD меняют данные, поэтому каждый тест получает свой набор сервисов
/// </summary>
public sealed class CrudServicesFixture
{
    /// <summary>
    /// Создать набор сервисов поверх новых начальных данных
    /// </summary>
    public CrudServices Create()
    {
        var seed = new VetClinicSeed();

        var ownerRepo        = new OwnerRepository(seed);
        var petRepo          = new PetRepository(seed);
        var vetRepo          = new VetRepository(seed);
        var appointmentRepo  = new AppointmentRepository(seed);
        var breedRepo        = new BreedRepository(seed);
        var specializationRepo = new SpecializationRepository(seed);

        return new CrudServices(
            seed,
            new OwnerService(ownerRepo, petRepo, appointmentRepo),
            new PetService(petRepo, ownerRepo, breedRepo, appointmentRepo),
            new AppointmentService(appointmentRepo, petRepo, vetRepo),
            new BreedService(breedRepo, petRepo),
            new VetService(vetRepo, specializationRepo, appointmentRepo));
    }
}
