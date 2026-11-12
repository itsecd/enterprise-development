using FitnessGym.Api.Controllers;
using FitnessGym.Domain.Interfaces;
using FitnessGym.Domain.Seeds;
using FitnessGym.Infrastructure.InMemory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FitnessGym.Tests;

/// <summary>
/// Подмена репозиториев только для unit/HTTP-тестов; рабочий API всегда использует PostgreSQL.
/// </summary>
public class InMemoryApiFactory : WebApplicationFactory<ClientsController>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((_, config) => config.AddInMemoryCollection(
            new Dictionary<string, string?>
            {
                ["Logging:LogLevel:Default"] = "None",
                ["ConnectionStrings:fitnessgym"] = "Host=localhost;Database=unused;Username=unused",
            }));
        builder.ConfigureServices(services =>
        {
            var data = ClubDataSeeder.Create();
            services.RemoveAll<IClientRepository>();
            services.RemoveAll<ITrainerRepository>();
            services.RemoveAll<ISpecializationRepository>();
            services.RemoveAll<ITrainingSessionRepository>();
            services.AddSingleton<IClientRepository>(new InMemoryClientRepository(data.Clients));
            services.AddSingleton<ITrainerRepository>(new InMemoryTrainerRepository(data.Trainers));
            services.AddSingleton<ISpecializationRepository>(new InMemorySpecializationRepository(data.Specializations));
            services.AddSingleton<ITrainingSessionRepository>(new InMemoryTrainingSessionRepository(data.Sessions));
        });
    }
}
