using System.Net;
using System.Text.Json;

namespace VetClinic.IntegrationTests;

/// <summary>
/// Проверки опубликованной документации OpenAPI
/// </summary>
public class DocumentationTests(ClinicApplicationFixture fixture) : IClassFixture<ClinicApplicationFixture>
{
    private static readonly string[] Resources = ["owners", "pets", "vets", "appointments", "breeds", "specializations"];

    private readonly ClinicApplicationFixture _fixture = fixture;

    /// <summary>
    /// В документе есть описания операций, коды ответов и схемы запросов
    /// </summary>
    [Fact]
    public async Task OpenApi_ContainsDescriptionsAndResponseCodes()
    {
        using var response = await _fixture.Api.GetAsync("openapi/v1.json", TestContext.Current.CancellationToken);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        using var document = JsonDocument.Parse(await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken));
        var root = document.RootElement;
        Assert.StartsWith("3.", root.GetProperty("openapi").GetString());
        Assert.False(string.IsNullOrWhiteSpace(root.GetProperty("info").GetProperty("title").GetString()));

        var paths = root.GetProperty("paths");
        foreach (var resource in Resources)
        {
            var collection = paths.GetProperty($"/api/{resource}");
            var item = paths.GetProperty($"/api/{resource}/{{id}}");

            Assert.False(string.IsNullOrWhiteSpace(collection.GetProperty("get").GetProperty("summary").GetString()));
            Assert.True(collection.GetProperty("post").GetProperty("responses").TryGetProperty("201", out _));
            Assert.True(collection.GetProperty("post").GetProperty("responses").TryGetProperty("400", out _));
            Assert.True(item.GetProperty("get").GetProperty("responses").TryGetProperty("404", out _));
            Assert.True(item.GetProperty("put").GetProperty("responses").TryGetProperty("409", out _));
        }
    }
}
