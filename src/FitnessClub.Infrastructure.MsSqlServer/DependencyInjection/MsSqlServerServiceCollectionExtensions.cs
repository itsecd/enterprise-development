using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.MsSqlServer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FitnessClub.Infrastructure.MsSqlServer.DependencyInjection;

/// <summary>
/// Регистрация работы с MS SQL Server
/// </summary>
public static class MsSqlServerServiceCollectionExtensions
{
    /// <summary>
    /// Регистрирует контекст базы данных через Aspire-интеграцию (с телеметрией) и репозитории на базе Entity Framework
    /// </summary>
    /// <param name="builder">Построитель приложения</param>
    /// <param name="connectionName">Имя подключения, заданное в AppHost (имя ресурса базы данных)</param>
    /// <returns>Тот же построитель приложения</returns>
    public static IHostApplicationBuilder AddMsSqlServerPersistence(this IHostApplicationBuilder builder, string connectionName)
    {
        builder.AddSqlServerDbContext<FitnessClubDbContext>(connectionName);

        builder.Services.AddScoped<IClientRepository, EfClientRepository>();
        builder.Services.AddScoped<ITrainerRepository, EfTrainerRepository>();
        builder.Services.AddScoped<ISpecializationRepository, EfSpecializationRepository>();
        builder.Services.AddScoped<IEnrollmentRepository, EfEnrollmentRepository>();

        return builder;
    }

    /// <summary>
    /// Применяет миграции: создаёт таблицы и заполняет их начальными данными
    /// </summary>
    /// <param name="services">Контейнер сервисов приложения</param>
    /// <returns>Задача, завершающаяся после применения миграций</returns>
    public static async Task ApplyMigrationsAsync(this IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FitnessClubDbContext>();
        await context.Database.MigrateAsync();
    }
}