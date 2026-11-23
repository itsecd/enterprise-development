using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.EfCore.Data;

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

        // Настройка связей
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

        modelBuilder.Entity<Client>().HasData(SeederData.Clients);
        modelBuilder.Entity<Master>().HasData(SeederData.Masters);
        modelBuilder.Entity<Service>().HasData(SeederData.Services);

        // Для Appointment нужно игнорировать навигационные свойства при HasData
        modelBuilder.Entity<Appointment>().HasData(
            SeederData.Appointments.Select(a => new Appointment
            {
                Id = a.Id,
                StartTime = a.StartTime,
                IsRegularClient = a.IsRegularClient,
                ClientId = a.ClientId,
                MasterId = a.MasterId,
                ServiceId = a.ServiceId,
                Client = null!,
                Master = null!,
                Service = null!
            })
        );
    }
}