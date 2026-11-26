using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.Domain.Models;

namespace VetClinic.Infrastructure.SqlServer.Configurations;

/// <summary>
/// Таблица пород
/// </summary>
public class BreedConfiguration : IEntityTypeConfiguration<Breed>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).HasMaxLength(100).IsRequired();
        builder.Property(b => b.Species).HasConversion<string>().HasMaxLength(20);
    }
}
