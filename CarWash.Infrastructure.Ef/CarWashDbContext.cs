using CarWash.Domain.Data;
using CarWash.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.Ef;

/// <summary>
/// Контекст базы данных автомойки
/// </summary>
public class CarWashDbContext(DbContextOptions<CarWashDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Клиенты
    /// </summary>
    public DbSet<Customer> Customers => Set<Customer>();

    /// <summary>
    /// Автомобили
    /// </summary>
    public DbSet<Car> Cars => Set<Car>();

    /// <summary>
    /// Услуги
    /// </summary>
    public DbSet<WashService> WashServices => Set<WashService>();

    /// <summary>
    /// Заказы
    /// </summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.Property(customer => customer.Id).HasIdentityOptions(startValue: 11);
            entity.Property(customer => customer.FullName).IsRequired();
            entity.Property(customer => customer.Phone).IsRequired();
        });
        modelBuilder.Entity<Car>(entity =>
        {
            entity.Property(car => car.Id).HasIdentityOptions(startValue: 11);
            entity.Property(car => car.LicensePlate).IsRequired();
            entity.Property(car => car.Brand).IsRequired();
            entity.HasOne(car => car.Customer).WithMany(customer => customer.Cars)
                .HasForeignKey(car => car.CustomerId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<WashService>(entity =>
        {
            entity.Property(service => service.Id).HasIdentityOptions(startValue: 11);
            entity.Property(service => service.Name).IsRequired();
            entity.Property(service => service.Cost).HasColumnType("numeric");
        });
        modelBuilder.Entity<Order>(entity =>
        {
            entity.Property(order => order.Id).HasIdentityOptions(startValue: 17);
            entity.Property(order => order.StartTime).HasColumnType("timestamp without time zone");
            entity.Ignore(order => order.EndTime);
            entity.Ignore(order => order.IsInProgress);
            entity.HasOne(order => order.Customer).WithMany().HasForeignKey(order => order.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(order => order.Car).WithMany().HasForeignKey(order => order.CarId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(order => order.WashService).WithMany().HasForeignKey(order => order.WashServiceId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        var data = new CarWashData(CarWashData.MigrationReferenceTime);
        modelBuilder.Entity<Customer>().HasData(data.Customers.Select(customer => new
        {
            customer.Id, customer.FullName, customer.Phone
        }));
        modelBuilder.Entity<Car>().HasData(data.Cars.Select(car => new
        {
            car.Id, car.LicensePlate, car.Brand, car.CustomerId
        }));
        modelBuilder.Entity<WashService>().HasData(data.WashServices.Select(service => new
        {
            service.Id, service.Name, service.Category, service.Cost, service.DurationMinutes
        }));
        modelBuilder.Entity<Order>().HasData(data.Orders.Select(order => new
        {
            order.Id, order.CustomerId, order.CarId, order.WashServiceId, order.StartTime, order.BoxNumber
        }));
    }
}
