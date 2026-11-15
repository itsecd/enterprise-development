using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Museum.Domain;

namespace Museum.Infrastructure.MySql.Configurations;

/// <summary>
/// Настройки таблицы посетителей и её начальные данные
/// </summary>
public sealed class VisitorConfiguration : IEntityTypeConfiguration<Visitor>
{
    /// <summary>
    /// Настраивает таблицу посетителей
    /// </summary>
    public void Configure(EntityTypeBuilder<Visitor> builder)
    {
        builder.ToTable("Visitors");
        builder.HasKey(v => v.Id);

        builder.Property(v => v.FullName).IsRequired().HasMaxLength(200);
        builder.Property(v => v.Phone).IsRequired().HasMaxLength(32);

        builder.HasData(SeedSource.Data.Visitors);
    }
}
