using AutoService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoService.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация таблицы видов работ
/// </summary>
public class WorkTypeConfiguration : IEntityTypeConfiguration<WorkType>
{
    /// <summary>
    /// Настроить отображение вида работ на таблицу базы данных
    /// </summary>
    public void Configure(EntityTypeBuilder<WorkType> builder)
    {
        builder.ToTable("work_types");

        builder.HasKey(w => w.Id);

        builder.Property(w => w.Name).HasMaxLength(200);
        builder.Property(w => w.Category).HasMaxLength(50);
        builder.Property(w => w.Price).HasPrecision(10, 2);
    }
}
