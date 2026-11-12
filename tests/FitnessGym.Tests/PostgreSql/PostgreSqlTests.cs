using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Nodes;
using FitnessGym.Api.Controllers;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Exceptions;
using FitnessGym.Domain.Seeds;
using FitnessGym.Infrastructure.PostgreSql.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessGym.Tests.PostgreSql;

/// <summary>
/// Проверки настоящего PostgreSQL: миграции, SQL-запросы, ограничения и HTTP API.
/// </summary>
public class PostgreSqlTests
{
    private static readonly DateTime _seedTime = new(2026, 11, 8, 12, 0, 0);

    [Fact(SkipUnless = nameof(PostgreSqlDatabase.IsConfigured), SkipType = typeof(PostgreSqlDatabase),
        Skip = "Для PostgreSQL задайте FITNESSGYM_TEST_CONNECTION_STRING.")]
    public async Task MigrationsShouldSeedOnceAndSupportRollback()
    {
        await using var database = await PostgreSqlDatabase.CreateAsync();
        await using var context = database.CreateContext();
        var token = TestContext.Current.CancellationToken;
        Assert.False(context.Database.HasPendingModelChanges());
        Assert.Equal(2, (await context.Database.GetAppliedMigrationsAsync(token)).Count());
        await context.Database.MigrateAsync(token);
        Assert.Equal(10, await context.Specializations.CountAsync(token));
        Assert.Equal(12, await context.Clients.CountAsync(token));
        Assert.Equal(12, await context.Trainers.CountAsync(token));
        Assert.Equal(26, await context.Sessions.CountAsync(token));
        Assert.NotNull(await context.Clients.SingleOrDefaultAsync(client => client.Id == 0, token));

        var migrator = context.GetService<IMigrator>();
        var schemaMigration = context.Database.GetMigrations().First();
        await migrator.MigrateAsync(schemaMigration, token);
        Assert.Empty(await context.Sessions.ToListAsync(token));
        Assert.Empty(await context.Clients.ToListAsync(token));
        await migrator.MigrateAsync("0", token);
        await context.Database.MigrateAsync(token);
        Assert.Equal(26, await context.Sessions.CountAsync(token));
    }

    [Theory(SkipUnless = nameof(PostgreSqlDatabase.IsConfigured), SkipType = typeof(PostgreSqlDatabase),
        Skip = "Для PostgreSQL задайте FITNESSGYM_TEST_CONNECTION_STRING.")]
    [InlineData("clients", "fullName")]
    [InlineData("trainers", "fullName")]
    [InlineData("specializations", "name")]
    [InlineData("sessions", "hallName")]
    public async Task HttpCrudShouldPersistAcrossApiRestarts(string resource, string field)
    {
        await using var database = await PostgreSqlDatabase.CreateAsync();
        var token = TestContext.Current.CancellationToken;
        var payload = ApiTests.CreatePayload(resource);
        int id;
        await using (var factory = CreateFactory(database))
        {
            using var client = factory.CreateClient();
            var response = await client.PostAsJsonAsync($"/api/{resource}", payload, token);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var created = (await response.Content.ReadFromJsonAsync<JsonObject>(token))!;
            id = created["id"]!.GetValue<int>();
            Assert.True(id >= 100);
            Assert.NotNull(response.Headers.Location);
            Assert.Equal(HttpStatusCode.OK, (await client.GetAsync(response.Headers.Location, token)).StatusCode);
        }

        await using (var factory = CreateFactory(database))
        {
            using var client = factory.CreateClient();
            var url = $"/api/{resource}/{id}";
            Assert.Equal(HttpStatusCode.OK, (await client.GetAsync(url, token)).StatusCode);
            payload[field] = "Сохранённое изменение";
            Assert.Equal(HttpStatusCode.OK, (await client.PutAsJsonAsync(url, payload, token)).StatusCode);
            var stored = (await client.GetFromJsonAsync<JsonObject>(url, token))!;
            Assert.Equal("Сохранённое изменение", stored[field]!.GetValue<string>());
            Assert.Equal(HttpStatusCode.NoContent, (await client.DeleteAsync(url, token)).StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, (await client.GetAsync(url, token)).StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, (await client.DeleteAsync(url, token)).StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, (await client.PutAsJsonAsync(url, payload, token)).StatusCode);
        }

        await using var context = database.CreateContext();
        Assert.Equal(12, await context.Clients.CountAsync(token));
        Assert.Equal(12, await context.Trainers.CountAsync(token));
        Assert.Equal(10, await context.Specializations.CountAsync(token));
        Assert.Equal(26, await context.Sessions.CountAsync(token));
    }

