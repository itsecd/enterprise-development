using DryCleaning.Domain;
using DryCleaning.Domain.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DryCleaning.Infrastructure.EfCore.Data.Configurations;

/// <summary>
/// Настройка таблицы категорий изделий 
/// </summary>
public class ItemCategoryConfiguration : IEntityTypeConfiguration<ItemCategory>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<ItemCategory> builder)
    {
        builder.HasKey(c => c.CategoryId);

        builder.Property(c => c.CategoryName)
            .IsRequired()
            .HasMaxLength(50);
            
        builder.Property(c => c.Price)
            .HasPrecision(18, 2);

        builder.HasData(new TestData().ItemCategories);
    }
}
