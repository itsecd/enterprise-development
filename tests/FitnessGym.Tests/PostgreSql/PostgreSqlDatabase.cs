using FitnessGym.Infrastructure.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FitnessGym.Tests.PostgreSql;

/// <summary>
/// Отдельная временная база на каждый тест. Удаляется только база, созданная этим экземпляром.
/// </summary>
public sealed class PostgreSqlDatabase : IAsyncDisposable
{
    private readonly string _adminConnectionString;
    private readonly string _databaseName = $"fitnessgym_test_{Guid.NewGuid():N}";

    private PostgreSqlDatabase(string adminConnectionString)
    {
        _adminConnectionString = adminConnectionString;
        ConnectionString = new NpgsqlConnectionStringBuilder(adminConnectionString)
        {
            Database = _databaseName,
            Pooling = false,
        }.ConnectionString;
    }

    public static bool IsConfigured =>
        !string.IsNullOrWhiteSpace(Environment.GetEnvironmentVariable("FITNESSGYM_TEST_CONNECTION_STRING"));

    public string ConnectionString { get; }

    public FitnessGymDbContext CreateContext() => new(new DbContextOptionsBuilder<FitnessGymDbContext>()
        .UseNpgsql(ConnectionString).Options);

    public static async Task<PostgreSqlDatabase> CreateAsync()
    {
        var connectionString = Environment.GetEnvironmentVariable("FITNESSGYM_TEST_CONNECTION_STRING")
                               ?? throw new InvalidOperationException("Не задан FITNESSGYM_TEST_CONNECTION_STRING.");
        var database = new PostgreSqlDatabase(connectionString);
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(TestContext.Current.CancellationToken);
        await using var command = new NpgsqlCommand($"CREATE DATABASE \"{database._databaseName}\"", connection);
        await command.ExecuteNonQueryAsync(TestContext.Current.CancellationToken);
        try
        {
            await using var context = database.CreateContext();
            await context.Database.MigrateAsync(TestContext.Current.CancellationToken);
            return database;
        }
        catch
        {
            await database.DisposeAsync();
            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await using var connection = new NpgsqlConnection(_adminConnectionString);
        await connection.OpenAsync();
        await using var command = new NpgsqlCommand($"DROP DATABASE \"{_databaseName}\" WITH (FORCE)", connection);
        await command.ExecuteNonQueryAsync();
    }
}
