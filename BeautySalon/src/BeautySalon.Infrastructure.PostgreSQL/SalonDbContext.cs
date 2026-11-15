using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Seeds;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.PostgreSQL;

/// <summary>
/// Данные салона красоты в PostgreSQL
/// </summary>
/// <param name="options">Настройки подключения и провайдера.</param>
public class SalonDbContext(DbContextOptions<SalonDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Имя индекса, который запрещает две записи одного мастера в одно время
    /// </summary>
    public const string AppointmentSlotIndex = "ux_appointments_master_datetime";

    /// <summary>
    /// Клиенты салона
    /// </summary>
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>
    /// Мастера салона
    /// </summary>
    public DbSet<Master> Masters => Set<Master>();

    /// <summary>
    /// Услуги салона
    /// </summary>
    public DbSet<Service> Services => Set<Service>();

    /// <summary>
    /// Записи клиентов на приём
    /// </summary>
    public DbSet<Appointment> Appointments => Set<Appointment>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().UseTpcMappingStrategy();
        modelBuilder.Entity<Client>().ToTable("clients");
        modelBuilder.Entity<Master>().ToTable("masters");

        var clients = modelBuilder.Entity<Client>();
        clients.Property(c => c.PhoneNumber).HasMaxLength(20);

        var masters = modelBuilder.Entity<Master>();
        masters.Property(m => m.PassportNumber).HasMaxLength(20);

        var services = modelBuilder.Entity<Service>();
        services.Property(s => s.Price).HasPrecision(10, 2);
        services.Property(s => s.Name).HasMaxLength(200);

        modelBuilder.Entity<Person>().Property(p => p.Gender).HasConversion<string>().HasMaxLength(20);
        services.Property(s => s.Category).HasConversion<string>().HasMaxLength(50);
        masters.Property(m => m.Specialization).HasConversion<string>().HasMaxLength(50);

        var appointments = modelBuilder.Entity<Appointment>();
        appointments.Property(a => a.DateTime).HasColumnType("timestamp without time zone");
        appointments.HasOne(a => a.Master).WithMany().HasForeignKey("master_id").OnDelete(DeleteBehavior.Restrict);
        appointments.HasOne(a => a.Client).WithMany().HasForeignKey("client_id").OnDelete(DeleteBehavior.Restrict);
        appointments.HasOne(a => a.Service).WithMany().HasForeignKey("service_id").OnDelete(DeleteBehavior.Restrict);
        appointments.Navigation(a => a.Master).AutoInclude();
        appointments.Navigation(a => a.Client).AutoInclude();
        appointments.Navigation(a => a.Service).AutoInclude();
        appointments.HasIndex("master_id", nameof(Appointment.DateTime)).IsUnique().HasDatabaseName(AppointmentSlotIndex);
    }
}