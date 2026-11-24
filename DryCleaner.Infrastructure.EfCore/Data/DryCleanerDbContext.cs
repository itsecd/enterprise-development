using DryCleaner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EfCore.Data;

/// <summary>
/// Контекст базы данных EfCore
/// </summary>
public class DryCleanerDbContext(DbContextOptions<DryCleanerDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Таблица клиентов химчистки
    /// </summary>
    public DbSet<Customer> Customers => Set<Customer>();
    /// <summary>
    /// Таблица изделий
    /// </summary>
    public DbSet<Item> Items => Set<Item>();
    /// <summary>
    /// Таблица категорий изделий
    /// </summary>
    public DbSet<ItemCategory> ItemCategories => Set<ItemCategory>();
    /// <summary>
    /// Таблица заказов
    /// </summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>
    /// Настраивает модель базы данных
    /// </summary>
    /// <param name="modelBuilder">Построитель модели, используемый EF Core</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>().Property(c => c.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<ItemCategory>().Property(c => c.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Item>().Property(i => i.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<Order>().Property(o => o.Id).ValueGeneratedOnAdd();

        modelBuilder.Entity<Item>()
            .HasOne(i => i.Category)
            .WithMany()
            .HasForeignKey(i => i.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Item)
            .WithMany()
            .HasForeignKey(o => o.ItemId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ItemCategory>().HasData(DryCleaner.Domain.Data.DataSeed.Categories);
        modelBuilder.Entity<Customer>().HasData(DryCleaner.Domain.Data.DataSeed.Customers);

        modelBuilder.Entity<Item>().HasData(
            DryCleaner.Domain.Data.DataSeed.Items.Select(i => new { i.Id, i.Name, i.CategoryId, i.Material })
        );

        modelBuilder.Entity<Order>().HasData(
            DryCleaner.Domain.Data.DataSeed.Orders.Select(o => new { o.Id, o.CustomerId, o.ItemId, o.AcceptanceDate, o.CompletionDays, o.Status })
        );
    }
}