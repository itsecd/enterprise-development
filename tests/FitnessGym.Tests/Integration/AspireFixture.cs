using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Testing;
using FitnessGym.Infrastructure.PostgreSql;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Tests.Integration;

[CollectionDefinition("Aspire")]
public class AspireCollection : ICollectionFixture<AspireFixture>;

/// <summary>
/// Запускает настоящий AppHost: PostgreSQL, миграции и API, без рабочего Docker volume.
/// </summary>
public sealed class AspireFixture : IAsyncLifetime
{
    private IDistributedApplicationTestingBuilder? _builder;
    private DistributedApplication? _application;

    public string ConnectionString { get; private set; } = string.Empty;

    public HttpClient CreateHttpClient() => _application!.CreateHttpClient("api", "http");

    public FitnessGymDbContext CreateContext() => new(new DbContextOptionsBuilder<FitnessGymDbContext>()
        .UseNpgsql(ConnectionString).Options);

    public async ValueTask InitializeAsync()
    {
        using var timeout = new CancellationTokenSource(TimeSpan.FromMinutes(3));
        var token = timeout.Token;
        try
        {
            _builder = await DistributedApplicationTestingBuilder.CreateAsync<Projects.FitnessGym_AppHost>(
                ["UseVolumes=false", $"Parameters:postgres-password=test-{Guid.NewGuid():N}"], token);
            var postgres = _builder.Resources.Single(resource => resource.Name == "postgres");
            Assert.Empty(postgres.Annotations.OfType<ContainerMountAnnotation>());

            _application = await _builder.BuildAsync(token);
            await _application.StartAsync(token);
            await _application.ResourceNotifications.WaitForResourceHealthyAsync("fitnessgym", token);
            await _application.ResourceNotifications.WaitForResourceAsync("migrations", KnownResourceStates.Finished, token);
            await _application.ResourceNotifications.WaitForResourceHealthyAsync("api", token);
            ConnectionString = await _application.GetConnectionStringAsync("fitnessgym", token)
                ?? throw new InvalidOperationException("Aspire не предоставил строку подключения к тестовой базе.");
        }
        catch
        {
            await DisposeAsync();
            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_application is not null)
        {
            await _application.DisposeAsync();
            _application = null;
        }
        if (_builder is not null)
        {
            await _builder.DisposeAsync();
            _builder = null;
        }
    }
}
