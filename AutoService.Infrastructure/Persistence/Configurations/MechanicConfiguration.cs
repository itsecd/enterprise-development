using AutoService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoService.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация таблицы механиков
/// </summary>
public class MechanicConfiguration : IEntityTypeConfiguration<Mechanic>
{
    /// <summary>
    /// Настроить отображение механика на таблицу базы данных
    /// </summary>
    public void Configure(EntityTypeBuilder<Mechanic> builder)
    {
        builder.ToTable("mechanics");

        builder.HasKey(m => m.Id);

        builder.Property(m => m.Passport).HasMaxLength(30);
        builder.HasIndex(m => m.Passport).IsUnique();
        builder.Property(m => m.FullName).HasMaxLength(200);

        builder.Property(m => m.Specialization)
            .HasConversion<string>()
            .HasMaxLength(50);
    }
}
