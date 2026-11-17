using FitnessGym.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessGym.Infrastructure.PostgreSql;

/// <summary>
/// Контекст PostgreSQL; правила отображения сущностей находятся в отдельных конфигурациях.
/// </summary>
public class FitnessGymDbContext(DbContextOptions<FitnessGymDbContext> options) : DbContext(options)
{
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<Trainer> Trainers => Set<Trainer>();
    public DbSet<Specialization> Specializations => Set<Specialization>();
    public DbSet<TrainingSession> Sessions => Set<TrainingSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FitnessGymDbContext).Assembly);
}
