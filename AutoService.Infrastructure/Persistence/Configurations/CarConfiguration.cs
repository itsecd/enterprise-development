using AutoService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoService.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация таблицы автомобилей
/// </summary>
public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    /// <summary>
    /// Настроить отображение автомобиля на таблицу базы данных
    /// </summary>
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.ToTable("cars");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.LicensePlate).HasMaxLength(20);
        builder.HasIndex(c => c.LicensePlate).IsUnique();
        builder.Property(c => c.Brand).HasMaxLength(50);
        builder.Property(c => c.Model).HasMaxLength(50);
    }
}
