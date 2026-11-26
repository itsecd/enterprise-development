using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.Domain.Models;

namespace VetClinic.Infrastructure.SqlServer.Configurations;

/// <summary>
/// Таблица специализаций врачей
/// </summary>
public class SpecializationConfiguration : IEntityTypeConfiguration<Specialization>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).HasMaxLength(100).IsRequired();
        builder.Property(s => s.FocusSpecies).HasConversion<string>().HasMaxLength(20);
    }
}
