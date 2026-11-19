using Aspire.Hosting;
using Aspire.Hosting.Testing;

namespace AutoService.IntegrationTests;

/// <summary>
/// Фикстура, которая один раз поднимает оркестратор Aspire со всеми ресурсами
/// и переиспользует его во всех интеграционных тестах коллекции.
/// </summary>
public sealed class AspireAppFixture : IAsyncLifetime
{
    private static readonly TimeSpan _startupTimeout = TimeSpan.FromMinutes(5);

    private readonly CancellationTokenSource _cts = new(_startupTimeout);
    private DistributedApplication? _app;

    /// <summary>
    /// Запущенное распределенное приложение (API + PostgreSQL).
    /// </summary>
    public DistributedApplication App => _app
        ?? throw new InvalidOperationException("Приложение не запущено: InitializeAsync не вызывался.");

    public async ValueTask InitializeAsync()
    {
        var appHost = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.AutoService_AppHost>(_cts.Token);
        var app = await appHost.BuildAsync(_cts.Token);
        await app.StartAsync(_cts.Token);
        _app = app;
    }

    public async ValueTask DisposeAsync()
    {
        if (_app is not null)
            await _app.DisposeAsync();
        _cts.Cancel();
        _cts.Dispose();
    }
}
