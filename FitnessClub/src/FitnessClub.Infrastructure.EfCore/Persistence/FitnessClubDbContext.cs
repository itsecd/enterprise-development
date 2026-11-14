using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.EfCore.Persistence;

public sealed class FitnessClubDbContext(DbContextOptions<FitnessClubDbContext> options) : DbContext(options)
{
    public DbSet<Hall> Halls => Set<Hall>();
    public DbSet<Specialization> Specializations => Set<Specialization>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Trainer> Trainers => Set<Trainer>();
    public DbSet<Booking> Bookings => Set<Booking>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().ToTable("people");
        modelBuilder.Entity<Hall>().ToTable("halls");
        modelBuilder.Entity<Specialization>().ToTable("specializations");
        modelBuilder.Entity<Booking>().ToTable("bookings");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FitnessClubDbContext).Assembly);
    }
}   