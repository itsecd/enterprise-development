using FitnessClub.Application.Services;
using FitnessClub.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Application;

/// <summary>
/// Регистрация сервисов бизнес-логики в DI-контейнере
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавляет CRUD- и аналитические сервисы
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ISpecializationService, SpecializationService>();
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ITrainerService, TrainerService>();
        services.AddScoped<ILessonService, LessonService>();
        services.AddScoped<IAnalyticsService, AnalyticsService>();

        return services;
    }
}