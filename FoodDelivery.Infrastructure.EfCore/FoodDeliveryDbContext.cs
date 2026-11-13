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

        ConfigureCategory(modelBuilder);
        ConfigureCustomer(modelBuilder);
        ConfigureRestaurant(modelBuilder);
        ConfigureDish(modelBuilder);
        ConfigureOrder(modelBuilder);
        ConfigureOrderItem(modelBuilder);

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            TestData.Categories.Select(category => new
            {
                category.Id,
                category.Name
            })
        );

        modelBuilder.Entity<Customer>().HasData(
            TestData.Customers.Select(customer => new
            {
                customer.Id,
                customer.LastName,
                customer.FirstName,
                customer.Patronymic,
                customer.Phone,
                customer.DeliveryAddress
            })
        );

        modelBuilder.Entity<Restaurant>().HasData(
            TestData.Restaurants.Select(restaurant => new
            {
                restaurant.Id,
                restaurant.Name,
                restaurant.Address,
                restaurant.Rating,
                restaurant.OpeningTime,
                restaurant.ClosingTime
            })
        );

        modelBuilder.Entity<Dish>().HasData(
            TestData.Dishes.Select(dish => new
            {
                dish.Id,
                dish.Name,
                dish.WeightGrams,
                dish.Price,
                dish.CategoryId,
                dish.RestaurantId
            })
        );

        modelBuilder.Entity<Order>().HasData(
            TestData.Orders.Select(order => new
            {
                order.Id,
                order.CustomerId,
                order.RestaurantId,
                order.CreatedAt,
                order.DeliveredAt
            })
        );

        modelBuilder.Entity<OrderItem>().HasData(
            TestData.OrderItems.Select(orderItem => new
            {
                orderItem.Id,
                orderItem.OrderId,
                orderItem.DishId,
                orderItem.Quantity
            })
        );
    }

    private static void ConfigureCategory(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(category => category.Id);

            entity.Property(category => category.Id)
                .ValueGeneratedOnAdd();

            entity.Property(category => category.Name)
                .IsRequired()
                .HasMaxLength(100);
        });
    }

    private static void ConfigureCustomer(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(customer => customer.Id);

            entity.Property(customer => customer.Id)
                .ValueGeneratedOnAdd();

            entity.Property(customer => customer.LastName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(customer => customer.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(customer => customer.Patronymic)
                .HasMaxLength(100);

            entity.Property(customer => customer.Phone)
                .IsRequired()
                .HasMaxLength(20);

            entity.Property(customer => customer.DeliveryAddress)
                .IsRequired()
                .HasMaxLength(250);
        });
    }

    private static void ConfigureRestaurant(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Restaurant>(entity =>
        {
            entity.HasKey(restaurant => restaurant.Id);

            entity.Property(restaurant => restaurant.Id)
                .ValueGeneratedOnAdd();

            entity.Property(restaurant => restaurant.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(restaurant => restaurant.Address)
                .IsRequired()
                .HasMaxLength(250);

            entity.Property(restaurant => restaurant.Rating)
                .IsRequired();

            entity.Property(restaurant => restaurant.OpeningTime)
                .IsRequired();

            entity.Property(restaurant => restaurant.ClosingTime)
                .IsRequired();
        });
    }

    private static void ConfigureDish(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(dish => dish.Id);

            entity.Property(dish => dish.Id)
                .ValueGeneratedOnAdd();

            entity.Property(dish => dish.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(dish => dish.WeightGrams)
                .IsRequired();

            entity.Property(dish => dish.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            entity.HasOne(dish => dish.Category)
                .WithMany()
                .HasForeignKey(dish => dish.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(dish => dish.Restaurant)
                .WithMany(restaurant => restaurant.Dishes)
                .HasForeignKey(dish => dish.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureOrder(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(order => order.Id);

            entity.Property(order => order.Id)
                .ValueGeneratedOnAdd();

            entity.HasOne(order => order.Customer)
                .WithMany()
                .HasForeignKey(order => order.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(order => order.Restaurant)
                .WithMany()
                .HasForeignKey(order => order.RestaurantId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(order => order.CreatedAt)
                .HasColumnType("timestamp without time zone")
                .IsRequired();

            entity.Property(order => order.DeliveredAt)
                .HasColumnType("timestamp without time zone")
                .IsRequired();

            entity.Ignore(order => order.TotalAmount);

            entity.HasMany(order => order.Items)
                .WithOne()
                .HasForeignKey(orderItem => orderItem.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureOrderItem(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(orderItem => orderItem.Id);

            entity.Property(orderItem => orderItem.Id)
                .ValueGeneratedOnAdd();

            entity.HasOne(orderItem => orderItem.Dish)
                .WithMany()
                .HasForeignKey(orderItem => orderItem.DishId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(orderItem => orderItem.Quantity)
                .IsRequired();
        });
    }
}