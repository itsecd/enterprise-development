using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using FitnessGym.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Tests.Integration;

/// <summary>
/// HTTP-запросы к отдельному процессу API, запущенному AppHost, с проверкой настоящей базы.
/// </summary>
[Collection("Aspire")]
[Trait("Category", "Integration")]
public class AspireApiTests(AspireFixture fixture)
{
    [Fact]
    public async Task AppHostShouldApplyMigrationsAndExposeHealthyApi()
    {
        var token = TestContext.Current.CancellationToken;
        using var client = fixture.CreateHttpClient();
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync("/health", token)).StatusCode);
        await using var context = fixture.CreateContext();
        Assert.False(context.Database.HasPendingModelChanges());
        Assert.Equal(3, (await context.Database.GetAppliedMigrationsAsync(token)).Count());
        Assert.Equal(10, await context.Specializations.CountAsync(token));
        Assert.Equal(12, await context.Clients.CountAsync(token));
        Assert.Equal(12, await context.Trainers.CountAsync(token));
        Assert.Equal(26, await context.Sessions.CountAsync(token));
    }

    [Theory]
    [InlineData("clients", "fullName")]
    [InlineData("trainers", "fullName")]
    [InlineData("specializations", "name")]
    [InlineData("sessions", "hallName")]
    public async Task CrudShouldPersistThroughAspireApi(string resource, string field)
    {
        var token = TestContext.Current.CancellationToken;
        using var client = fixture.CreateHttpClient();
        var payload = ApiTests.CreatePayload(resource);
        var response = await client.PostAsJsonAsync($"/api/{resource}", payload, token);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var created = (await response.Content.ReadFromJsonAsync<JsonObject>(token))!;
        var id = created["id"]!.GetValue<int>();
        Assert.True(id >= 100);
        var url = $"/api/{resource}/{id}";
        Assert.NotNull(response.Headers.Location);
        Assert.Equal(HttpStatusCode.OK, (await client.GetAsync(response.Headers.Location, token)).StatusCode);
        try
        {
            payload[field] = "Изменено через Aspire";
            Assert.Equal(HttpStatusCode.OK, (await client.PutAsJsonAsync(url, payload, token)).StatusCode);
            var updated = (await client.GetFromJsonAsync<JsonObject>(url, token))!;
            Assert.Equal("Изменено через Aspire", updated[field]!.GetValue<string>());

            // Отдельное соединение доказывает, что изменения сохранены в PostgreSQL.
            await using var context = fixture.CreateContext();
            var storedValue = resource switch
            {
                "clients" => (await context.Clients.SingleAsync(item => item.Id == id, token)).FullName,
                "trainers" => (await context.Trainers.SingleAsync(item => item.Id == id, token)).FullName,
                "specializations" => (await context.Specializations.SingleAsync(item => item.Id == id, token)).Name,
                "sessions" => (await context.Sessions.SingleAsync(item => item.Id == id, token)).HallName,
                _ => throw new ArgumentOutOfRangeException(nameof(resource)),
            };
            Assert.Equal("Изменено через Aspire", storedValue);
        }
        finally
        {
            Assert.Equal(HttpStatusCode.NoContent, (await client.DeleteAsync(url, token)).StatusCode);
        }
        Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync(url, token)).StatusCode);
    }

    [Theory]
    [InlineData("clients", "passportNumber")]
    [InlineData("trainers", "passportNumber")]
    [InlineData("specializations", "name")]
    public async Task DuplicateUniqueFieldsShouldReturnConflictWithoutChangingData(string resource, string field)
    {
        var token = TestContext.Current.CancellationToken;
        using var client = fixture.CreateHttpClient();
        var first = (await client.GetFromJsonAsync<JsonObject>($"/api/{resource}/0", token))!;
        var before = await client.GetStringAsync($"/api/{resource}", token);
        var payload = ApiTests.CreatePayload(resource);
        payload[field] = first[field]!.GetValue<string>();

        foreach (var response in new[]
        {
            await client.PostAsJsonAsync($"/api/{resource}", payload, token),
            await client.PutAsJsonAsync($"/api/{resource}/1", payload, token),
        })
        {
            using (response)
            {
                Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
                Assert.Equal("application/problem+json", response.Content.Headers.ContentType?.MediaType);
                var problem = (await response.Content.ReadFromJsonAsync<ProblemDetails>(token))!;
                Assert.Equal(409, problem.Status);
                Assert.DoesNotContain(payload[field]!.GetValue<string>(), problem.Detail!);
            }
        }
        Assert.Equal(before, await client.GetStringAsync($"/api/{resource}", token));
        // Собственное уникальное значение не конфликтует при повторном PUT.
        Assert.Equal(HttpStatusCode.OK, (await client.PutAsJsonAsync($"/api/{resource}/0", first, token)).StatusCode);
    }

    [Theory]
    [InlineData("clients", "passportNumber", TextLimits.PassportNumber)]
    [InlineData("clients", "fullName", TextLimits.FullName)]
    [InlineData("clients", "phone", TextLimits.Phone)]
    [InlineData("trainers", "passportNumber", TextLimits.PassportNumber)]
    [InlineData("trainers", "fullName", TextLimits.FullName)]
    [InlineData("specializations", "name", TextLimits.SpecializationName)]
    [InlineData("sessions", "hallName", TextLimits.HallName)]
    public async Task TextLengthLimitsShouldBeEnforcedOnPostAndPut(string resource, string field, int maxLength)
    {
        var token = TestContext.Current.CancellationToken;
        using var client = fixture.CreateHttpClient();
        var payload = ApiTests.CreatePayload(resource);
        payload[field] = new string('9', maxLength + 1);
        var before = await client.GetStringAsync($"/api/{resource}", token);
        Assert.Equal(HttpStatusCode.BadRequest,
            (await client.PostAsJsonAsync($"/api/{resource}", payload, token)).StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest,
            (await client.PutAsJsonAsync($"/api/{resource}/0", payload, token)).StatusCode);
        Assert.Equal(before, await client.GetStringAsync($"/api/{resource}", token));

        payload[field] = new string('9', maxLength);
        var created = await client.PostAsJsonAsync($"/api/{resource}", payload, token);
        Assert.Equal(HttpStatusCode.Created, created.StatusCode);
        var id = (await created.Content.ReadFromJsonAsync<JsonObject>(token))!["id"]!.GetValue<int>();
        try
        {
            Assert.Equal(HttpStatusCode.OK,
                (await client.PutAsJsonAsync($"/api/{resource}/{id}", payload, token)).StatusCode);
        }
        finally
        {
            Assert.Equal(HttpStatusCode.NoContent,
                (await client.DeleteAsync($"/api/{resource}/{id}", token)).StatusCode);
        }
    }
}
