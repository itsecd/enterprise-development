using Hotel.Application.Interfaces;
using Hotel.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hotel.Application;

/// <summary>
/// Регистрация зависимостей application
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Добавляет сервисы приложения в коллекцию сервисов
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Коллекция сервисов с зарегистрированными сервисами</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<IRoomTypeService, RoomTypeService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IAnalyticsService, AnalyticsService>();

        return services;
    }
}