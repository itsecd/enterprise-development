using FitnessClub.Infrastructure.PostgreSql;
using FitnessClub.Infrastructure.PostgreSql.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Tests.Infrastructure;

/// <summary>
/// Коллекция, в которой выполняются все интеграционные тесты
/// </summary>
[CollectionDefinition("PostgreSql")]
public sealed class PostgresCollection : ICollectionFixture<PostgresFixture>;

/// <summary>
/// Фикстура уровня коллекции
/// </summary>
public sealed class PostgresFixture : IDisposable
{
    /// <summary>
    /// Переменная окружения для переопределения строки подключения к тестовой БД
    /// </summary>
    public const string ConnectionStringEnvVar = "FITNESSCLUB_TESTS_CONNECTION";

    /// <summary>
    /// Строка подключения к тестовой базе fitnessclub_tests
    /// </summary>
    public string ConnectionString { get; } =
        Environment.GetEnvironmentVariable(ConnectionStringEnvVar)
        ?? "Host=localhost;Port=5432;Database=fitnessclub_tests;Username=postgres;Password=postgres";

    /// <summary>
    /// DI-контейнер с DbContext и репозиториями PostgreSql-хранилища
    /// </summary>
    private ServiceProvider? _services;

    /// <summary>
    /// Задача однократной инициализации фикстуры
    /// </summary>
    private Task? _initTask;

    /// <summary>
    /// Открывает DI-область
    /// </summary>
    /// <returns>DI-область с DbContext и репозиториями</returns>
    public Task<IServiceScope> CreateScopeAsync()
    {
        _initTask ??= InitializeCoreAsync();

        return AwaitReady();

        async Task<IServiceScope> AwaitReady()
        {
            await _initTask;

            return _services!.CreateScope();
        }
    }

    /// <summary>
    /// Пересоздаёт тестовую базу и строит DI-контейнер
    /// </summary>
    private async Task InitializeCoreAsync()
    {
        var options = new DbContextOptionsBuilder<FitnessClubDbContext>()
            .UseNpgsql(ConnectionString)
            .Options;

        await using var db = new FitnessClubDbContext(options);
        await db.Database.EnsureDeletedAsync();
        await db.Database.MigrateAsync();

        var services = new ServiceCollection();
        services.AddDbContext<FitnessClubDbContext>(o => o.UseNpgsql(ConnectionString));
        services.AddPostgreSqlRepositories();
        _services = services.BuildServiceProvider();
    }

    /// <summary>
    /// Останавливает DI-контейнер
    /// </summary>
    public void Dispose()
    {
        _services?.DisposeAsync().AsTask().GetAwaiter().GetResult();
        _services = null;
    }
}
