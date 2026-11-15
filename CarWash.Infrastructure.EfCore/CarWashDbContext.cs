
using CarWash.Domain.Data;
using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных автомойки
/// </summary>
/// <param name="options">Параметры контекста базы данных</param>
public class CarWashDbContext(DbContextOptions<CarWashDbContext> options)
    : DbContext(options)
{
    /// <summary>
    /// Клиенты автомойки
    /// </summary>
    public DbSet<CarWashCustomer> CarWashCustomers => Set<CarWashCustomer>();

    /// <summary>
    /// Автомобили клиентов
    /// </summary>
    public DbSet<Car> Cars => Set<Car>();

    /// <summary>
    /// Услуги автомойки
    /// </summary>
    public DbSet<CarWashProcedure> CarWashProcedures => Set<CarWashProcedure>();

    /// <summary>
    /// Заказы на обслуживание автомобилей
    /// </summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var seed = new DataSeeder();

        modelBuilder.Entity<CarWashCustomer>(entity =>
        {
            entity.Ignore(customer => customer.Orders);

            entity.HasMany(customer => customer.Cars)
                .WithOne(car => car.Customer)
                .HasForeignKey(car => car.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasMany(car => car.Orders)
                .WithOne(order => order.Car)
                .HasForeignKey(order => order.CarId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<CarWashProcedure>(entity =>
        {
            entity.HasMany(procedure => procedure.Orders)
                .WithOne(order => order.Procedure)
                .HasForeignKey(order => order.ProcedureId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(procedure => procedure.Price)
                .HasPrecision(10, 2);
        });

        modelBuilder.Entity<CarWashCustomer>(entity =>
        {
            entity.HasData(seed.Customers.Select(customer => new
            {
                customer.Id,
                customer.LastName,
                customer.FirstName,
                customer.Patronymic,
                customer.Phone
            }));
        });

        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasData(seed.Cars.Select(car => new
            {
                car.Id,
                car.CarCategory,
                car.LicensePlate,
                car.Brand,
                car.CustomerId
            }));
        });

        modelBuilder.Entity<CarWashProcedure>(entity =>
        {
            entity.HasData(seed.Procedures.Select(procedure => new
            {
                procedure.Id,
                procedure.Name,
                procedure.CarCategory,
                procedure.Price,
                procedure.Duration
            }));
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasData(seed.Orders.Select(order => new
            {
                order.Id,
                order.CarId,
                order.ProcedureId,
                order.StartTime,
                order.WashBoxNumber
            }));
        });
    }
}
