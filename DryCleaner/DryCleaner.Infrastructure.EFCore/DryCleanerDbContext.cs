using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EFCore;

/// <summary>
/// Класс описывающий контекст базы данных.
/// </summary>
/// <param name="options">Настройки контекста базы данных</param>
public class DryCleanerDbContext(DbContextOptions options) : DbContext(options)
{

    /// <summary>
    /// Набор клиентов, хранящихся в базе данных.
    /// </summary>
    public DbSet<Client> Clients { get; set; }

    /// <summary>
    /// Набор категорий товаров, хранящихся в базе данных.
    /// </summary>
    public DbSet<ProductCategory> ProductCategories { get; set; }

    /// <summary>
    /// Набор товаров, хранящихся в базе данных.
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Набор заказов, хранящихся в базе данных.
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Настраивает конфигурацию сущностей и их связей в модели базы данных,
    /// а также добавляет начальные данные.
    /// </summary>
    /// <param name="modelBuilder">Строитель модели базы данных.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(builder =>
        {
            builder.HasKey(s => s.Id);

            builder.HasData(DataSeed.Clients);
        });

        modelBuilder.Entity<ProductCategory>(builder =>
        {
            builder.HasKey(pc => pc.Id);

            builder.HasData(DataSeed.Categories);
        });

        modelBuilder.Entity<Product>(builder =>
        {
            builder.HasOne(c => c.Category);

            builder.HasKey(p => p.Id);

            builder.HasData(DataSeed.Products.Select(p => new
            {
                p.Id,
                p.Name,
                p.Material,
                CategoryId = p.Category.Id
            }));
        });

        modelBuilder.Entity<Order>(builder =>
        {
            builder.HasOne(o => o.Client)
            .WithMany()
            .IsRequired();

            builder.HasOne(o => o.Product)
            .WithMany()
            .IsRequired();

            builder.HasKey(o => o.Id);

            builder.HasData(DataSeed.Orders.Select(o => new
            {
                o.Id,
                ClientId = o.Client.Id,
                ProductId = o.Product.Id,
                o.AcceptedAt,
                o.ProcessingDays,
                o.Status
            }));
        });
    }
}
