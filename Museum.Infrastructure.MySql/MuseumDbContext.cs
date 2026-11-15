using Microsoft.EntityFrameworkCore;
using Museum.Domain;

namespace Museum.Infrastructure.MySql;

/// <summary>
/// Контекст базы данных музея
/// </summary>
public sealed class MuseumDbContext(DbContextOptions<MuseumDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Таблица выставок
    /// </summary>
    public DbSet<Exhibition> Exhibitions => Set<Exhibition>();

    /// <summary>
    /// Таблица посетителей
    /// </summary>
    public DbSet<Visitor> Visitors => Set<Visitor>();

    /// <summary>
    /// Таблица экскурсий
    /// </summary>
    public DbSet<Tour> Tours => Set<Tour>();

    /// <summary>
    /// Таблица билетов
    /// </summary>
    public DbSet<Ticket> Tickets => Set<Ticket>();

    /// <summary>
    /// Применяет настройки таблиц из папки Configurations
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MuseumDbContext).Assembly);
    }
}
