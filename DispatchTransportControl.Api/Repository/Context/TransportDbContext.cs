using DispatchTransportControl.Domain;
using Microsoft.EntityFrameworkCore;

namespace DispatchTransportControl.Api.Repository.Context;

/// <summary>
/// Класс для доступа к базе данных
/// </summary>
public class TransportDbContext(DbContextOptions<TransportDbContext> options) : DbContext(options)
{
    public DbSet<Driver> Drivers { get; set; }

    public DbSet<RouteAssignment> RouteAssignments { get; set; }

    public DbSet<Vehicle> Vehicles { get; set; }

    public DbSet<VehicleModel> VehicleModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Vehicle>()
            .Navigation(v => v.VehicleModel)
            .AutoInclude();

        modelBuilder.Entity<RouteAssignment>()
            .Navigation(ra => ra.Vehicle)
            .AutoInclude();
        modelBuilder.Entity<RouteAssignment>()
            .Navigation(ra => ra.Driver)
            .AutoInclude();
    }
}