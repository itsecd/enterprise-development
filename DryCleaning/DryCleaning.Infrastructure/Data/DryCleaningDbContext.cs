using Microsoft.EntityFrameworkCore;
using DryCleaning.Domain.Models;
using DryCleaning.Domain.Data;

namespace DryCleaning.Infrastructure.Data;

public class DryCleaningDbContext(DbContextOptions<DryCleaningDbContext> options) : DbContext(options)
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /// <summary>
        /// Настройка связи Клиентов с Заказами и наоборот
        /// </summary>
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Client)
            .WithMany()
            .HasForeignKey(o => o.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        /// <summary>
        /// Настройка связи Заказа с Изделием и наоборот
        /// </summary>
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Item)
            .WithMany()
            .HasForeignKey(o => o.ItemId)
            .OnDelete(DeleteBehavior.Restrict);

        /// <summary>
        /// Настройка связи Изделия с Категорией и наоборот
        /// </summary>
        modelBuilder.Entity<Item>()
            .HasOne(i => i.Category)
            .WithMany()
            .HasForeignKey(i => i.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        /// <summary>
        /// Получение стартовых данных из DataSeed.cs
        /// </summary>
        modelBuilder.Entity<Category>().HasData(DataSeed.GetCategories());
        modelBuilder.Entity<Client>().HasData(DataSeed.GetClients());
        modelBuilder.Entity<Item>().HasData(DataSeed.GetItems().Select(i => new { i.Id, i.Name, i.Material, i.CategoryId }));

        var orders = DataSeed.GetOrders().Select(o => new
        {
            o.Id,
            o.ClientId,
            o.ItemId,
            o.AcceptDate,
            o.ExeDays,
            o.Status
        });
        modelBuilder.Entity<Order>().HasData(orders);
    }
}
