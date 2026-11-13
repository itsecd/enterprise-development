using System.Net;
using System.Net.Http.Json;
using Hotel.Contracts.DTO;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace Hotel.Tests;

/// <summary>
/// Тесты проверяющие API
/// </summary>
public class ApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    /// <summary>
    /// Топ 5 клиентов по стоимости проживания
    /// </summary>
    [Fact]
    public async Task GetTop5Clients_ReturnsOkAndFiveItems()
    {
        var response = await _client.GetAsync("/api/analytics/top5-clients", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<List<RepeatVisitDto>>(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.Equal(5, result.Count);
    }

    /// <summary>
    /// Количество бронирований для каждого номера
    /// </summary>
    [Fact]
    public async Task GetBookingsCount_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/analytics/bookings-count", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    /// <summary>
    /// Номера, занятые в указанную дату
    /// </summary>
    [Fact]
    public async Task GetBookedRooms_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/analytics/booked-rooms?date=2019-04-13", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<List<int>>(cancellationToken: TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    /// <summary>
    /// Тест CRUD
    /// </summary>
    [Fact]
    public async Task GetClients_ReturnsOk()
    {
        var response = await _client.GetAsync("/api/clients", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    /// <summary>
    /// Обработчик ошибок
    /// </summary>
    [Fact]
    public async Task GetNonExistentClient_ReturnsNotFound()
    {
        var response = await _client.GetAsync("/api/clients/1000", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}