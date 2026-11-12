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
    /// <summary>
    /// Задаёт ключ и ограничения полей изделия, связь с категорией
    /// через внешний ключ CategoryId и добавляет начальные данные из TestData
    /// </summary>
    /// <param name="builder">Построитель конфигурации таблицы</param>
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.HasKey(i => i.ItemId);

        builder.HasOne(i => i.Category)
            .WithMany()
            .HasForeignKey(i => i.CategoryId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.Property(i => i.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(i => i.Material)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(TestData.Instance.Items.Select(i => new
        {
            i.ItemId,
            i.Name,
            i.Material,
            i.Category.CategoryId,
        }));
    }
}