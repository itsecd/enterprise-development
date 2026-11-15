using Microsoft.EntityFrameworkCore;
using Vet.Domain.DataSeed;
using Vet.Domain.Entities;

namespace Vet.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных ветклиники
/// </summary>
/// <param name="options">Настройки контекста базы данных</param>
public class VetDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    /// Справочник пород
    /// </summary>
    public DbSet<Breed> Breeds { get; set; }

    /// <summary>
    /// Справочник специализаций
    /// </summary>
    public DbSet<Specialization> Specializations { get; set; }

    /// <summary>
    /// Виды животных по специализациям
    /// </summary>
    public DbSet<SpecializationSpecies> SpecializationSpecies { get; set; }

    /// <summary>
    /// Ветеринары
    /// </summary>
    public DbSet<Veterinarian> Veterinarians { get; set; }

    /// <summary>
    /// Владельцы питомцев
    /// </summary>
    public DbSet<Owner> Owners { get; set; }

    /// <summary>
    /// Питомцы
    /// </summary>
    public DbSet<Pet> Pets { get; set; }

    /// <summary>
    /// Записи на приём
    /// </summary>
    public DbSet<Appointment> Appointments { get; set; }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var fixture = new VetClinicTestFixture();

        modelBuilder.Entity<Breed>(builder =>
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Species).IsRequired();
            builder.Property(b => b.Name).HasMaxLength(100).IsRequired();
            builder.HasData(fixture.Breeds);
        });

        modelBuilder.Entity<Specialization>(builder =>
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
            builder.HasIndex(s => s.Name).IsUnique();
            builder.HasMany(s => s.Species)
                .WithOne()
                .HasForeignKey(ss => ss.SpecializationId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(fixture.Specializations.Select(s => new
            {
                s.Id,
                s.Name
            }));
        });

        modelBuilder.Entity<SpecializationSpecies>(builder =>
        {
            builder.HasKey(ss => new { ss.SpecializationId, ss.Species });
            builder.Property(ss => ss.Species).IsRequired();
            builder.HasData(fixture.Specializations.SelectMany(s => s.Species));
        });

        modelBuilder.Entity<Owner>(builder =>
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.FullName).HasMaxLength(150).IsRequired();
            builder.Property(o => o.Address).HasMaxLength(250);
            builder.Property(o => o.PhoneNumber).HasMaxLength(25).IsRequired();
            builder.HasMany(o => o.Pets)
                .WithOne(p => p.Owner)
                .HasForeignKey("OwnerId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(fixture.Owners.Select(o => new
            {
                o.Id,
                o.FullName,
                o.Address,
                o.PhoneNumber
            }));
        });

        modelBuilder.Entity<Pet>(builder =>
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
            builder.Property(p => p.Species).IsRequired();
            builder.HasOne(p => p.Breed)
                .WithMany()
                .HasForeignKey("BreedId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(p => p.Appointments)
                .WithOne(a => a.Pet)
                .HasForeignKey("PetId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(fixture.Pets.Select(p => new
            {
                p.Id,
                p.Name,
                p.Species,
                BreedId = p.Breed.Id,
                p.BirthDate,
                p.BodyWeight,
                OwnerId = p.Owner.Id
            }));
        });

        modelBuilder.Entity<Veterinarian>(builder =>
        {
            builder.HasKey(v => v.Id);
            builder.Property(v => v.PassportNumber).HasMaxLength(20).IsRequired();
            builder.HasIndex(v => v.PassportNumber).IsUnique();
            builder.Property(v => v.FullName).HasMaxLength(150).IsRequired();
            builder.Property(v => v.BirthYear).IsRequired();
            builder.Property(v => v.WorkExperience).IsRequired();
            builder.HasOne(v => v.Specialization)
                .WithMany()
                .HasForeignKey("SpecializationId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(v => v.Appointments)
                .WithOne(a => a.Veterinarian)
                .HasForeignKey("VeterinarianId")
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(fixture.Veterinarians.Select(v => new
            {
                v.Id,
                v.PassportNumber,
                v.FullName,
                SpecializationId = v.Specialization.Id,
                v.BirthYear,
                v.WorkExperience
            }));
        });

        modelBuilder.Entity<Appointment>(builder =>
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.AppointmentDateTime).IsRequired();
            builder.Property(a => a.RoomNumber).IsRequired();
            builder.Property(a => a.IsRepeat).IsRequired();
            builder.HasIndex(a => new { a.RoomNumber, a.AppointmentDateTime });
            builder.HasData(fixture.Appointments.Select(a => new
            {
                a.Id,
                PetId = a.Pet.Id,
                VeterinarianId = a.Veterinarian.Id,
                a.AppointmentDateTime,
                a.RoomNumber,
                a.IsRepeat
            }));
        });
    }
}