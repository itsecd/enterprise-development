using FitnessClub.Domain.Interfaces;
using FitnessClub.Domain.Seeds;
using FitnessClub.Infrastructure.InMemory.Repositories;
using FitnessClub.Infrastructure.InMemory.Seeding;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Infrastructure.InMemory;

public static class DependencyInjection
{
    public static IServiceCollection AddInMemoryStorage(this IServiceCollection services)
    {
        services.AddSingleton(typeof(IRepository<>), typeof(InMemoryRepository<>));
        services.AddTransient<DataSeeder>();
        return services;
    }

    public static async Task SeedInMemoryStorageAsync(
        this IServiceProvider services,
        CancellationToken cancellationToken = default)
    {
        await using var scope = services.CreateAsyncScope();
        var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();
        await seeder.DataSeedingAsync(new SeedData(), cancellationToken);
    }
}