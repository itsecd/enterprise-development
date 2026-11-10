using VetClinic.Application.Services;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Interfaces;
using VetClinic.Infrastructure.InMemory.Repositories;
using VetClinic.Infrastructure.InMemory.Seeding;

namespace VetClinic.WebApi.Extensions;

/// <summary>
/// Методы расширения для регистрации сервисов в контейнере зависимостей
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрация репозиториев для хранения данных в памяти
    /// </summary>
    public static IServiceCollection AddInMemoryRepositories(this IServiceCollection services)
    {
        var seeder = new ClinicDataSeeder();

        services.AddSingleton<IRepository<Breed>>(_ => new InMemoryRepository<Breed>(seeder.Breeds));
        services.AddSingleton<IRepository<Specialization>>(_ => new InMemoryRepository<Specialization>(seeder.Specializations));
        services.AddSingleton<IRepository<Owner>>(_ => new InMemoryRepository<Owner>(seeder.Owners));
        services.AddSingleton<IRepository<Pet>>(_ => new InMemoryRepository<Pet>(seeder.Pets));
        services.AddSingleton<IRepository<Veterinarian>>(_ => new InMemoryRepository<Veterinarian>(seeder.Veterinarians));
        services.AddSingleton<IRepository<Appointment>>(_ => new InMemoryRepository<Appointment>(seeder.Appointments));

        return services;
    }

    /// <summary>
    /// Регистрация сервисов бизнес-логики приложения
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IVetClinicService, VetClinicService>();
        return services;
    }
}