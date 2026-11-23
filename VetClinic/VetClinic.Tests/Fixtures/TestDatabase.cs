using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using VetClinic.Infrastructure.SqlServer;

namespace VetClinic.Tests.Fixtures;

/// <summary>
/// База SQLite в памяти с той же схемой и начальными данными, что и миграции для SQL Server
/// </summary>
public sealed class TestDatabase : IAsyncDisposable
{
    private readonly SqliteConnection _connection;

    /// <summary>
    /// Контекст, через который работают репозитории теста
    /// </summary>
    public VetClinicDbContext Context { get; }

    /// <summary>
    /// Открыть соединение и создать схему с начальными данными
    /// </summary>
    public TestDatabase()
    {
        _connection = new SqliteConnection("DataSource=:memory:");
        _connection.Open();
        Context = CreateContext();
        Context.Database.EnsureCreated();
    }

    /// <summary>
    /// Создаёт отдельный контекст для проверки сохранённых данных
    /// </summary>
    public VetClinicDbContext CreateContext() =>
        new(new DbContextOptionsBuilder<VetClinicDbContext>().UseSqlite(_connection).Options);

    /// <inheritdoc/>
    public async ValueTask DisposeAsync()
    {
        await Context.DisposeAsync();
        await _connection.DisposeAsync();
    }
}
