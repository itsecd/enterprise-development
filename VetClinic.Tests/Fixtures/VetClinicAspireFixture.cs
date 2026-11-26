using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace VetClinic.Tests.Fixtures;

/// <summary>
/// Фикстура для однократного поднятия оркестратора Aspire и контейнера MySQL на время выполнения интеграционных тестов
/// </summary>
public class VetClinicAspireFixture : IAsyncLifetime
{
    private DistributedApplication? _app;

    /// <summary>
    /// HTTP-клиент, предварительно настроенный на адрес запущенного VetClinic.Api.Host
    /// </summary>
    public HttpClient HttpClient { get; private set; } = null!;

    public async ValueTask InitializeAsync()
    {
        var appHost = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.VetClinic_AppHost>();

        _app = await appHost.BuildAsync();
        await _app.StartAsync();

        var notificationService = _app.Services.GetRequiredService<ResourceNotificationService>();
        await notificationService.WaitForResourceHealthyAsync("vetclinic-api");

        using var defaultClient = _app.CreateHttpClient("vetclinic-api");
        var baseAddress = defaultClient.BaseAddress;

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };

        HttpClient = new HttpClient(handler)
        {
            BaseAddress = baseAddress
        };
    }

    public async ValueTask DisposeAsync()
    {
        HttpClient?.Dispose();
        if (_app is not null)
        {
            await _app.DisposeAsync();
        }
    }
}
