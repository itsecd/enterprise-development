using FitnessGym.Domain.Interfaces;
using FitnessGym.Infrastructure.PostgreSql.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessGym.Infrastructure.PostgreSql;

public static class PostgreSqlServiceCollectionExtensions
{
    public static IServiceCollection AddPostgreSqlRepositories(this IServiceCollection services)
    {
        services.AddScoped<IClientRepository, PostgreSqlClientRepository>();
        services.AddScoped<ITrainerRepository, PostgreSqlTrainerRepository>();
        services.AddScoped<ISpecializationRepository, PostgreSqlSpecializationRepository>();
        services.AddScoped<ITrainingSessionRepository, PostgreSqlTrainingSessionRepository>();
        return services;
    }
}
