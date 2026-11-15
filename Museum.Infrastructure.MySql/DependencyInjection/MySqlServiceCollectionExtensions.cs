using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Museum.Domain.Interfaces;
using Museum.Infrastructure.MySql.Repositories;

namespace Museum.Infrastructure.MySql.DependencyInjection;

/// <summary>
/// Подключает базу данных MySQL и репозитории музея
/// </summary>
public static class MySqlServiceCollectionExtensions
{
    /// <summary>
    /// Версия сервера MySQL, под которую генерируются запросы
    /// </summary>
    public static readonly ServerVersion ServerVersion = new MySqlServerVersion(new Version(8, 0, 36));

    /// <summary>
    /// Регистрирует контекст базы данных и все репозитории
    /// </summary>
    public static IServiceCollection AddMySqlPersistence(
        this IServiceCollection services,
        string? connectionString)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(
            connectionString,
            "Не задана строка подключения museum. Запускайте проект через Museum.AppHost");

        services.AddDbContext<MuseumDbContext>(options =>
            options.UseMySql(connectionString, ServerVersion));

        services.AddScoped<IExhibitionRepository, EfExhibitionRepository>();
        services.AddScoped<ITourRepository, EfTourRepository>();
        services.AddScoped<IVisitorRepository, EfVisitorRepository>();
        services.AddScoped<ITicketRepository, EfTicketRepository>();

        return services;
    }

    /// <summary>
    /// Применяет миграции: создаёт таблицы и заполняет их начальными данными
    /// </summary>
    public static async Task ApplyMigrationsAsync(this IServiceProvider services)
    {
        await using var scope = services.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<MuseumDbContext>();

        await context.Database.MigrateAsync();
    }
}
