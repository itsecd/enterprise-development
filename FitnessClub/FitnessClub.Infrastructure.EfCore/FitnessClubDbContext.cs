using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.EfCore;

/// <summary>Контекст PostgreSQL для данных фитнес-клуба.</summary>
public class FitnessClubDbContext(DbContextOptions<FitnessClubDbContext> options) : DbContext(options)
{
    /// <summary>Клиенты клуба.</summary>
    public DbSet<Client> Clients => Set<Client>();
    /// <summary>Тренеры клуба.</summary>
    public DbSet<Trainer> Trainers => Set<Trainer>();
    /// <summary>Специализации тренеров.</summary>
    public DbSet<Specialization> Specializations => Set<Specialization>();
    /// <summary>Залы клуба.</summary>
    public DbSet<Hall> Halls => Set<Hall>();
    /// <summary>Записи на персональные занятия.</summary>
    public DbSet<Training> Trainings => Set<Training>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FitnessClubDbContext).Assembly);
        SeedData.Configure(modelBuilder);
    }
}
