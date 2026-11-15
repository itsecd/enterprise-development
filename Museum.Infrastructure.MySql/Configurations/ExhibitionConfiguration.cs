using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Museum.Domain;

namespace Museum.Infrastructure.MySql.Configurations;

/// <summary>
/// Настройки таблицы выставок и её начальные данные
/// </summary>
public sealed class ExhibitionConfiguration : IEntityTypeConfiguration<Exhibition>
{
    /// <summary>
    /// Настраивает таблицу выставок
    /// </summary>
    public void Configure(EntityTypeBuilder<Exhibition> builder)
    {
        builder.ToTable("Exhibitions");
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
        builder.Property(e => e.Theme).HasConversion<string>().HasMaxLength(20);

        builder.HasData(SeedSource.Data.Exhibitions);
    }
}
