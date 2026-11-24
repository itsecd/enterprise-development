using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.Testing;

namespace VetClinic.IntegrationTests;

/// <summary>
/// Поднимает приложение через Aspire с настоящим SQL Server на время всего класса тестов
/// </summary>
public sealed class ClinicApplicationFixture : IAsyncLifetime
{
    private static readonly TimeSpan StartupTimeout = TimeSpan.FromMinutes(3);

    private DistributedApplication _application = null!;

    /// <summary>
    /// Клиент API, готовый к запросам после запуска приложения
    /// </summary>
    public HttpClient Api { get; private set; } = null!;

    /// <summary>
    /// Поднимает приложение на чистом томе, чтобы тесты не зависели от данных разработчика
    /// </summary>
    /// <remarks>
    /// Пустой путь к файлу AppHost отличает тестовый запуск от обычного, иначе Aspire
    /// считает их одним приложением и останавливает запущенный из Visual Studio
    /// </remarks>
    public async ValueTask InitializeAsync()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var builder = await DistributedApplicationTestingBuilder.CreateAsync<Projects.VetClinic_AppHost>(
            ["--SqlDataVolume=false"], cancellationToken);
        builder.Configuration["AppHost:FilePath"] = string.Empty;
        _application = await builder.BuildAsync();
        await _application.StartAsync(cancellationToken);

        await _application.ResourceNotifications.WaitForResourceHealthyAsync("api", cancellationToken)
            .WaitAsync(StartupTimeout, cancellationToken);

        Api = _application.CreateHttpClient("api");
        Api.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync(cancellationToken));
    }

    /// <summary>
    /// Токен администратора из начальных данных
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<string> GetTokenAsync(CancellationToken cancellationToken)
    {
        using var response = await Api.PostAsJsonAsync("api/auth/login",
            new { username = "admin", password = "Vet2026!" }, cancellationToken);
        response.EnsureSuccessStatusCode();
        var token = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken);
        return token!.AccessToken;
    }

    /// <summary>
    /// Клиент без токена, чтобы проверить отказ для неавторизованных запросов
    /// </summary>
    public HttpClient CreateAnonymousClient() => _application.CreateHttpClient("api");

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        Api?.Dispose();
        await _application.DisposeAsync();
    }

    /// <summary>
    /// Ответ входа
    /// </summary>
    /// <param name="AccessToken">Токен доступа</param>
    private sealed record LoginResponse(string AccessToken);
}
