using VeterinaryClinic.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Tests.TestDoubles;

/// <summary>
/// Регистрирует изолированные тестовые подмены репозиториев
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Подключает тестовые репозитории для всех ресурсов клиники
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации</param>
    public static IServiceCollection AddTestRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IRepository<Breed>>(_ => new TestRepository<Breed>([], x => x.Id));
        services.AddSingleton<IRepository<Specialization>>(_ => new TestRepository<Specialization>([], x => x.Id));
        services.AddSingleton<IRepository<Owner>>(_ => new TestRepository<Owner>([], x => x.Id));
        services.AddSingleton<IRepository<Veterinarian>>(_ => new TestRepository<Veterinarian>([], x => x.Id));
        services.AddSingleton<IRepository<Pet>>(_ => new TestRepository<Pet>([], x => x.Id));
        services.AddSingleton<IRepository<Appointment>>(_ => new TestRepository<Appointment>([], x => x.Id));
        services.AddSingleton<IOwnerAnalytics, TestOwnerAnalytics>();
        return services;
    }
}
