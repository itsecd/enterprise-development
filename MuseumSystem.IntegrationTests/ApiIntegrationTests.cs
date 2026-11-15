using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using MuseumSystem.Contracts.Models;
using Xunit;

namespace MuseumSystem.IntegrationTests;

public class ApiIntegrationTests(AppHostFixture fixture) : IClassFixture<AppHostFixture>
{
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
}