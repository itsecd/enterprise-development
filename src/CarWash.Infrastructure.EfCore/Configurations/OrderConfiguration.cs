using CarWash.Domain.Data;
using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarWash.Infrastructure.EfCore.Configurations;

/// <summary>
/// Настройка таблицы заказов
/// </summary>
public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        builder.HasKey(o => o.Id);

        builder.HasOne(o => o.Client)
            .WithMany()
            .HasForeignKey("ClientId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Car)
            .WithMany()
            .HasForeignKey("CarId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(o => o.Service)
            .WithMany()
            .HasForeignKey("ServiceId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(DataSeed.Orders.Select(o => new
        {
            o.Id,
            ClientId = o.Client.Id,
            CarId = o.Car.Id,
            ServiceId = o.Service.Id,
            o.StartTime,
            o.BoxNumber
        }));
    }
}
