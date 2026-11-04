using Microsoft.Extensions.DependencyInjection;
using Vet.Domain.DataSeed;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;
using Vet.Infrastructure.InMemory.Repositories;

namespace Vet.Infrastructure.InMemory;

public static class DependencyInjection
{
    public static IServiceCollection AddInMemoryInfrastructure(this IServiceCollection services)
    {
        var fixture = new VetClinicTestFixture();

        services.AddSingleton(fixture);

        services.AddSingleton<IRepository<Breed, int>>(new BreedRepository(fixture));
        services.AddSingleton<IRepository<Specialization, int>>(new SpecializationRepository(fixture));
        services.AddSingleton<IRepository<Veterinarian, int>>(new VeterinarianRepository(fixture));
        services.AddSingleton<IRepository<Owner, int>>(new OwnerRepository(fixture));
        services.AddSingleton<IRepository<Pet, int>>(new PetRepository(fixture));
        services.AddSingleton<IRepository<Appointment, int>>(new AppointmentRepository(fixture));

        return services;
    }
}