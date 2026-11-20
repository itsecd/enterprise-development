using VeterinaryClinic.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Infrastructure.PostgreSQL;

public static class PostgreSqlRegistration
{
    public static IServiceCollection AddClinicPostgreSql(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ClinicDbContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped(typeof(IRepository<>), typeof(PostgreSqlRepository<>));
        services.AddScoped<IOwnerAnalytics, PostgreSqlOwnerAnalytics>();
        return services;
    }
}
