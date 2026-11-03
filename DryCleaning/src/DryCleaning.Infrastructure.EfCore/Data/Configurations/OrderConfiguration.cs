using DryCleaning.Domain;
using DryCleaning.Domain.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DryCleaning.Infrastructure.EfCore.Data.Configurations;

/// <summary>
/// Настройка таблицы заказов
/// </summary>
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);

        builder.HasOne(o => o.Client)
            .WithMany()
            .HasForeignKey("ClientId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.HasOne(o => o.Item)
            .WithMany()
            .HasForeignKey("ItemId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        builder.Property(o => o.AcceptDate)
            .HasConversion(
                v => v,
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.Property(o => o.CompletedDate)
            .HasConversion(
                v => v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v);

        builder.HasData(new TestData().Orders.Select(o => new
        {
            o.Id,
            o.Client.ClientId,
            o.Item.ItemId,
            o.Status,
            o.ProcessingDays,
            o.AcceptDate,
            o.CompletedDate
        }));
    }
}
