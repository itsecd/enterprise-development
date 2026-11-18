using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.PostgreSql.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Infrastructure.PostgreSql;

/// <summary>
/// Регистрация PostgreSQL-хранилища в DI-контейнере
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавляет EF Core-репозитории контракты
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Коллекция сервисов для цепочки вызовов</returns>
    public static IServiceCollection AddPostgreSqlRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISpecializationRepository, SpecializationRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ITrainerRepository, TrainerRepository>();
        services.AddScoped<ILessonRepository, LessonRepository>();

        return services;
    }
}
