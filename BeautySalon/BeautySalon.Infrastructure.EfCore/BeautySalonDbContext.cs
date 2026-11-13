using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных
/// </summary>
/// <param name="options">Опции контекста</param>
public class BeautySalonDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Master> Masters { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(builder =>
        {
            builder.HasKey(c => c.Id);

            builder.HasData(DataSeeder.Clients);
        });
        modelBuilder.Entity<Master>(builder =>
        {
            builder.HasKey(c => c.Id);

            builder.HasData(DataSeeder.Masters);
        });
        modelBuilder.Entity<Service>(builder =>
        {
            builder.HasKey(s => s.Id);

            builder.HasData(DataSeeder.Services);
        });
        modelBuilder.Entity<Appointment>(builder =>
        {
            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.Client).WithMany();

            builder.HasOne(a => a.Master).WithMany();

            builder.HasOne(a => a.Service).WithMany();

            builder.Navigation(a => a.Client).AutoInclude();
            builder.Navigation(a => a.Master).AutoInclude();
            builder.Navigation(a => a.Service).AutoInclude();

            builder.HasData(DataSeeder.Appointments.Select(a => new
            {
                a.Id,
                ClientId = a.Client.Id,
                ServiceId = a.Service.Id,
                MasterId = a.Master.Id,
                a.StartTime,
                a.IsRegular
            }));
        });
    }
}
