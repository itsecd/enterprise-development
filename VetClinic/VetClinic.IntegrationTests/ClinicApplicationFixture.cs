using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Aspire.Hosting;
using Aspire.Hosting.Testing;

namespace VetClinic.IntegrationTests;

/// <summary>
/// Поднимает приложение через Aspire с настоящим SQL Server на время всего класса тестов
/// </summary>
public sealed class ClinicApplicationFixture : IAsyncLifetime
{
    private static readonly TimeSpan StartupTimeout = TimeSpan.FromMinutes(8);

    private DistributedApplication? _application;
    private HttpClient? _api;

    /// <summary>
    /// Клиент API, готовый к запросам после запуска приложения
    /// </summary>
    public HttpClient Api => _api ?? throw new InvalidOperationException("Приложение не запущено");

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
        var application = await builder.BuildAsync();
        _application = application;
        await application.StartAsync(cancellationToken);

        await application.ResourceNotifications.WaitForResourceHealthyAsync("api", cancellationToken)
            .WaitAsync(StartupTimeout, cancellationToken);

        _api = application.CreateHttpClient("api", "http");
        _api.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync(cancellationToken));
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
        var token = await response.Content.ReadFromJsonAsync<LoginResponse>(cancellationToken)
            ?? throw new InvalidOperationException("Ответ входа не содержит токен");
        return token.AccessToken;
    }

    /// <summary>
    /// Клиент без токена, чтобы проверить отказ для неавторизованных запросов
    /// </summary>
    public HttpClient CreateAnonymousClient() => Application.CreateHttpClient("api", "http");

    /// <summary>
    /// Запущенное приложение
    /// </summary>
    private DistributedApplication Application =>
        _application ?? throw new InvalidOperationException("Приложение не запущено");

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        _api?.Dispose();
        if (_application is not null)
        {
            await _application.DisposeAsync();
        }
    }

    /// <summary>
    /// Ответ входа
    /// </summary>
    /// <param name="AccessToken">Токен доступа</param>
    private sealed record LoginResponse(string AccessToken);
}
