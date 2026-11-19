using AutoService.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoService.Infrastructure.Persistence.Configurations;

/// <summary>
/// Конфигурация таблицы заказов
/// </summary>
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    /// <summary>
    /// Название связующей таблицы со списком работ заказа
    /// </summary>
    public const string JoinTableName = "order_works";

    /// <summary>
    /// Настроить отображение заказа на таблицу базы данных
    /// </summary>
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("orders");

        builder.HasKey(o => o.Id);

        builder.Ignore(o => o.TotalCost);

        builder.HasOne(o => o.Car)
            .WithMany()
            .HasForeignKey(o => o.CarId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Client)
            .WithMany()
            .HasForeignKey(o => o.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Mechanic)
            .WithMany()
            .HasForeignKey(o => o.MechanicId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(o => o.Works)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                JoinTableName,
                j => j.HasOne<WorkType>()
                    .WithMany()
                    .HasForeignKey("work_type_id")
                    .OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Order>()
                    .WithMany()
                    .HasForeignKey("order_id")
                    .OnDelete(DeleteBehavior.Cascade));
    }
}
