using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных приложения FoodDelivery
/// </summary>
public class FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Категории блюд
    /// </summary>
    public DbSet<Category> Categories => Set<Category>();

    /// <summary>
    /// Клиенты
    /// </summary>
    public DbSet<Customer> Customers => Set<Customer>();

    /// <summary>
    /// Блюда
    /// </summary>
    public DbSet<Dish> Dishes => Set<Dish>();

    /// <summary>
    /// Рестораны
    /// </summary>
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();

    /// <summary>
    /// Заказы
    /// </summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>
    /// Позиции заказов
    /// </summary>
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customers");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.LastName)
                .HasColumnName("last_name")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.FirstName)
                .HasColumnName("first_name")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.Patronymic)
                .HasColumnName("patronymic")
                .HasMaxLength(100)
                .IsRequired(false);

            entity.Property(x => x.Phone)
                .HasColumnName("phone")
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(x => x.DeliveryAddress)
                .HasColumnName("delivery_address")
                .HasMaxLength(250)
                .IsRequired();
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.ToTable("restaurants");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.Address)
                .HasColumnName("address")
                .HasMaxLength(250)
                .IsRequired();

            entity.Property(x => x.Rating)
                .HasColumnName("rating");

            entity.Property(x => x.OpeningTime)
                .HasColumnName("opening_time");

            entity.Property(x => x.ClosingTime)
                .HasColumnName("closing_time");
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.ToTable("dishes");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(x => x.WeightGrams)
                .HasColumnName("weight_grams");

            entity.Property(x => x.Price)
                .HasColumnName("price")
                .HasPrecision(18, 2);

            entity.Property(x => x.CategoryId)
                .HasColumnName("category_id");

            entity.Property(x => x.RestaurantId)
                .HasColumnName("restaurant_id");

            entity.HasOne(x => x.Category)
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Restaurant)
                .WithMany(x => x.Dishes)
                .HasForeignKey(x => x.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.CustomerId)
                .HasColumnName("customer_id");

            entity.Property(x => x.RestaurantId)
                .HasColumnName("restaurant_id");

            entity.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnType("timestamp without time zone")
                .IsRequired();

            entity.Property(x => x.DeliveredAt)
                .HasColumnName("delivered_at")
                .HasColumnType("timestamp without time zone")
                .IsRequired(false);

            entity.HasOne(x => x.Customer)
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.Restaurant)
                .WithMany()
                .HasForeignKey(x => x.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(x => x.Items)
                .WithOne()
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.Ignore(x => x.TotalAmount);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("order_items");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(x => x.OrderId)
                .HasColumnName("order_id");

            entity.Property(x => x.DishId)
                .HasColumnName("dish_id");

            entity.Property(x => x.Quantity)
                .HasColumnName("quantity");

            entity.HasOne(x => x.Dish)
                .WithMany()
                .HasForeignKey(x => x.DishId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(TestData.Categories.Select(c => new { c.Id, c.Name }));
        modelBuilder.Entity<Customer>().HasData(TestData.Customers.Select(c => new { c.Id, c.LastName, c.FirstName, c.Patronymic, c.Phone, c.DeliveryAddress }));
        modelBuilder.Entity<Restaurant>().HasData(TestData.Restaurants.Select(r => new { r.Id, r.Name, r.Address, r.Rating, r.OpeningTime, r.ClosingTime }));
        modelBuilder.Entity<Dish>().HasData(TestData.Dishes.Select(d => new { d.Id, d.Name, d.WeightGrams, d.Price, d.CategoryId, d.RestaurantId }));
        modelBuilder.Entity<Order>().HasData(TestData.Orders.Select(o => new { o.Id, o.CustomerId, o.RestaurantId, o.CreatedAt, o.DeliveredAt }));
        modelBuilder.Entity<OrderItem>().HasData(TestData.OrderItems.Select(oi => new { oi.Id, oi.OrderId, oi.DishId, oi.Quantity }));
    }
}