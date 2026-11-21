using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.EfCore.Persistence.Configurations;

/// <summary>
/// Конфигурация маппинга зала в базу данных
/// </summary>
public class HallConfiguration : IEntityTypeConfiguration<Hall>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Hall> builder)
    {
        builder.Property(h => h.Name).HasMaxLength(200).IsRequired();
        builder.HasIndex(h => h.Name).IsUnique();

        builder.ToTable("halls");
    }
}