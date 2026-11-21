using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных службы доставки еды
/// </summary>
/// <param name="options">Параметры подключения к MySQL</param>
public sealed class FoodDeliveryDbContext(DbContextOptions<FoodDeliveryDbContext> options)
    : DbContext(options)
{
    /// <summary>Категории блюд</summary>
    public DbSet<Category> Categories => Set<Category>();

    /// <summary>Клиенты</summary>
    public DbSet<Customer> Customers => Set<Customer>();

    /// <summary>Рестораны</summary>
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();

    /// <summary>Блюда</summary>
    public DbSet<Dish> Dishes => Set<Dish>();

    /// <summary>Заказы</summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>Позиции заказов</summary>
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var data = new FoodDeliveryData();

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("categories");
            entity.Property(category => category.Name).HasMaxLength(100);
            entity.HasData(data.Categories);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customers");
            entity.Property(customer => customer.LastName).HasMaxLength(100);
            entity.Property(customer => customer.FirstName).HasMaxLength(100);
            entity.Property(customer => customer.Patronymic).HasMaxLength(100);
            entity.Property(customer => customer.Phone).HasMaxLength(30);
            entity.Property(customer => customer.DeliveryAddress).HasMaxLength(255);
            entity.HasData(data.Customers);
        });

        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.ToTable("restaurants");
            entity.Property(restaurant => restaurant.Name).HasMaxLength(100);
            entity.Property(restaurant => restaurant.Address).HasMaxLength(255);
            entity.Property(restaurant => restaurant.Rating).HasPrecision(30, 28);
            entity.HasData(data.Restaurants);
        });

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.ToTable("dishes");
            entity.Property(dish => dish.Name).HasMaxLength(100);
            entity.Property(dish => dish.Price).HasPrecision(18, 2);

            entity.HasOne(dish => dish.Category)
                .WithMany()
                .HasForeignKey(dish => dish.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(dish => dish.Restaurant)
                .WithMany()
                .HasForeignKey(dish => dish.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(data.Dishes.Select(dish => new
            {
                dish.Id,
                dish.Name,
                dish.Weight,
                dish.Price,
                CategoryId = dish.Category.Id,
                RestaurantId = dish.Restaurant.Id
            }));
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("orders");

            entity.HasOne(order => order.Customer)
                .WithMany()
                .HasForeignKey(order => order.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(order => order.Restaurant)
                .WithMany()
                .HasForeignKey(order => order.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(order => order.Items)
                .WithOne()
                .HasForeignKey(item => item.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(data.Orders.Select(order => new
            {
                order.Id,
                order.CreatedAt,
                order.DeliveredAt,
                CustomerId = order.Customer.Id,
                RestaurantId = order.Restaurant.Id
            }));
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("order_items");
            entity.Property(item => item.UnitPrice).HasPrecision(18, 2);

            entity.HasOne(item => item.Dish)
                .WithMany()
                .HasForeignKey(item => item.DishId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(data.Orders.SelectMany(order => order.Items).Select(item => new
            {
                item.Id,
                item.OrderId,
                item.DishId,
                item.Quantity,
                item.UnitPrice
            }));
        });
    }
}
