using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.EfCore.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Infrastructure.EfCore;

/// <summary>Регистрация контекста и репозиториев PostgreSQL.</summary>
public static class DependencyInjection
{
    /// <summary>Регистрирует контекст и репозитории на время HTTP-запроса.</summary>
    public static IServiceCollection AddPostgresRepositories(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ITrainerRepository, TrainerRepository>();
        services.AddScoped<IHallRepository, HallRepository>();
        services.AddScoped<ISpecializationRepository, SpecializationRepository>();
        services.AddScoped<ITrainingRepository, TrainingRepository>();
        services.AddScoped<IAnalyticsRepository, AnalyticsRepository>();
        return services;
    }
}
