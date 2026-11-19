using AutoService.Domain.Data;
using AutoService.Domain.Models;
using AutoService.Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Infrastructure.Persistence;

/// <summary>
/// Контекст базы данных автосервиса
/// </summary>
public class AutoServiceDbContext(DbContextOptions<AutoServiceDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Клиенты автосервиса
    /// </summary>
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>
    /// Автомобили клиентов
    /// </summary>
    public DbSet<Car> Cars => Set<Car>();

    /// <summary>
    /// Механики автосервиса
    /// </summary>
    public DbSet<Mechanic> Mechanics => Set<Mechanic>();

    /// <summary>
    /// Виды работ автосервиса
    /// </summary>
    public DbSet<WorkType> WorkTypes => Set<WorkType>();

    /// <summary>
    /// Заказы автосервиса
    /// </summary>
    public DbSet<Order> Orders => Set<Order>();

    /// <summary>
    /// Пользователи приложения
    /// </summary>
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AutoServiceDbContext).Assembly);

        Seed(modelBuilder);
    }

    /// <summary>
    /// Начальное заполнение таблиц данными из DataSeed
    /// </summary>
    private static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>().HasData(DataSeed.Clients);
        modelBuilder.Entity<Car>().HasData(DataSeed.Cars.Select(c => new
        {
            c.Id,
            c.LicensePlate,
            c.Brand,
            c.Model,
            c.Year,
            c.ClientId
        }));
        modelBuilder.Entity<Mechanic>().HasData(DataSeed.Mechanics);
        modelBuilder.Entity<WorkType>().HasData(DataSeed.Works);
        modelBuilder.Entity<User>().HasData(DataSeed.Users);

        modelBuilder.Entity<Order>().HasData(DataSeed.Orders.Select(o => new
        {
            o.Id,
            o.CarId,
            o.ClientId,
            o.MechanicId,
            o.AcceptedAt,
            o.IssuedAt
        }));

        modelBuilder.Entity(OrderConfiguration.JoinTableName).HasData(
            DataSeed.Orders.SelectMany(o => o.Works, (o, w) => new
            {
                order_id = o.Id,
                work_type_id = w.Id
            }));
    }
}
