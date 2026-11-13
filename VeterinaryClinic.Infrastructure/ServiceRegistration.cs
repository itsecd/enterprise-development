using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Infrastructure;

/// <summary>
/// Регистрирует реализации репозиториев для работы с данными в памяти. Не отвечает за создание тестовых записей
/// </summary>
public static class ServiceRegistration
{
    /// <summary>
    /// Подключает пустые хранилища для всех ресурсов клиники
    /// </summary>
    /// <param name="services">Коллекция сервисов для регистрации</param>
    public static IServiceCollection AddClinicRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IRepository<Breed>>(_ => new InMemoryRepository<Breed>([], x => x.Id));
        services.AddSingleton<IRepository<Specialization>>(_ => new InMemoryRepository<Specialization>([], x => x.Id));
        services.AddSingleton<IRepository<Owner>>(_ => new InMemoryRepository<Owner>([], x => x.Id));
        services.AddSingleton<IRepository<Veterinarian>>(_ => new InMemoryRepository<Veterinarian>([], x => x.Id));
        services.AddSingleton<IRepository<Pet>>(_ => new InMemoryRepository<Pet>([], x => x.Id));
        services.AddSingleton<IRepository<Appointment>>(_ => new InMemoryRepository<Appointment>([], x => x.Id));
        return services;
    }
}
