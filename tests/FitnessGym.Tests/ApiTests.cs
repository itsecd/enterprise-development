using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using FitnessGym.Api.Controllers;
using FitnessGym.Contracts.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace FitnessGym.Tests;

/// <summary>
/// Проверки API через HTTP: CRUD, валидация, ошибки и документация.
/// </summary>
public class ApiTests(InMemoryApiFactory factory)
    : IClassFixture<InMemoryApiFactory>
{
    private static readonly Dictionary<string, string> _payloads = new()
    {
        ["clients"] = """
            {"passportNumber":"4599 999999","fullName":"Тестовый клиент","gender":"Male",
             "birthDate":"2000-01-01","phone":"+79119999999",
             "subscriptionStart":"2026-01-01","subscriptionEnd":"2026-12-31"}
            """,
        ["trainers"] = """
            {"passportNumber":"4699 999999","fullName":"Тестовый тренер","gender":"Female",
             "birthDate":"1995-05-05","specializationId":0,"experienceYears":5}
            """,
        ["specializations"] = """{"name":"Тестовая специализация"}""",
        ["sessions"] = """
            {"clientId":0,"trainerId":0,"startsAt":"2026-11-01T10:00:00",
             "endsAt":"2026-11-01T11:00:00","hallName":"Тестовый зал","isTrial":false}
            """,
    };

    internal static JsonObject CreatePayload(string resource) => JsonNode.Parse(_payloads[resource])!.AsObject();

    /// <summary>
    /// Идентификатор формируется сервером, Location доступен, PUT возвращает обновлённый DTO.
    /// </summary>
    [Theory]
    [InlineData("clients", "fullName")]
    [InlineData("trainers", "fullName")]
    [InlineData("specializations", "name")]
    [InlineData("sessions", "hallName")]
    public async Task CrudShouldReturnExpectedStatusesAndPreserveIdentifier(string resource, string changedField)
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        using var client = factory.CreateClient();
        var payload = JsonNode.Parse(_payloads[resource])!.AsObject();
        var created = await client.PostAsJsonAsync($"/api/{resource}", payload, cancellationToken);
        Assert.Equal(HttpStatusCode.Created, created.StatusCode);
        var dto = (await created.Content.ReadFromJsonAsync<JsonObject>(cancellationToken))!;
        var id = dto["id"]!.GetValue<int>();
        Assert.True(id >= 10);
        Assert.NotNull(created.Headers.Location);
        var fetched = await client.GetFromJsonAsync<JsonObject>(created.Headers.Location, cancellationToken);
        Assert.Equal(id, fetched!["id"]!.GetValue<int>());

        payload[changedField] = "Обновлённое значение";
        var updated = await client.PutAsJsonAsync($"/api/{resource}/{id}", payload, cancellationToken);
        Assert.Equal(HttpStatusCode.OK, updated.StatusCode);
        var updatedDto = (await updated.Content.ReadFromJsonAsync<JsonObject>(cancellationToken))!;
        Assert.Equal(id, updatedDto["id"]!.GetValue<int>());
        Assert.Equal("Обновлённое значение", updatedDto[changedField]!.GetValue<string>());
        fetched = await client.GetFromJsonAsync<JsonObject>($"/api/{resource}/{id}", cancellationToken);
        Assert.Equal("Обновлённое значение", fetched![changedField]!.GetValue<string>());

        Assert.Equal(HttpStatusCode.NoContent, (await client.DeleteAsync($"/api/{resource}/{id}", cancellationToken)).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync($"/api/{resource}/{id}", cancellationToken)).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound, (await client.DeleteAsync($"/api/{resource}/{id}", cancellationToken)).StatusCode);
        Assert.Equal(HttpStatusCode.NotFound,
            (await client.PutAsJsonAsync($"/api/{resource}/{id}", payload, cancellationToken)).StatusCode);
    }

    /// <summary>
    /// Отсутствующие даты, пол и внешние ключи не подменяются значениями по умолчанию.
    /// </summary>
    [Theory]
    [InlineData("clients", "birthDate")]
    [InlineData("clients", "gender")]
    [InlineData("clients", "subscriptionStart")]
    [InlineData("clients", "subscriptionEnd")]
    [InlineData("clients", "phone")]
    [InlineData("trainers", "birthDate")]
    [InlineData("trainers", "gender")]
    [InlineData("trainers", "specializationId")]
    [InlineData("sessions", "clientId")]
    [InlineData("sessions", "trainerId")]
    [InlineData("sessions", "startsAt")]
    public async Task MissingRequiredFieldsShouldReturnValidationProblem(string resource, string field)
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        using var client = factory.CreateClient();
        var payload = JsonNode.Parse(_payloads[resource])!.AsObject();
        payload.Remove(field);
        var response = await client.PostAsJsonAsync($"/api/{resource}", payload, cancellationToken);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>(cancellationToken);
        Assert.NotEmpty(problem!.Errors);
        Assert.Equal(HttpStatusCode.BadRequest,
            (await client.PutAsJsonAsync($"/api/{resource}/0", payload, cancellationToken)).StatusCode);
    }

    /// <summary>
    /// Проверяются интервалы дат, диапазоны, строки и ссылки на существующие сущности.
    /// </summary>
    [Theory]
    [InlineData("clients", "subscriptionEnd", "\"2025-12-31\"")]
    [InlineData("clients", "phone", "\"invalid\"")]
    [InlineData("clients", "fullName", "\"   \"")]
    [InlineData("trainers", "gender", "99")]
    [InlineData("trainers", "experienceYears", "-1")]
    [InlineData("trainers", "specializationId", "999999")]
    [InlineData("sessions", "endsAt", "\"2026-11-01T09:00:00\"")]
    [InlineData("sessions", "endsAt", "\"2026-11-01T10:00:00\"")]
    [InlineData("sessions", "startsAt", "\"2026-11-01T10:00:00Z\"")]
    [InlineData("sessions", "endsAt", "\"2026-11-01T11:00:00Z\"")]
    [InlineData("sessions", "startsAt", "\"2026-11-01T10:00:00+04:00\"")]
    [InlineData("sessions", "endsAt", "\"2026-11-01T11:00:00+04:00\"")]
    [InlineData("sessions", "clientId", "-1")]
    [InlineData("sessions", "clientId", "999999")]
    [InlineData("sessions", "trainerId", "999999")]
    [InlineData("specializations", "name", "\"X\"")]
    public async Task InvalidInputShouldNotChangeStoredData(string resource, string field, string value)
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        using var client = factory.CreateClient();
        var before = await client.GetStringAsync($"/api/{resource}", cancellationToken);
        var payload = JsonNode.Parse(_payloads[resource])!.AsObject();
        payload[field] = JsonNode.Parse(value);
        var response = await client.PostAsJsonAsync($"/api/{resource}", payload, cancellationToken);
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        var problem = await response.Content.ReadFromJsonAsync<ValidationProblemDetails>(cancellationToken);
        Assert.NotEmpty(problem!.Errors);
        Assert.Equal(HttpStatusCode.BadRequest,
            (await client.PutAsJsonAsync($"/api/{resource}/0", payload, cancellationToken)).StatusCode);
        Assert.Equal(before, await client.GetStringAsync($"/api/{resource}", cancellationToken));
    }

    /// <summary>
    /// Связанные записи нельзя удалить, пока на них ссылаются другие сущности.
    /// </summary>
    [Theory]
    [InlineData("clients")]
    [InlineData("trainers")]
    [InlineData("specializations")]
    public async Task DeletingReferencedEntityShouldReturnConflict(string resource)
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        using var client = factory.CreateClient();
        Assert.Equal(HttpStatusCode.Conflict, (await client.DeleteAsync($"/api/{resource}/0", cancellationToken)).StatusCode);
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync($"/api/{resource}/0", cancellationToken)).StatusCode);
    }

    /// <summary>
    /// Некорректные параметры аналитических запросов отклоняются до обращения к данным.
    /// </summary>
    [Theory]
    [InlineData("/api/trainers/experienced?minYears=-1")]
    [InlineData("/api/trainers/popular?count=0")]
    [InlineData("/api/sessions/current-month")]
    [InlineData("/api/sessions/current-month?hallName=%20%20")]
    [InlineData("/api/halls/Test/availability?at=invalid")]
    [InlineData("/api/halls/Test/availability?at=2026-11-01T10:00:00Z")]
    [InlineData("/api/halls/Test/availability?at=2026-11-01T10:00:00%2B04:00")]
    public async Task InvalidQueryShouldReturnBadRequest(string url)
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        using var client = factory.CreateClient();
        Assert.Equal(HttpStatusCode.BadRequest, (await client.GetAsync(url, cancellationToken)).StatusCode);
    }

    /// <summary>
    /// Непредвиденная ошибка возвращает 500 без внутренних подробностей.
    /// </summary>
    [Fact]
    public async Task UnexpectedFailureShouldReturnSafeProblemDetails()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await using var failingFactory = factory.WithWebHostBuilder(builder => builder
            .UseEnvironment("Production")
            .ConfigureServices(services =>
            {
                services.RemoveAll<IClientService>();
                services.AddScoped<IClientService>(_ => throw new InvalidOperationException("private database details"));
            }));
        using var client = failingFactory.CreateClient();
        var response = await client.GetAsync("/api/clients", cancellationToken);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
        var body = await response.Content.ReadAsStringAsync(cancellationToken);
        Assert.DoesNotContain("private database details", body);
        Assert.DoesNotContain("InvalidOperationException", body);
        var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>(cancellationToken);
        Assert.Equal(500, problem!.Status);
        Assert.NotNull(problem.Extensions["traceId"]);
    }

    /// <summary>
    /// Scalar и OpenAPI доступны; схемы запросов не содержат серверных полей.
    /// </summary>
    [Fact]
    public async Task OpenApiShouldDescribeSeparateRequestSchemasAndResponseStatuses()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        using var client = factory.CreateClient();
        Assert.Contains("scalar", await client.GetStringAsync("/scalar/v1", cancellationToken), StringComparison.OrdinalIgnoreCase);
        var document = (await client.GetFromJsonAsync<JsonObject>("/openapi/v1.json", cancellationToken))!;
        var schemas = document["components"]!["schemas"]!.AsObject();
        string[] entities = ["Client", "Trainer", "Specialization", "TrainingSession"];
        foreach (var entity in entities)
        {
            var properties = schemas[$"Upsert{entity}Dto"]!["properties"]!.AsObject();
            Assert.False(properties.ContainsKey("id"));
            Assert.False(properties.ContainsKey("specializationName"));
            Assert.True(schemas[$"{entity}Dto"]!["properties"]!.AsObject().ContainsKey("id"));
        }

        foreach (var path in document["paths"]!.AsObject())
        {
            foreach (var operation in path.Value!.AsObject())
            {
                Assert.True(operation.Value!["responses"]!.AsObject().ContainsKey("500"));
            }
        }
    }

    /// <summary>
    /// Примеры и схемы времени соответствуют формату, который принимается API и хранится в PostgreSQL.
    /// </summary>
    [Fact]
    public async Task OpenApiShouldDescribeLocalTimestampsWithoutOffsets()
    {
        using var client = factory.CreateClient();
        var document = (await client.GetFromJsonAsync<JsonObject>("/openapi/v1.json",
            TestContext.Current.CancellationToken))!;
        var schemas = document["components"]!["schemas"]!.AsObject();
        foreach (var name in new[] { "UpsertTrainingSessionDto", "TrainingSessionDto" })
        {
            foreach (var field in new[] { "startsAt", "endsAt" })
            {
                AssertLocalTimeSchema(schemas[name]!["properties"]![field]!,
                    field == "endsAt" ? "2026-11-08T13:00:00" : "2026-11-08T12:00:00");
            }
        }

        var parameters = document["paths"]!["/api/halls/{hallName}/availability"]!["get"]!["parameters"]!.AsArray();
        var at = Assert.Single(parameters, parameter => parameter!["name"]!.GetValue<string>() == "at");
        AssertLocalTimeSchema(at!["schema"]!);
    }

    private static void AssertLocalTimeSchema(JsonNode schema, string example = "2026-11-08T12:00:00")
    {
        Assert.Null(schema["format"]);
        var pattern = schema["pattern"]!.GetValue<string>();
        Assert.Matches(pattern, "2026-11-08T12:00:00");
        Assert.Matches(pattern, "2026-11-08T12:00:00.123456");
        Assert.DoesNotMatch(pattern, "2026-11-08T12:00:00Z");
        Assert.DoesNotMatch(pattern, "2026-11-08T12:00:00+04:00");
        Assert.Equal(example, schema["examples"]![0]!.GetValue<string>());
    }
}
