using EnterpriseDevelopment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseDevelopment.Infrastructure.Persistence;

/// <summary>
/// Контекст базы данных службы доставки еды
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Dish> Dishes => Set<Dish>();
    public DbSet<DishCategory> DishCategories => Set<DishCategory>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<Restaurant> Restaurants => Set<Restaurant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>()
            .HasKey(client => client.Id);

        modelBuilder.Entity<DishCategory>()
            .HasKey(category => category.Id);

        modelBuilder.Entity<Restaurant>()
            .HasKey(restaurant => restaurant.Id);

        modelBuilder.Entity<Dish>(entity =>
        {
            entity.HasKey(dish => dish.Id);

            entity.Property(dish => dish.Price)
                .HasPrecision(18, 2);

            entity.HasOne(dish => dish.Category)
                .WithMany()
                .HasForeignKey(dish => dish.CategoryId);

            entity.HasOne(dish => dish.Restaurant)
                .WithMany()
                .HasForeignKey(dish => dish.RestaurantId);
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(order => order.Id);

            entity.Ignore(order => order.TotalAmount);
            entity.Ignore(order => order.DeliveryTime);
            entity.Ignore(order => order.Dishes);

            entity.HasOne(order => order.Client)
                .WithMany()
                .HasForeignKey(order => order.ClientId);

            entity.HasOne(order => order.Restaurant)
                .WithMany()
                .HasForeignKey(order => order.RestaurantId);
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(item => new
            {
                item.OrderId,
                item.DishId
            });

            entity.Property(item => item.Quantity)
                .IsRequired();

            entity.HasOne(item => item.Order)
                .WithMany()
                .HasForeignKey(item => item.OrderId);

            entity.HasOne(item => item.Dish)
                .WithMany()
                .HasForeignKey(item => item.DishId);
        });
    }
}
