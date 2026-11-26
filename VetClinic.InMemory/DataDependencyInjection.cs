using Microsoft.Extensions.DependencyInjection;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;
using VetClinic.Domain.Seeder;
using VetClinic.InMemory.Repositories;

namespace VetClinic.InMemory;
/// <summary>
/// Класс, отвечающий за подключение in memory репозитория
/// </summary>
public static class DataDependencyInjection
{
    /// <summary>
    /// Регистрирует централизованный сидер данных как Singleton и привязывает интерфейсы репозиториев к их InMemory-реализациям как Scoped
    /// </summary>
    /// <param name="services">Коллекция дескрипторов сервисов</param>
    /// <returns>Обновленная коллекция сервисов</returns>
    public static IServiceCollection AddDataLayer(this IServiceCollection services)
    {
        services.AddSingleton<VetClinicDataSeeder>();

        services.AddScoped<IRepository<Pet>, PetRepository>();
        services.AddScoped<IRepository<Specialization>, SpecializationRepository>();
        services.AddScoped<IRepository<Breed>, BreedRepository>();
        services.AddScoped<IRepository<Owner>, OwnerRepository>();
        services.AddScoped<IRepository<Service>, ServiceRepository>();
        services.AddScoped<IRepository<Vet>, VetRepository>();
        services.AddScoped<IVetRepository, VetRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();

        return services;
    }
}

