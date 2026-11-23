using FitnessGym.Domain.Entities;
using FitnessGym.Tests.Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql;

namespace FitnessGym.Tests.PostgreSql;

[Collection("Aspire")]
[Trait("Category", "Integration")]
public class SchemaConstraintTests(AspireFixture fixture)
{
    [Theory]
    [InlineData("Clients", "PassportNumber", TextLimits.PassportNumber)]
    [InlineData("Clients", "FullName", TextLimits.FullName)]
    [InlineData("Clients", "Phone", TextLimits.Phone)]
    [InlineData("Trainers", "PassportNumber", TextLimits.PassportNumber)]
    [InlineData("Trainers", "FullName", TextLimits.FullName)]
    [InlineData("Specializations", "Name", TextLimits.SpecializationName)]
    [InlineData("TrainingSessions", "HallName", TextLimits.HallName)]
    public async Task DatabaseShouldEnforceTextLimitsWithoutDtoValidation(string table, string column, int maxLength)
    {
        await using var database = await PostgreSqlDatabase.CreateAsync(fixture.ConnectionString);
        await using var connection = new NpgsqlConnection(database.ConnectionString);
        var token = TestContext.Current.CancellationToken;
        await connection.OpenAsync(token);
        // Имена идентификаторов берутся только из InlineData, значение передаётся параметром.
        await using var command = new NpgsqlCommand(
            $"UPDATE \"{table}\" SET \"{column}\" = @value WHERE \"Id\" = 0", connection);
        var value = command.Parameters.AddWithValue("value", new string('9', maxLength));
        Assert.Equal(1, await command.ExecuteNonQueryAsync(token));
        value.Value = new string('9', maxLength + 1);
        var exception = await Assert.ThrowsAsync<PostgresException>(() => command.ExecuteNonQueryAsync(token));
        Assert.Equal(PostgresErrorCodes.StringDataRightTruncation, exception.SqlState);
    }

    [Theory]
    [InlineData("Clients", "PassportNumber")]
    [InlineData("Trainers", "PassportNumber")]
    [InlineData("Specializations", "Name")]
    public async Task DatabaseShouldRejectDuplicateUniqueValues(string table, string column)
    {
        await using var database = await PostgreSqlDatabase.CreateAsync(fixture.ConnectionString);
        await using var connection = new NpgsqlConnection(database.ConnectionString);
        var token = TestContext.Current.CancellationToken;
        await connection.OpenAsync(token);
        await using var command = new NpgsqlCommand(
            $"UPDATE \"{table}\" SET \"{column}\" = (SELECT \"{column}\" FROM \"{table}\" WHERE \"Id\" = 0) WHERE \"Id\" = 1",
            connection);
        var exception = await Assert.ThrowsAsync<PostgresException>(() => command.ExecuteNonQueryAsync(token));
        Assert.Equal(PostgresErrorCodes.UniqueViolation, exception.SqlState);
    }

    [Fact]
    public async Task ConstraintMigrationShouldPreserveExistingData()
    {
        await using var database = await PostgreSqlDatabase.CreateAsync(fixture.ConnectionString);
        await using var context = database.CreateContext();
        var token = TestContext.Current.CancellationToken;
        var migrator = context.GetService<IMigrator>();
        var seedMigration = context.Database.GetMigrations().Single(name => name.EndsWith("_SeedInitialData"));
        await migrator.MigrateAsync(seedMigration, token);
        context.Specializations.Add(new Specialization { Name = "Добавлено до новых ограничений" });
        await context.SaveChangesAsync(token);
        var before = await context.Specializations.AsNoTracking().OrderBy(item => item.Id)
            .Select(item => new { item.Id, item.Name }).ToListAsync(token);

        await context.Database.MigrateAsync(token);

        Assert.Equal(before, await context.Specializations.AsNoTracking().OrderBy(item => item.Id)
            .Select(item => new { item.Id, item.Name }).ToListAsync(token));
        Assert.Equal(12, await context.Clients.CountAsync(token));
        Assert.Equal(12, await context.Trainers.CountAsync(token));
        Assert.Equal(26, await context.Sessions.CountAsync(token));
        Assert.False(context.Database.HasPendingModelChanges());
    }
}
