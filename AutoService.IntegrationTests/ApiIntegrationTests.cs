using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using AutoService.Contracts.Dtos;
using Aspire.Hosting.Testing;

namespace AutoService.IntegrationTests;

/// <summary>
/// Интеграционные тесты HTTP-эндпоинтов: запускаются против поднятого
/// через Aspire приложения (см. <see cref="AspireAppFixture"/>).
/// </summary>
[Collection("Aspire integration")]
public class ApiIntegrationTests(AspireAppFixture fixture, ITestOutputHelper output)
{
    private static readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(120);

    /// <summary>
    /// Тест: проверка получения списка машин, защиты API и ограничений удаления.
    /// </summary>
    [Fact]
    public async Task GetAllCars_WhenCalled_ReturnsSuccessAndData()
    {
        using var cts = new CancellationTokenSource(_defaultTimeout);

        var httpClient = fixture.App.CreateHttpClient("autoservice-api", "http");

        var unauthorizedResponse = await httpClient.GetAsync("/api/cars", cts.Token);
        Assert.Equal(HttpStatusCode.Unauthorized, unauthorizedResponse.StatusCode);

        await AuthorizeAsync(httpClient, cts.Token);

        output.WriteLine("Отправка запроса к API...");
        var response = await httpClient.GetAsync("/api/cars", cts.Token);

        response.EnsureSuccessStatusCode();
        var cars = await response.Content.ReadFromJsonAsync<List<CarDto>>(cts.Token);

        output.WriteLine($"Получено машин: {cars?.Count ?? 0}");

        Assert.NotNull(cars);
        Assert.NotEmpty(cars);
        Assert.All(cars, car => Assert.NotEqual(Guid.Empty, car.ClientId));

        var deleteClientResponse = await httpClient.DeleteAsync($"/api/clients/{cars[0].ClientId}", cts.Token);
        Assert.Equal(HttpStatusCode.Conflict, deleteClientResponse.StatusCode);
    }

    /// <summary>
    /// Тест: проверка топ-5 работ через API.
    /// </summary>
    [Fact]
    public async Task GetTop5Works_WhenCalled_ReturnsSuccessAndData()
    {
        using var cts = new CancellationTokenSource(_defaultTimeout);

        var httpClient = fixture.App.CreateHttpClient("autoservice-api", "http");
        await AuthorizeAsync(httpClient, cts.Token);

        output.WriteLine("Отправка запроса к Reports API...");
        var response = await httpClient.GetAsync("/api/reports/top-5-works", cts.Token);

        response.EnsureSuccessStatusCode();
        var works = await response.Content.ReadFromJsonAsync<List<TopWorkReportDto>>(cts.Token);

        Assert.NotNull(works);
        Assert.Equal(5, works.Count);
        Assert.All(works, work =>
        {
            Assert.False(string.IsNullOrWhiteSpace(work.Name));
            Assert.True(work.Count > 0);
        });
    }

    private static async Task AuthorizeAsync(HttpClient httpClient, CancellationToken cancellationToken)
    {
        var response = await httpClient.PostAsJsonAsync("/api/auth/login", new LoginRequestDto
        {
            Username = "admin",
            Password = "Admin123!"
        }, cancellationToken);
        response.EnsureSuccessStatusCode();

        var login = await response.Content.ReadFromJsonAsync<LoginResponseDto>(cancellationToken);
        Assert.NotNull(login);
        Assert.False(string.IsNullOrWhiteSpace(login.Token));

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", login.Token);
    }
}
