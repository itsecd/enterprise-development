using BeautySalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.Data;

/// <summary>
/// Контекст базы данных для салона красоты.
/// </summary>
public class BeautySalonDbContext(DbContextOptions<BeautySalonDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Клиенты.
    /// </summary>
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>
    /// Мастера.
    /// </summary>
    public DbSet<Master> Masters => Set<Master>();

    /// <summary>
    /// Услуги.
    /// </summary>
    public DbSet<Service> Services => Set<Service>();

    /// <summary>
    /// Записи.
    /// </summary>
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Client)
            .WithMany(c => c.Appointments)
            .HasForeignKey(a => a.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Master)
            .WithMany(m => m.Appointments)
            .HasForeignKey(a => a.MasterId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Service)
            .WithMany(s => s.Appointments)
            .HasForeignKey(a => a.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}