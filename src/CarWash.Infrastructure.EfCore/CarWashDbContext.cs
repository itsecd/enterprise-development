using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных автомойки
/// </summary>
/// <param name="options">Параметры контекста</param>
public class CarWashDbContext(DbContextOptions<CarWashDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Версия сервера MySQL, под которую генерируется SQL.
    /// Должна совпадать с тегом образа MySQL в CarWash.AppHost
    /// </summary>
    public static readonly ServerVersion MySqlVersion = new MySqlServerVersion(new Version(8, 4, 0));

    /// <summary>
    /// Имя таблицы истории миграций
    /// </summary>
    public const string MigrationsHistoryTableName = "__ef_migrations_history";

    /// <summary>
    /// Автомобили
    /// </summary>
    public DbSet<Car> Cars => Set<Car>();

    /// <summary>
    /// Клиенты
    /// </summary>
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>
    /// Услуги
    /// </summary>
    public DbSet<Service> Services => Set<Service>();

    /// <summary>
    /// Заказы
    /// </summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarWashDbContext).Assembly);
    }
}
