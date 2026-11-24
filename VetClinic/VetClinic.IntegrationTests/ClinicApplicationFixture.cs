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
    private static readonly TimeSpan StartupTimeout = TimeSpan.FromMinutes(8);

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

        await _application.ResourceNotifications.WaitForResourceAsync("api", KnownResourceStates.Running, cancellationToken)
            .WaitAsync(StartupTimeout, cancellationToken);

        Api = _application.CreateHttpClient("api", "http");
        await WaitForApiAsync(cancellationToken);
        Api.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync(cancellationToken));
    }

    /// <summary>
    /// Ждёт, пока API ответит на проверку работоспособности
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <remarks>
    /// Проверка идёт по http: на сборочном сервере сертификат разработчика не доверенный,
    /// поэтому проверка Aspire по https всегда остаётся нездоровой
    /// </remarks>
    private async Task WaitForApiAsync(CancellationToken cancellationToken)
    {
        var deadline = DateTime.UtcNow + StartupTimeout;
        while (DateTime.UtcNow < deadline)
        {
            try
            {
                using var response = await Api.GetAsync("health", cancellationToken);
                if (response.IsSuccessStatusCode)
                {
                    return;
                }
            }
            catch (HttpRequestException)
            {
            }

            await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        }

        throw new TimeoutException($"API не ответил на /health за {StartupTimeout.TotalMinutes} минут");
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
    public HttpClient CreateAnonymousClient() => _application.CreateHttpClient("api", "http");

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
