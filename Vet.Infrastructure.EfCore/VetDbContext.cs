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
            builder.HasData(fixture.Breeds);
        });

        modelBuilder.Entity<Specialization>(builder =>
        {
            builder.HasKey(s => s.Id);
            builder.HasData(fixture.Specializations);
        });

        modelBuilder.Entity<Owner>(builder =>
        {
            builder.HasKey(o => o.Id);
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
            })
                );
        });

        modelBuilder.Entity<Pet>(builder =>
        {
            builder.HasKey(p => p.Id);

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

