using BeautySalon.Application.Services;
using BeautySalon.Contracts.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BeautySalon.Application;

/// <summary>
/// Регистрация сервисов слоя Application в контейнере зависимостей
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует сервисы управления сущностями и сервис аналитики
    /// </summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IMasterService, MasterService>();
        services.AddScoped<IServiceService, SalonServiceService>();
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IAnalyticsService, AnalyticsService>();
        return services;
    }
}