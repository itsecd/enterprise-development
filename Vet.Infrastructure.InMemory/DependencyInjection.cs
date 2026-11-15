using Microsoft.Extensions.DependencyInjection;
using Vet.Domain.DataSeed;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;
using Vet.Infrastructure.InMemory.Repositories;

namespace Vet.Infrastructure.InMemory;

/// <summary>
/// Внедрение зависимостей слоя inmemory
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует тестовые данные и репозитории в контейнере зависимостей
    /// </summary>
    /// <param name="services">Коллекция сервисов приложения</param>
    /// <returns>Коллекция сервисов приложения</returns>
    public static IServiceCollection AddInMemoryInfrastructure(this IServiceCollection services)
    {
        var fixture = new VetClinicTestFixture();
        services.AddSingleton(fixture);

        var vetRepo = new VeterinarianRepository(fixture);
        var ownerRepo = new OwnerRepository(fixture);
        var petRepo = new PetRepository(fixture);
        var apptRepo = new AppointmentRepository(fixture);

        services.AddSingleton<IRepository<Breed, int>>(new BreedRepository(fixture));
        services.AddSingleton<IRepository<Specialization, int>>(new SpecializationRepository(fixture));

        services.AddSingleton<IVeterinarianRepository>(vetRepo);
        services.AddSingleton<IRepository<Veterinarian, int>>(vetRepo);

        services.AddSingleton<IOwnerRepository>(ownerRepo);
        services.AddSingleton<IRepository<Owner, int>>(ownerRepo);

        services.AddSingleton<IPetRepository>(petRepo);
        services.AddSingleton<IRepository<Pet, int>>(petRepo);

        services.AddSingleton<IAppointmentRepository>(apptRepo);
        services.AddSingleton<IRepository<Appointment, int>>(apptRepo);

        return services;
    }
}