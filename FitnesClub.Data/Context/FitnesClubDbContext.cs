using FitnesClub.Data.Data;
using FitnesClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FitnesClub.Data.Context;

/// <summary>
/// Контекст базы данных фитнес-клуба для MS SQL Server.
/// </summary>
/// <param name="options">Параметры конфигурации контекста.</param>
public class FitnesClubDbContext(DbContextOptions<FitnesClubDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Таблица тренеров клуба.
    /// </summary>
    public DbSet<Trainer> Trainers => Set<Trainer>();

    /// <summary>
    /// Таблица клиентов клуба.
    /// </summary>
    public DbSet<Member> Members => Set<Member>();

    /// <summary>
    /// Таблица записей на персональные тренировки.
    /// </summary>
    public DbSet<Booking> Bookings => Set<Booking>();

    /// <summary>
    /// Справочник специализаций тренеров.
    /// </summary>
    public DbSet<Specialization> Specializations => Set<Specialization>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Конфигурация справочника специализаций
        modelBuilder.Entity<Specialization>(entity =>
        {
            entity.ToTable("Specializations");
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasData(DataSeeder.Specializations);
        });

        //Конфигурация таблицы тренеров
        modelBuilder.Entity<Trainer>(entity =>
        {
            entity.ToTable("Trainers");
            entity.HasKey(t => t.Id);

            // Поля, унаследованные от Person
            entity.Property(t => t.PassportNumber).IsRequired().HasMaxLength(20);
            entity.Property(t => t.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(t => t.LastName).IsRequired().HasMaxLength(100);
            entity.Property(t => t.PhoneNumber).IsRequired().HasMaxLength(25);
            entity.Property(t => t.Email).HasMaxLength(150);
            entity.Property(t => t.Gender).HasConversion<string>().HasMaxLength(20);

            // Связь: у одной специализации много тренеров
            entity.HasOne(t => t.Specialization)
                .WithMany(s => s.Trainers)
                .HasForeignKey(t => t.SpecializationId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasData(DataSeeder.Trainers);
        });

        //Конфигурация таблицы клиентов
        modelBuilder.Entity<Member>(entity =>
        {
            entity.ToTable("Members");
            entity.HasKey(m => m.Id);

            // Поля, унаследованные от Person
            entity.Property(m => m.PassportNumber).IsRequired().HasMaxLength(20);
            entity.Property(m => m.FirstName).IsRequired().HasMaxLength(100);
            entity.Property(m => m.LastName).IsRequired().HasMaxLength(100);
            entity.Property(m => m.PhoneNumber).IsRequired().HasMaxLength(25);
            entity.Property(m => m.Email).HasMaxLength(150);
            entity.Property(m => m.Gender).HasConversion<string>().HasMaxLength(20);

            entity.HasData(DataSeeder.Members);
        });

        //Конфигурация таблицы записей на занятия
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.ToTable("Bookings");
            entity.HasKey(b => b.Id);

            entity.Property(b => b.RoomName)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(b => b.Trainer)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TrainerId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(b => b.Member)
                .WithMany(m => m.Bookings)
                .HasForeignKey(b => b.MemberId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasData(DataSeeder.Bookings);
        });
    }
}