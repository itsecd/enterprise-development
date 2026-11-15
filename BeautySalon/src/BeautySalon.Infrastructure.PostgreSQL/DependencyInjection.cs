using BeautySalon.Domain.Interfaces;
using BeautySalon.Infrastructure.PostgreSQL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BeautySalon.Infrastructure.PostgreSQL;

/// <summary>
/// Регистрация PostgreSQL-хранилища в контейнере зависимостей
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Регистрирует контекст базы данных и репозитории сущностей
    /// </summary>
    public static IHostApplicationBuilder AddPostgreSqlStorage(this IHostApplicationBuilder builder, string connectionName)
    {
        builder.AddNpgsqlDbContext<SalonDbContext>(connectionName, settings => settings.DisableRetry = true);
        builder.Services.AddScoped<IClientRepository, ClientRepository>();
        builder.Services.AddScoped<IMasterRepository, MasterRepository>();
        builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
        builder.Services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        return builder;
    }
}