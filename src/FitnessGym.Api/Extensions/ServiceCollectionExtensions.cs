using FitnessGym.Application.Services;
using FitnessGym.Contracts.Interfaces;

namespace FitnessGym.Api.Extensions;

/// <summary>
/// Регистрация сервисов приложения и репозиториев в контейнере зависимостей.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует отдельные сервисы для клиентов, тренеров, специализаций, занятий и залов.
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<ISpecializationService, SpecializationService>();
        services.AddScoped<ITrainingSessionService, TrainingSessionService>();
        services.AddScoped<IHallService, HallService>();
        return services;
    }
}
