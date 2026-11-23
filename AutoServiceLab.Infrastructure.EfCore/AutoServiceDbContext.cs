using AutoServiceLab.Domain.Data;
using AutoServiceLab.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AutoServiceLab.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных для автосервиса
/// </summary>
public class AutoServiceDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    /// Коллекция клиентов
    /// </summary>
    public DbSet<Client> Clients { get; set; }

    /// <summary>
    /// Коллекция автомобилей
    /// </summary>
    public DbSet<Car> Cars { get; set; }

    /// <summary>
    /// Коллекция механиков
    /// </summary>
    public DbSet<Mechanic> Mechanics { get; set; }

    /// <summary>
    /// Коллекция видов работ
    /// </summary>
    public DbSet<WorkType> WorkTypes { get; set; }

    /// <summary>
    /// Коллекция заказов
    /// </summary>
    public DbSet<ServiceOrder> ServiceOrders { get; set; }

    /// <summary>
    /// Коллекция связей заказов и работ
    /// </summary>
    public DbSet<OrderWork> OrderWorks { get; set; }

    /// <summary>
    /// Коллекция связей заказов и механиков
    /// </summary>
    public DbSet<OrderMechanic> OrderMechanics { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Car>().HasOne(c => c.Client).WithMany(cl => cl.Cars).HasForeignKey(c => c.ClientId);
        modelBuilder.Entity<ServiceOrder>().HasOne(o => o.Client).WithMany(c => c.Orders).HasForeignKey(o => o.ClientId);
        modelBuilder.Entity<ServiceOrder>().HasOne(o => o.Car).WithMany(c => c.Orders).HasForeignKey(o => o.CarId);
        modelBuilder.Entity<OrderWork>().HasOne(ow => ow.ServiceOrder).WithMany(o => o.OrderWorks).HasForeignKey(ow => ow.ServiceOrderId);
        modelBuilder.Entity<OrderWork>().HasOne(ow => ow.WorkType).WithMany(w => w.OrderWorks).HasForeignKey(ow => ow.WorkTypeId);
        modelBuilder.Entity<OrderMechanic>().HasOne(om => om.ServiceOrder).WithMany(o => o.OrderMechanics).HasForeignKey(om => om.ServiceOrderId);
        modelBuilder.Entity<OrderMechanic>().HasOne(om => om.Mechanic).WithMany(m => m.OrderMechanics).HasForeignKey(om => om.MechanicId);

        var clients = DataSeeder.GetClients();
        modelBuilder.Entity<Client>().HasData(
            clients.Select(c => new { c.Id, c.FullName, c.Phone })
        );

        var cars = DataSeeder.GetCars(clients);
        modelBuilder.Entity<Car>().HasData(
            cars.Select(c => new { c.Id, c.LicensePlate, c.Brand, c.Model, c.Year, c.ClientId })
        );

        var mechanics = DataSeeder.GetMechanics();
        modelBuilder.Entity<Mechanic>().HasData(
            mechanics.Select(m => new { m.Id, m.FullName, m.PassportNumber, m.Phone, m.Specialization, m.ExperienceYears })
        );

        var workTypes = DataSeeder.GetWorkTypes();
        modelBuilder.Entity<WorkType>().HasData(
            workTypes.Select(w => new { w.Id, w.Name, w.Category, w.Duration, w.LaborCost })
        );

        var orders = DataSeeder.GetServiceOrders(clients, cars);
        modelBuilder.Entity<ServiceOrder>().HasData(
            orders.Select(o => new { o.Id, o.ReceptionDate, o.IssueDate, o.ClientId, o.CarId })
        );

        var orderWorks = DataSeeder.GetOrderWorks(orders, workTypes);
        modelBuilder.Entity<OrderWork>().HasData(
            orderWorks.Select(ow => new { ow.Id, ow.ServiceOrderId, ow.WorkTypeId })
        );

        var orderMechanics = DataSeeder.GetOrderMechanics(orders, mechanics);
        modelBuilder.Entity<OrderMechanic>().HasData(
            orderMechanics.Select(om => new { om.Id, om.ServiceOrderId, om.MechanicId })
        );
    }
}