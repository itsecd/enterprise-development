using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Entities;
using VetClinic.Domain.Seeder;
namespace VetClinic.EfCore;

/// <summary>
/// Контекст базы данных ветклиники
/// </summary>
/// <param name="options">Настройки контекста базы данных</param>
public class VetClinicDbContext(DbContextOptions options) : DbContext(options)
{
    /// <summary>
    /// Справочник пород
    /// </summary>
    public DbSet<Breed> Breeds { get; set; }
    /// <summary>
    /// Справочник владельцев
    /// </summary>
    public DbSet<Owner> Owners { get; set; }
    /// <summary>
    /// Справочник специализаций
    /// </summary>
    public DbSet<Specialization> Specializations { get; set; }
    /// <summary>
    /// Справочник питомцев
    /// </summary>
    public DbSet<Pet> Pets { get; set; }
    /// <summary>
    /// Справочник ветеринаров
    /// </summary>
    public DbSet<Vet> Vets { get; set; }
    /// <summary>
    /// Справочник услуг
    /// </summary>
    public DbSet<Service> Services { get; set; }
    /// <summary>
    /// Справочник записей на приём
    /// </summary>
    public DbSet<Appointment> Appointments { get; set; }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = "Server=localhost;Database=vetclinicdb;User=root;Password=root;";
            optionsBuilder.UseMySql(connectionString, new MySqlServerVersion(new Version(9, 7, 0)));
        }
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var seeder = new VetClinicDataSeeder();
        modelBuilder.Entity<Breed>(builder =>
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Type).IsRequired();
            builder.Property(b => b.Title).HasMaxLength(100).IsRequired();
            builder.HasData(seeder.Breeds);
        });

        modelBuilder.Entity<Specialization>(builder =>
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Title).HasMaxLength(100).IsRequired();
            builder.HasIndex(s => s.Title).IsUnique();
            builder.Property(e => e.TargetSpecies).HasConversion<string>().HasMaxLength(50);
            builder.HasData(seeder.Specializations);

        });

        modelBuilder.Entity<Owner>(builder =>
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.FullName).IsRequired().HasMaxLength(150);
            builder.Property(o => o.Phone).IsRequired().HasMaxLength(30);
            builder.Property(o => o.Adress).IsRequired().HasMaxLength(250);
            builder.HasData(seeder.Owners);
        });

        modelBuilder.Entity<Pet>(builder =>
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Type).HasConversion<string>().HasMaxLength(50);

            builder.HasOne(p => p.Breed)
                  .WithMany()
                  .HasForeignKey(p => p.BreedId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Owner)
                  .WithMany()
                  .HasForeignKey(p => p.OwnerId)
                  .OnDelete(DeleteBehavior.Cascade);
            builder.HasData(seeder.Pets.Select(p => new Pet
            {
                Id = p.Id,
                Name = p.Name,
                Type = p.Type,
                BirthDate = p.BirthDate,
                Weight = p.Weight,
                BreedId = p.BreedId,
                OwnerId = p.OwnerId,
                Owner = null!
            }));
        });

        modelBuilder.Entity<Vet>(builder =>
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FullName).IsRequired().HasMaxLength(150);
            builder.Property(e => e.PassportNumber).IsRequired().HasMaxLength(20);

            builder.HasOne(v => v.Specialization)
                  .WithMany()
                  .HasForeignKey(v => v.SpecializationId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasData(seeder.Vets.Select(v => new Vet
            {
                Id = v.Id,
                FullName = v.FullName,
                PassportNumber = v.PassportNumber,
                BirthDate = v.BirthDate,
                ExpirienceYears = v.ExpirienceYears,
                SpecializationId = v.SpecializationId,
                Specialization = null!
            }));
        });

        modelBuilder.Entity<Service>(builder =>
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title).IsRequired().HasMaxLength(150);
            builder.Property(e => e.Price).HasPrecision(10, 2);

            builder.HasOne(s => s.Specialization)
                  .WithMany()
                  .HasForeignKey(s => s.SpecializationId)
                  .OnDelete(DeleteBehavior.Restrict);
            builder.HasData(seeder.Services.Select(s => new Service
            {
                Id = s.Id,
                Title = s.Title,
                Price = s.Price,
                SpecializationId = s.SpecializationId,
                Specialization = null!
            }));
        });

        modelBuilder.Entity<Appointment>(builder =>
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Diagnosis).HasMaxLength(500);
            builder.Property(e => e.RepeatVisitIndicator).HasConversion<string>().HasMaxLength(20);

            builder.HasOne(a => a.Pet)
                  .WithMany()
                  .HasForeignKey(a => a.PetId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(a => a.Vet)
                  .WithMany()
                  .HasForeignKey(a => a.VetId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(a => a.Service)
                  .WithMany()
                  .HasForeignKey(a => a.ServiceId)
                  .OnDelete(DeleteBehavior.Restrict);
            builder.HasData(seeder.Appointments.Select(a => new Appointment
            {
                Id = a.Id,
                DateTime = a.DateTime,
                Diagnosis = a.Diagnosis,
                RoomNumber = a.RoomNumber,
                RepeatVisitIndicator = a.RepeatVisitIndicator,
                PetId = a.PetId,
                Pet = null!,
                VetId = a.VetId,
                Vet = null!,
                ServiceId = a.ServiceId,
                Service = null!
            }));
        });
    }



}
