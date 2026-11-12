using Microsoft.EntityFrameworkCore;
using Carwash.Domain.Entities;
using Carwash.Domain.Data;

namespace Carwash.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных для управления сущностями Carwash
/// </summary>
/// <param name="options">Опции контекста базы данных</param>
public class CarwashDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Service> Services { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Конфигурация сущности Client
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasMany(c => c.Cars).WithOne(c => c.Client)
            .HasForeignKey(c => c.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
            entity.HasData(DataSeed.Clients);
        });
        // Конфигурация сущности Car
        modelBuilder.Entity<Car>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasData(DataSeed.Cars.Select(c => new
            {
                c.Id,
                c.LicensePlate,
                c.Brand,
                c.ClientId
            }));
        });
        // Конфигурация сущности Order
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Ignore(o => o.EndTime);

            entity.HasOne(o => o.Car)
            .WithMany()
            .HasForeignKey(o => o.CarId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(o => o.Client)
            .WithMany()
            .HasForeignKey(o => o.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(o => o.Service)
            .WithMany()
            .HasForeignKey(o => o.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

            entity.Navigation(o => o.Car).AutoInclude();
            entity.Navigation(o => o.Client).AutoInclude();
            entity.Navigation(o => o.Service).AutoInclude();

            entity.HasData(DataSeed.Orders.Select(o => new
            {
                o.Id,
                o.ClientId,
                o.CarId,
                o.ServiceId,
                o.BoxNumber,
                o.StartTime
            }));
        });
        // Конфигурация сущности Service
        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasData(DataSeed.Services);
        });
    }
}
