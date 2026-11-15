using Aspire.Hosting;
using Aspire.Hosting.Testing;

namespace MuseumSystem.IntegrationTests;

public class AppHostFixture : IAsyncLifetime
{
    private DistributedApplication? _app;

    public HttpClient HttpClient { get; private set; } = null!;

    public async ValueTask InitializeAsync()
    {
        // Запускаем Aspire один раз
        var appHost = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.MuseumSystem_AppHost>();

        _app = await appHost.BuildAsync();
        await _app.StartAsync();

        // Ждем, пока API будет готов
        await _app.ResourceNotifications.WaitForResourceHealthyAsync(
            "museum-api",
            TestContext.Current.CancellationToken);

        // Создаем HttpClient
        HttpClient = _app.CreateHttpClient("museum-api");
    }

    public async ValueTask DisposeAsync()
    {
        if (_app is not null)
        {
            await _app.DisposeAsync();
        }
    }
}