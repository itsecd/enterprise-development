using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnessClub.Infrastructure.PostgreSql.Data;

/// <summary>
/// Entity Framework-контекст базы данных фитнес-клуба
/// </summary>
/// <param name="options">Параметры подключения (провайдер Npgsql, строка соединения)</param>
public class FitnessClubDbContext(DbContextOptions<FitnessClubDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Специализации тренеров
    /// </summary>
    public DbSet<Specialization> Specializations { get; set; }

    /// <summary>
    /// Тренеры
    /// </summary>
    public DbSet<Trainer> Trainers { get; set; }

    /// <summary>
    /// Клиенты
    /// </summary>
    public DbSet<Client> Clients { get; set; }

    /// <summary>
    /// Записи клиентов на занятия
    /// </summary>
    public DbSet<Lesson> Lessons { get; set; }

    /// <summary>
    /// Конфигурация схемы БД: ограничения полей, внешние ключи и seed-данные
    /// </summary>
    /// <param name="modelBuilder">Построитель модели</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetColumnType("timestamp without time zone");
                }
            }
        }

        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.Property(s => s.Name)
                .HasMaxLength(100)
                .IsRequired();

            entity.HasData(SeedData.Specializations);
        });

        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.Property(t => t.PassportNumber)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(t => t.FullName)
                .HasMaxLength(150)
                .IsRequired();

            entity.HasOne(t => t.Specialization)
                .WithMany()
                .HasForeignKey(t => t.SpecializationId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(SeedData.Trainers);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.Property(c => c.PassportNumber)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(c => c.FullName)
                .HasMaxLength(150)
                .IsRequired();

            entity.Property(c => c.Phone)
                .HasMaxLength(20)
                .IsRequired();

            entity.HasData(SeedData.Clients);
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.Property(l => l.Hall)
                .HasMaxLength(100);

            entity.HasOne(l => l.Client)
                .WithMany()
                .HasForeignKey(l => l.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(l => l.Trainer)
                .WithMany()
                .HasForeignKey(l => l.TrainerId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(SeedData.Lessons);
        });
    }
}
