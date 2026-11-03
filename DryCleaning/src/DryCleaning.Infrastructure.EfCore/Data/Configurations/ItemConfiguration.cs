using DryCleaning.Domain;
using DryCleaning.Domain.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DryCleaning.Infrastructure.EfCore.Data.Configurations;

/// <summary>
/// Настройка таблицы изделий
/// </summary>
public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.ItemId);

        builder.HasOne(i => i.Category)
            .WithMany()
            .HasForeignKey("CategoryId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.Material)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(new TestData().Items.Select(i => new
        {
            i.ItemId,
            i.Name,
            i.Material,
            i.Category.CategoryId,
        }));
    }
}