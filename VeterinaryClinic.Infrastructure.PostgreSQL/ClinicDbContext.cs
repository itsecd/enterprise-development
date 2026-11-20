using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Data;

namespace VeterinaryClinic.Infrastructure.PostgreSQL;

public sealed class ClinicDbContext(DbContextOptions<ClinicDbContext> options) : DbContext(options)
{
    public DbSet<Breed> Breeds => Set<Breed>();
    public DbSet<Specialization> Specializations => Set<Specialization>();
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<Veterinarian> Veterinarians => Set<Veterinarian>();
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Breed>(b =>
        {
            b.ToTable("breeds");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityByDefaultColumn().HasIdentityOptions(startValue: 1000);
            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.AnimalType).HasConversion<string>();
        });
        modelBuilder.Entity<Specialization>(b =>
        {
            b.ToTable("specializations");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityByDefaultColumn().HasIdentityOptions(startValue: 1000);
            b.Property(x => x.Name).IsRequired();
            b.Property(x => x.AnimalType).HasConversion<string>();
        });
        modelBuilder.Entity<Owner>(b =>
        {
            b.ToTable("owners");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityByDefaultColumn().HasIdentityOptions(startValue: 1000);
            b.HasMany(x => x.Pets).WithOne(x => x.Owner).HasForeignKey("OwnerId").OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<Veterinarian>(b =>
        {
            b.ToTable("veterinarians");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityByDefaultColumn().HasIdentityOptions(startValue: 1000);
            b.HasOne(x => x.Specialization).WithMany().HasForeignKey("SpecializationId").OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<Pet>(b =>
        {
            b.ToTable("pets");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityByDefaultColumn().HasIdentityOptions(startValue: 1000);
            b.HasOne(x => x.Breed).WithMany().HasForeignKey("BreedId").OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Owner).WithMany(x => x.Pets).HasForeignKey("OwnerId").OnDelete(DeleteBehavior.SetNull);
        });
        modelBuilder.Entity<Appointment>(b =>
        {
            b.ToTable("appointments");
            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityByDefaultColumn().HasIdentityOptions(startValue: 1000);
            b.HasOne(x => x.Pet).WithMany().HasForeignKey("PetId").OnDelete(DeleteBehavior.Restrict);
            b.HasOne(x => x.Veterinarian).WithMany().HasForeignKey("VeterinarianId").OnDelete(DeleteBehavior.Restrict);
            b.Property(x => x.AppointmentDate).HasColumnType("timestamp without time zone");
            b.HasIndex(x => new { x.AppointmentDate, x.RoomNumber }).IsUnique();
        });

        // Фиксированные начальные данные включаются в миграцию EF Core.
        modelBuilder.Entity<Breed>().HasData(VeterinaryClinicTestData.Breeds);
        modelBuilder.Entity<Specialization>().HasData(VeterinaryClinicTestData.Specializations);
        modelBuilder.Entity<Owner>().HasData(VeterinaryClinicTestData.Owners.Select(x => new
        {
            x.Id,
            x.LastName,
            x.FirstName,
            x.Patronymic,
            x.Phone,
            x.Address
        }));
        modelBuilder.Entity<Veterinarian>().HasData(VeterinaryClinicTestData.Veterinarians.Select(x => new
        {
            x.Id,
            x.LastName,
            x.FirstName,
            x.Patronymic,
            x.Phone,
            x.PassportNumber,
            x.BirthDate,
            x.ExperienceYears,
            SpecializationId = x.Specialization.Id
        }));
        modelBuilder.Entity<Pet>().HasData(VeterinaryClinicTestData.Pets.Select(x => new
        {
            x.Id,
            x.Name,
            x.BirthDate,
            x.Weight,
            BreedId = x.Breed.Id,
            OwnerId = x.Owner?.Id
        }));
        modelBuilder.Entity<Appointment>().HasData(VeterinaryClinicTestData.Appointments.Select(x => new
        {
            x.Id,
            x.AppointmentDate,
            x.RoomNumber,
            x.IsRepeated,
            PetId = x.Pet.Id,
            VeterinarianId = x.Veterinarian.Id
        }));

        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entity.GetProperties())
                property.SetColumnName(ToSnakeCase(property.Name));
        }
    }

    private static string ToSnakeCase(string name)
        => System.Text.RegularExpressions.Regex.Replace(name, "([a-z0-9])([A-Z])", "$1_$2").ToLowerInvariant();
}
