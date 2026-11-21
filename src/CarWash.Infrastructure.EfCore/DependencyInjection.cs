using CarWash.Domain.Interfaces;
using CarWash.Infrastructure.EfCore.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;

namespace CarWash.Infrastructure.EfCore;

/// <summary>
/// Методы расширения для подключения инфраструктуры на базе EF Core
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует контекст БД (MySQL) и репозитории
    /// </summary>
    /// <param name="services">Коллекция сервисов</param>
    /// <returns>Коллекция сервисов</returns>
    public static IServiceCollection AddEfCoreInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<CarWashDbContext>((serviceProvider, options) =>
            options.UseMySql(
                serviceProvider.GetRequiredService<MySqlDataSource>(),
                CarWashDbContext.MySqlVersion,
                mySqlOptions => mySqlOptions.MigrationsHistoryTable(CarWashDbContext.MigrationsHistoryTableName)));

        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IServiceRepository, ServiceRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }

    /// <summary>
    /// Применяет непримененные миграции к базе данных (создаёт таблицы и заполняет их начальными данными)
    /// </summary>
    /// <param name="services">Провайдер сервисов</param>
    public static async Task ApplyMigrationsAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<CarWashDbContext>();
        await context.Database.MigrateAsync();
    }
}
