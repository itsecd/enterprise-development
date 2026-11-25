using BeautySalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BeautySalon.Infrastructure.SqlServer;

public sealed class SalonDbContext(DbContextOptions<SalonDbContext> options) : DbContext(options)
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<BeautySpecialist> Specialists => Set<BeautySpecialist>();
    public DbSet<Service> Services => Set<Service>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Person>();

        var customer = modelBuilder.Entity<Customer>();
        customer.ToTable("Customers");
        ConfigurePerson(customer);
        customer.HasData(BeautySalonData.Customers);

        var specialist = modelBuilder.Entity<BeautySpecialist>();
        specialist.ToTable("Specialists");
        ConfigurePerson(specialist);
        specialist.Property(item => item.PassportNumber).HasMaxLength(20).UseCollation("Latin1_General_100_CI_AS");
        specialist.Property(item => item.Specialization).HasMaxLength(100);
        specialist.HasIndex(item => item.PassportNumber).IsUnique().HasFilter("[PassportNumber] IS NOT NULL");
        specialist.HasData(BeautySalonData.Specialists);

        var service = modelBuilder.Entity<Service>();
        service.ToTable("Services");
        service.Property(item => item.Name).HasMaxLength(150);
        service.Property(item => item.Category).HasMaxLength(100);
        service.Property(item => item.Price).HasPrecision(18, 2);
        service.HasData(BeautySalonData.Services);

        var appointment = modelBuilder.Entity<Appointment>();
        appointment.ToTable("Appointments");
        appointment.HasOne(item => item.Customer).WithMany().HasForeignKey("CustomerId").OnDelete(DeleteBehavior.Restrict);
        appointment.HasOne(item => item.Specialist).WithMany().HasForeignKey("SpecialistId").OnDelete(DeleteBehavior.Restrict);
        appointment.HasOne(item => item.Service).WithMany().HasForeignKey("ServiceId").OnDelete(DeleteBehavior.Restrict);
        appointment.HasIndex("SpecialistId", nameof(Appointment.AppointmentDateTime));
        appointment.HasIndex("CustomerId", nameof(Appointment.AppointmentDateTime));
        appointment.HasData(BeautySalonData.Appointments.Select(item => new
        {
            item.Id,
            CustomerId = item.Customer.Id,
            SpecialistId = item.Specialist.Id,
            ServiceId = item.Service.Id,
            item.AppointmentDateTime,
            item.IsRegularCustomer
        }));
    }

    private static void ConfigurePerson<TPerson>(EntityTypeBuilder<TPerson> person) where TPerson : Person
    {
        person.Property(item => item.LastName).HasMaxLength(100);
        person.Property(item => item.FirstName).HasMaxLength(100);
        person.Property(item => item.Patronymic).HasMaxLength(100);
        person.Property(item => item.PhoneNumber).HasMaxLength(30);
    }
}
