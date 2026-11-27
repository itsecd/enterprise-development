using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Entities;

namespace VetClinic.Infrastructure.PostgreSql;

public sealed class VetClinicDbContext(DbContextOptions<VetClinicDbContext> options) : DbContext(options)
{
    public DbSet<Breed> Breeds => Set<Breed>();
    public DbSet<Specialization> Specializations => Set<Specialization>();
    public DbSet<Owner> Owners => Set<Owner>();
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<Veterinarian> Veterinarians => Set<Veterinarian>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pet>(entity =>
        {
            entity.HasOne(p => p.Breed)
                .WithMany()
                .HasForeignKey(p => p.BreedId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(p => p.Owner)
                .WithMany(o => o.Pets)
                .HasForeignKey(p => p.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Veterinarian>()
            .HasOne(v => v.Specialization)
            .WithMany()
            .HasForeignKey(v => v.SpecializationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Appointment>(entity =>
        {
            entity.Property(a => a.DateTime)
                .HasColumnType("timestamp without time zone")
                .HasConversion(
                    value => DateTime.SpecifyKind(value, DateTimeKind.Unspecified),
                    value => value);

            entity.HasOne(a => a.Pet)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PetId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(a => a.Veterinarian)
                .WithMany(v => v.Appointments)
                .HasForeignKey(a => a.VeterinarianId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
