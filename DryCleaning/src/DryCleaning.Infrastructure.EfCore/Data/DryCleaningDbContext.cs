using DryCleaning.Domain;
using Microsoft.EntityFrameworkCore;

namespace DryCleaning.Infrastructure.EfCore.Data;

/// <summary>
/// Контекст базы данных химчистки
/// </summary>
/// <param name="options">Параметры подключения к базе</param>
public class DryCleaningDbContext(DbContextOptions<DryCleaningDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Таблица клиентов
    /// </summary>
    public DbSet<Client> Clients => Set<Client>();
    /// <summary>
    /// Таблица категорий изделий
    /// </summary>
    public DbSet<ItemCategory> ItemCategories => Set<ItemCategory>();
    /// <summary>
    /// Таблица изделий
    /// </summary>
    public DbSet<Item> Items => Set<Item>();
    /// <summary>
    /// Таблица заказов
    /// </summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>
    /// Применяет конфигурации сущностей из текущей сборки
    /// </summary>
    /// <param name="modelBuilder">Билдер модели</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DryCleaningDbContext).Assembly);
    }
}