using Microsoft.Extensions.DependencyInjection;
using Museum.Application.Services;
using Museum.Contracts.Interfaces;

namespace Museum.Application.DependencyInjection;

/// <summary>Добавляет сервисы приложения в контейнер зависимостей</summary>
public static class ApplicationServiceCollectionExtensions
{
    /// <summary>Регистрирует сервисы для работы с данными музея и аналитикой</summary>
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IExhibitionService, ExhibitionService>();
        services.AddScoped<ITourService, TourService>();
        services.AddScoped<IVisitorService, VisitorService>();
        services.AddScoped<ITicketService, TicketService>();
        services.AddScoped<IAnalyticsService, AnalyticsService>();

        return services;
    }
}
