using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Seeds;
using FitnessClub.Infrastructure.InMemory;
using FitnessClub.Infrastructure.InMemory.Seeding;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Application.Tests;

public sealed class BookingQueriesFixture : IAsyncLifetime
{
    private IAnalyticsService? _analyticsService;
    private ServiceProvider? _serviceProvider;
    private AsyncServiceScope? _scope;
    public SeedData SeedData { get; } = new();
    public IAnalyticsService AnalyticsService => 
        _analyticsService 
        ?? throw new InvalidOperationException("Фикстура не инициализирована.");

    public async ValueTask InitializeAsync()
    {
        var services = new ServiceCollection();
        services.AddInMemoryStorage();
        services.AddApplication();
        _serviceProvider = services.BuildServiceProvider();
        _scope = _serviceProvider.CreateAsyncScope();
        var scopedServices = _scope.Value.ServiceProvider;
        var seeder = scopedServices.GetRequiredService<DataSeeder>();
        await seeder.DataSeedingAsync(SeedData);
        _analyticsService = scopedServices.GetRequiredService<IAnalyticsService>();
    }

    public async ValueTask DisposeAsync()
    {
        if (_scope is not null)
            await _scope.Value.DisposeAsync();
        if (_serviceProvider is not null)
            await _serviceProvider.DisposeAsync();
    }
}