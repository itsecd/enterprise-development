using Microsoft.EntityFrameworkCore;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Seeds;

namespace TransportCompany.Infrastructure.MsSqlServer.Data;

/// <summary>
/// Контекст базы данных
/// </summary>
public class TransportCompanyDbContext(DbContextOptions<TransportCompanyDbContext> options)
    : DbContext(options)
{
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Driver> Drivers => Set<Driver>();
    public DbSet<VehicleModel> VehicleModels => Set<VehicleModel>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<Trip> Trips => Set<Trip>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).HasMaxLength(100);
            entity.Property(c => c.Phone).HasMaxLength(20);
        });

        modelBuilder.Entity<Driver>(entity =>
        {
            entity.HasKey(d => d.Id);
            entity.Property(d => d.PassportNumber).HasMaxLength(20);
            entity.HasIndex(d => d.PassportNumber).IsUnique();
            entity.Property(d => d.FullName).HasMaxLength(100);
        });

        modelBuilder.Entity<VehicleModel>(entity =>
        {
            entity.HasKey(m => m.Id);
        });

        modelBuilder.Entity<Vehicle>(entity =>
        {
            entity.HasKey(v => v.Id);
            entity.Property(v => v.LicensePlate).HasMaxLength(9);
            entity.HasIndex(v => v.LicensePlate).IsUnique();

            entity.HasOne(v => v.Model)
                .WithMany()
                .HasForeignKey(v => v.ModelId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(t => t.Id);
            entity.Property(t => t.Origin).HasMaxLength(100);
            entity.Property(t => t.Destination).HasMaxLength(100);
            entity.Property(t => t.TransportationCost).HasPrecision(18, 2);

            entity.HasOne(t => t.Driver)
                .WithMany()
                .HasForeignKey(t => t.DriverId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Vehicle)
                .WithMany()
                .HasForeignKey(t => t.VehicleId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(t => t.Client)
                .WithMany()
                .HasForeignKey(t => t.ClientId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        SeedData(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        var models = TestData.GetVehicleModels();
        var clients = TestData.GetClients();
        var drivers = TestData.GetDrivers();
        var vehicles = TestData.GetVehicles(models);
        var trips = TestData.GetTrips(vehicles, drivers, clients);

        modelBuilder.Entity<VehicleModel>().HasData(models);
        modelBuilder.Entity<Client>().HasData(clients);
        modelBuilder.Entity<Driver>().HasData(drivers);

        modelBuilder.Entity<Vehicle>().HasData(vehicles.Select(v => new Vehicle
        {
            Id = v.Id,
            LicensePlate = v.LicensePlate,
            LoadCapacity = v.LoadCapacity,
            ModelId = v.ModelId
        }));

        modelBuilder.Entity<Trip>().HasData(trips.Select(t => new Trip
        {
            Id = t.Id,
            DriverId = t.DriverId,
            VehicleId = t.VehicleId,
            ClientId = t.ClientId,
            Origin = t.Origin,
            Destination = t.Destination,
            Date = t.Date,
            CargoWeight = t.CargoWeight,
            TransportationCost = t.TransportationCost,
            Status = t.Status
        }));
    }
}