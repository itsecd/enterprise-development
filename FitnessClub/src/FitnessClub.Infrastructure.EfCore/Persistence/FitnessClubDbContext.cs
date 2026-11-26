using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.EfCore.Persistence;

/// <summary>
/// Контекст базы данных фитнес-клуба
/// </summary>
public sealed class FitnessClubDbContext(DbContextOptions<FitnessClubDbContext> options) : DbContext(options)
{
    /// <summary>Залы тренировок</summary>
    public DbSet<Hall> Halls => Set<Hall>();
    /// <summary>Специализации тренеров</summary>
    public DbSet<Specialization> Specializations => Set<Specialization>();
    /// <summary>Клиенты фитнес-клуба</summary>
    public DbSet<Client> Clients => Set<Client>();
    /// <summary>Тренеры фитнес-клуба</summary>
    public DbSet<Trainer> Trainers => Set<Trainer>();
    /// <summary>Бронирования тренировок</summary>
    public DbSet<Booking> Bookings => Set<Booking>();
    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FitnessClubDbContext).Assembly);
    }
}   