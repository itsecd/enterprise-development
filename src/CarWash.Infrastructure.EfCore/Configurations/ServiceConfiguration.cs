using CarWash.Domain.Data;
using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarWash.Infrastructure.EfCore.Configurations;

/// <summary>
/// Настройка таблицы услуг
/// </summary>
public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("Services");
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Name).IsRequired().HasMaxLength(200);

        builder.Property(s => s.Category).HasConversion<string>().HasMaxLength(20);
        builder.Property(s => s.Price).HasPrecision(18, 2);

        builder.HasData(DataSeed.Services);
    }
}
