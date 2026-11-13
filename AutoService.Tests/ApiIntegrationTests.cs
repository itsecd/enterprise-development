using System.Net.Http.Json;
using AutoService.Domain.Models;
using Aspire.Hosting.Testing;

namespace AutoService.Tests;

public class ApiIntegrationTests(ITestOutputHelper output)
{
    private static readonly TimeSpan _defaultTimeout = TimeSpan.FromSeconds(30);

    /// <summary>
    /// Тест: проверка получения списка машин через API.
    /// </summary>
    [Fact]
    public async Task GetAllCars_WhenCalled_ReturnsSuccessAndData()
    {
        var cts = new CancellationTokenSource(_defaultTimeout);

        output.WriteLine("Запуск оркестратора Aspire для теста машин...");

        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.AutoService_AppHost>(cts.Token);
        await using var app = await appHost.BuildAsync(cts.Token);
        await app.StartAsync(cts.Token);

        var httpClient = app.CreateHttpClient("autoservice-api", "http");

        output.WriteLine("Отправка запроса к API...");
        var response = await httpClient.GetAsync("/api/cars", cts.Token);

        response.EnsureSuccessStatusCode();
        var cars = await response.Content.ReadFromJsonAsync<List<Car>>(cts.Token);

        output.WriteLine($"Получено машин: {cars?.Count ?? 0}");

        Assert.NotNull(cars);
        Assert.NotEmpty(cars);
    }
    /// <summary>
    /// Тест: проверка аналитического запроса (топ-5 работ).
    /// </summary>
    [Fact]
    public async Task GetTop5Works_WhenCalled_ReturnsSuccess()
    {
        var cts = new CancellationTokenSource(_defaultTimeout);

        output.WriteLine("Запуск оркестратора Aspire для теста аналитики...");

        var appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.AutoService_AppHost>(cts.Token);
        await using var app = await appHost.BuildAsync(cts.Token);
        await app.StartAsync(cts.Token);

        var httpClient = app.CreateHttpClient("autoservice-api", "http");

        output.WriteLine("Отправка запроса к Reports API...");
        var response = await httpClient.GetAsync("/api/reports/top-5-works", cts.Token);

        response.EnsureSuccessStatusCode();
        Assert.True(true);
    }
}