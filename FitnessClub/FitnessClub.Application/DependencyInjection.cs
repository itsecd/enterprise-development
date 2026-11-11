using Microsoft.Extensions.DependencyInjection;
using FitnessClub.Application.Services;
using FitnessClub.Contracts.Services;

namespace FitnessClub.Application;

/// <summary>
/// Регистрация зависимостей уровня Application.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует сервисы приложения.
    /// </summary>
    /// <param name="services">Коллекция сервисов.</param>
    /// <returns>Обновлённая коллекция сервисов.</returns>
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<ISpecializationService, SpecializationService>();
        services.AddScoped<IHallService, HallService>();
        services.AddScoped<ITrainingService, TrainingService>();
        services.AddScoped<IAnalyticsService, AnalyticsService>();
        return services;
    }
}