    [Fact(SkipUnless = nameof(PostgreSqlDatabase.IsConfigured), SkipType = typeof(PostgreSqlDatabase),
        Skip = "Для PostgreSQL задайте FITNESSGYM_TEST_CONNECTION_STRING.")]
    public async Task DetachedNavigationObjectsShouldNotBeInsertedOrUpdated()
    {
        await using var database = await PostgreSqlDatabase.CreateAsync();
        var token = TestContext.Current.CancellationToken;
        var data = ClubDataSeeder.Create(_seedTime);
        var trainer = data.Trainers[0];
        trainer.SpecializationId = 1;
        trainer.Specialization = new Specialization { Id = 1, Name = "Не записывать" };
        await using (var context = database.CreateContext())
        {
            var repository = new PostgreSqlTrainerRepository(context);
            Assert.True(await repository.UpdateAsync(0, trainer));
            var added = await repository.AddAsync(trainer);
            Assert.True(added.Id >= 100);
        }

        await using (var context = database.CreateContext())
        {
            var repository = new PostgreSqlTrainingSessionRepository(context);
            var session = data.Sessions[0];
            session.ClientId = 1;
            session.TrainerId = 1;
            session.Client.FullName = "Не записывать";
            session.Trainer.FullName = "Не записывать";
            Assert.True(await repository.UpdateAsync(0, session));
            var added = await repository.AddAsync(session);
            Assert.True(added.Id >= 100);
        }

        await using (var context = database.CreateContext())
        {
            var stored = await new PostgreSqlTrainingSessionRepository(context).GetByIdAsync(0);
            Assert.Equal(1, stored!.Client.Id);
            Assert.Equal(1, stored.Trainer.Id);
            Assert.NotNull(stored.Trainer.Specialization);
            Assert.Equal(12, await context.Clients.CountAsync(token));
            Assert.Equal(13, await context.Trainers.CountAsync(token));
            Assert.Equal("Пилатес", (await context.Specializations.SingleAsync(item => item.Id == 1, token)).Name);
            Assert.DoesNotContain(await context.Clients.ToListAsync(token), item => item.FullName == "Не записывать");
            Assert.DoesNotContain(await context.Trainers.ToListAsync(token), item => item.FullName == "Не записывать");
            var updatedTrainer = await new PostgreSqlTrainerRepository(context).GetByIdAsync(0);
            Assert.Equal(1, updatedTrainer!.SpecializationId);
            Assert.Equal("Пилатес", updatedTrainer.Specialization.Name);
        }
    }

    [Fact(SkipUnless = nameof(PostgreSqlDatabase.IsConfigured), SkipType = typeof(PostgreSqlDatabase),
        Skip = "Для PostgreSQL задайте FITNESSGYM_TEST_CONNECTION_STRING.")]
    public async Task AnalyticsShouldExecuteInPostgreSqlAndRespectTimeBoundaries()
    {
        await using var database = await PostgreSqlDatabase.CreateAsync();
        await using var context = database.CreateContext();
        var clients = new PostgreSqlClientRepository(context);
        var trainers = new PostgreSqlTrainerRepository(context);
        var sessions = new PostgreSqlTrainingSessionRepository(context);
        var data = ClubDataSeeder.Create(_seedTime);
        Assert.Equal(data.Trainers.Where(item => item.ExperienceYears >= 5).Select(item => item.Id),
            (await trainers.GetExperiencedAsync(5)).Select(item => item.Id));
        Assert.Equal(new[] { 0, 1, 2, 3, 4 },
            (await clients.GetExpiredAsync(DateOnly.FromDateTime(_seedTime))).Select(item => item.Id));
        Assert.Equal(new[] { 0, 1, 2 },
            (await sessions.GetForMonthAsync("Силовой зал", _seedTime)).Select(item => item.Id));
        Assert.Equal(new[] { 7, 4, 10, 1, 6 },
            (await sessions.GetPopularTrainersAsync(5)).Select(item => item.Id));
        Assert.All(await sessions.GetPopularTrainersAsync(5), trainer => Assert.NotNull(trainer.Specialization));
        Assert.Equal(5, (await sessions.GetHallsAsync()).Count);
        Assert.False(await sessions.IsHallAvailableAsync("Зал групповых занятий", _seedTime.AddMinutes(-30)));
        Assert.False(await sessions.IsHallAvailableAsync("Зал групповых занятий", _seedTime));
        Assert.True(await sessions.IsHallAvailableAsync("Зал групповых занятий", _seedTime.AddMinutes(30)));
        Assert.True(await sessions.IsHallAvailableAsync("Силовой зал", new DateTime(2026, 11, 5, 10, 0, 0)));

        await sessions.AddAsync(new TrainingSession
        {
            ClientId = 0, TrainerId = 0, StartsAt = new DateTime(2026, 12, 1),
            HallName = "Силовой зал", IsTrial = false,
        });
        Assert.Equal(3, (await sessions.GetForMonthAsync("Силовой зал", _seedTime)).Count);
        Assert.Single(await sessions.GetForMonthAsync("Силовой зал", new DateTime(2026, 12, 1)),
            session => session.StartsAt == new DateTime(2026, 12, 1));
    }

