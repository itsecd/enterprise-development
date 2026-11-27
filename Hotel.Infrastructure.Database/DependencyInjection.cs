using Hotel.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Infrastructure.Database;

/// <summary>
/// Регистрация зависимостей базы данных
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавляет репозитории для работы с базой данных в коллекцию сервисов
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Коллекция сервисов с зарегистрированными репозиториями</returns>
    public static IServiceCollection AddDatabaseInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        return services;
    }
}
