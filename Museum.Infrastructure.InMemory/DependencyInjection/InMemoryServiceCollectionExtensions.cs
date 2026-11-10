using Microsoft.Extensions.DependencyInjection;
using Museum.Domain;
using Museum.Domain.Interfaces;
using Museum.Infrastructure.InMemory.Repositories;

namespace Museum.Infrastructure.InMemory.DependencyInjection;

/// <summary>
/// Подключает хранилище и репозитории музея
/// </summary>
public static class InMemoryServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует все репозитории для работы с данными в памяти
    /// </summary>
    public static IServiceCollection AddInMemoryPersistence(
        this IServiceCollection services)
    {
        // Один набор связанных данных на всё приложение
        services.AddSingleton<DataSeeder>();

        services.AddScoped<IExhibitionRepository, InMemoryExhibitionRepository>();
        services.AddScoped<ITourRepository, InMemoryTourRepository>();
        services.AddScoped<IVisitorRepository, InMemoryVisitorRepository>();
        services.AddScoped<ITicketRepository, InMemoryTicketRepository>();

        return services;
    }
}
