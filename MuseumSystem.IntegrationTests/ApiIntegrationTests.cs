using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using MuseumSystem.Contracts.Models;
using MuseumSystem.Shared.Enums;
using Xunit;

namespace MuseumSystem.IntegrationTests;

public class ApiIntegrationTests(AppHostFixture fixture) : IClassFixture<AppHostFixture>
{

    // Вспомогательный метод для логина
    private async Task<HttpClient> GetAuthenticatedClientAsync()
    {
        var ct = TestContext.Current.CancellationToken;
        var loginRequest = new LoginRequest { Username = "admin", Password = "password" };
        var loginResponse = await fixture.HttpClient.PostAsJsonAsync("/api/auth/login", loginRequest, ct);
        loginResponse.EnsureSuccessStatusCode();

        var loginData = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>(ct);
        Assert.NotNull(loginData);

        fixture.HttpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", loginData.Token);

        return fixture.HttpClient;
    }

    [Fact]
    public async Task Exhibitions_Endpoint_ReturnsSuccess_And_Seeded_Data()
    {
        var ct = TestContext.Current.CancellationToken;

        // Логинимся
        var loginRequest = new LoginRequest { Username = "admin", Password = "password" };
        var loginResponse = await fixture.HttpClient.PostAsJsonAsync("/api/auth/login", loginRequest, ct);
        loginResponse.EnsureSuccessStatusCode();

        var loginData = await loginResponse.Content.ReadFromJsonAsync<LoginResponse>(ct);
        Assert.NotNull(loginData);

        // Ставим токен
        fixture.HttpClient.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", loginData.Token);

        // Идем за выставками
        using var response = await fixture.HttpClient.GetAsync("/api/exhibitions", ct);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var exhibitions = await response.Content.ReadFromJsonAsync<List<ExhibitionResponse>>(ct);
        Assert.NotNull(exhibitions);
        Assert.NotEmpty(exhibitions); 
    }

    [Fact]
    public async Task Auth_Login_With_Valid_Credentials_Returns_Token()
    {
        var ct = TestContext.Current.CancellationToken;

        var loginRequest = new LoginRequest { Username = "admin", Password = "password" };
        using var response = await fixture.HttpClient.PostAsJsonAsync("/api/auth/login", loginRequest, ct);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>(ct);
        Assert.NotNull(loginResponse);
        Assert.False(string.IsNullOrWhiteSpace(loginResponse.Token));
    }

    [Fact]
    public async Task Exhibitions_FullCrud_WorksCorrectly()
    {
        var client = await GetAuthenticatedClientAsync();
        var ct = TestContext.Current.CancellationToken;

        // Создаем выставку
        var createReq = new ExhibitionCreateRequest
        {
            Name = "Test Exhibition",
            Theme = ExhibitionTheme.Art,
            HallNumber = 1,
            StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
            EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(1))
        };
        var createRes = await client.PostAsJsonAsync("/api/exhibitions", createReq, ct);
        createRes.EnsureSuccessStatusCode();
        var created = await createRes.Content.ReadFromJsonAsync<ExhibitionResponse>(ct);
        Assert.NotNull(created);

        // Читаем созданную выставку
        var getRes = await client.GetAsync($"/api/exhibitions/{created.Id}", ct);
        Assert.Equal(HttpStatusCode.OK, getRes.StatusCode);

        // Обновляем выставку
        var updateReq = new ExhibitionUpdateRequest
        {
            Name = "Updated Exhibition",
            Theme = ExhibitionTheme.Art,
            HallNumber = 2,
            StartDate = DateOnly.FromDateTime(DateTime.UtcNow),
            EndDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2))
        };
        var updateRes = await client.PutAsJsonAsync($"/api/exhibitions/{created.Id}", updateReq, ct);
        Assert.Equal(HttpStatusCode.NoContent, updateRes.StatusCode);

        // Удаляем выставку
        var deleteRes = await client.DeleteAsync($"/api/exhibitions/{created.Id}", ct);
        Assert.Equal(HttpStatusCode.NoContent, deleteRes.StatusCode);

        // Проверяем, что удаление действительно произошло
        var getAfterDelete = await client.GetAsync($"/api/exhibitions/{created.Id}", ct);
        Assert.Equal(HttpStatusCode.NotFound, getAfterDelete.StatusCode);
    }

    [Fact]
    public async Task Visitors_FullCrud_WorksCorrectly()
    {
        var client = await GetAuthenticatedClientAsync();
        var ct = TestContext.Current.CancellationToken;

        // Создаем посетителя
        var createReq = new VisitorCreateRequest
        {
            FullName = "Иванов Иван Иванович",
            Phone = "+79999999999",
            BirthDate = DateOnly.FromDateTime(new DateTime(1990, 1, 1))
        };
        var createRes = await client.PostAsJsonAsync("/api/visitors", createReq, ct);
        createRes.EnsureSuccessStatusCode();
        var created = await createRes.Content.ReadFromJsonAsync<VisitorResponse>(ct);
        Assert.NotNull(created);

        // Читаем созданного посетителя
        var getRes = await client.GetAsync($"/api/visitors/{created.Id}", ct);
        Assert.Equal(HttpStatusCode.OK, getRes.StatusCode);

        // Обновляем посетителя
        var updateReq = new VisitorUpdateRequest
        {
            FullName = "Петров Петр Петрович",
            Phone = "+78888888888",
            BirthDate = DateOnly.FromDateTime(new DateTime(1995, 5, 5))
        };
        var updateRes = await client.PutAsJsonAsync($"/api/visitors/{created.Id}", updateReq, ct);
        Assert.Equal(HttpStatusCode.NoContent, updateRes.StatusCode);

        // Удаляем посетителя
        var deleteRes = await client.DeleteAsync($"/api/visitors/{created.Id}", ct);
        Assert.Equal(HttpStatusCode.NoContent, deleteRes.StatusCode);

        // Проверяем, что удаление произошло
        var getAfterDelete = await client.GetAsync($"/api/visitors/{created.Id}", ct);
        Assert.Equal(HttpStatusCode.NotFound, getAfterDelete.StatusCode);
    }

    [Theory]
    [InlineData("/api/excursions")]
    [InlineData("/api/tickets")]
    [InlineData("/api/analytics/top-exhibitions")]
    [InlineData("/api/analytics/minimal-participants")]
    public async Task GetEndpoints_ReturnSuccess(string url)
    {
        var client = await GetAuthenticatedClientAsync();
        var ct = TestContext.Current.CancellationToken;

        var response = await client.GetAsync(url, ct);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}