    [Fact(SkipUnless = nameof(PostgreSqlDatabase.IsConfigured), SkipType = typeof(PostgreSqlDatabase),
        Skip = "Для PostgreSQL задайте FITNESSGYM_TEST_CONNECTION_STRING.")]
    public async Task ForeignKeysShouldProtectReferencesEvenWithoutServiceChecks()
    {
        await using var database = await PostgreSqlDatabase.CreateAsync();
        await using (var context = database.CreateContext())
        {
            await Assert.ThrowsAsync<EntityReferenceException>(() => new PostgreSqlClientRepository(context).DeleteAsync(0));
        }
        await using (var context = database.CreateContext())
        {
            await Assert.ThrowsAsync<EntityReferenceException>(() => new PostgreSqlTrainerRepository(context).DeleteAsync(0));
        }
        await using (var context = database.CreateContext())
        {
            await Assert.ThrowsAsync<EntityReferenceException>(() => new PostgreSqlSpecializationRepository(context).DeleteAsync(0));
        }
        await using (var context = database.CreateContext())
        {
            await Assert.ThrowsAsync<EntityReferenceException>(() => new PostgreSqlTrainingSessionRepository(context)
                .AddAsync(new TrainingSession
                {
                    ClientId = 999999, TrainerId = 0, StartsAt = _seedTime,
                    HallName = "Зал", IsTrial = false,
                }));
        }
    }

    [Fact(SkipUnless = nameof(PostgreSqlDatabase.IsConfigured), SkipType = typeof(PostgreSqlDatabase),
        Skip = "Для PostgreSQL задайте FITNESSGYM_TEST_CONNECTION_STRING.")]
    public async Task CheckConstraintFailuresShouldNotBeMisclassifiedAsReferenceConflicts()
    {
        await using var database = await PostgreSqlDatabase.CreateAsync();
        await using var context = database.CreateContext();
        await Assert.ThrowsAsync<DbUpdateException>(() => new PostgreSqlTrainingSessionRepository(context)
            .AddAsync(new TrainingSession
            {
                ClientId = 0, TrainerId = 0, StartsAt = _seedTime, EndsAt = _seedTime,
                HallName = "Зал", IsTrial = false,
            }));
    }

    [Fact(SkipUnless = nameof(PostgreSqlDatabase.IsConfigured), SkipType = typeof(PostgreSqlDatabase),
        Skip = "Для PostgreSQL задайте FITNESSGYM_TEST_CONNECTION_STRING.")]
    public async Task HttpQueriesAndValidationShouldUseDatabaseRepositories()
    {
        await using var database = await PostgreSqlDatabase.CreateAsync();
        await using var factory = CreateFactory(database);
        using var client = factory.CreateClient();
        var token = TestContext.Current.CancellationToken;
        string[] urls = ["/api/clients/expired", "/api/trainers/experienced", "/api/trainers/popular",
            "/api/halls", "/api/sessions/current-month?hallName=Силовой%20зал",
            "/api/halls/Силовой%20зал/availability?at=2026-11-05T10:00:00", "/health"];
        foreach (var url in urls)
        {
            Assert.Equal(HttpStatusCode.OK, (await client.GetAsync(url, token)).StatusCode);
        }
        foreach (var resource in new[] { "clients", "trainers", "specializations" })
        {
            Assert.Equal(HttpStatusCode.Conflict, (await client.DeleteAsync($"/api/{resource}/0", token)).StatusCode);
        }
        var payload = ApiTests.CreatePayload("sessions");
        payload["clientId"] = 999999;
        Assert.Equal(HttpStatusCode.BadRequest, (await client.PostAsJsonAsync("/api/sessions", payload, token)).StatusCode);
        payload["clientId"] = 0;
        payload["startsAt"] = "2026-11-01T10:00:00Z";
        Assert.Equal(HttpStatusCode.BadRequest, (await client.PostAsJsonAsync("/api/sessions", payload, token)).StatusCode);
        Assert.Equal(HttpStatusCode.BadRequest,
            (await client.GetAsync("/api/halls/Test/availability?at=2026-11-01T10:00:00Z", token)).StatusCode);
    }

    private static WebApplicationFactory<ClientsController> CreateFactory(PostgreSqlDatabase database) =>
        new WebApplicationFactory<ClientsController>().WithWebHostBuilder(builder =>
        {
            builder.UseSetting("ConnectionStrings:fitnessgym", database.ConnectionString);
            builder.UseSetting("Logging:LogLevel:Default", "Error");
            builder.ConfigureServices(services => services.AddSingleton<Func<DateTime>>(() => _seedTime));
        });
}
