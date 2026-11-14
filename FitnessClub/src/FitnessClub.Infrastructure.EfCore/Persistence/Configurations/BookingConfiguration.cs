using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.EfCore.Persistence.Configurations;

/// <summary>
/// Конфигурация маппинга бронирования в базу данных
/// </summary>
public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.DateTime).IsRequired();
        builder.Property(b => b.IsTrial);
        builder.Property(b => b.Duration).IsRequired();
        builder.HasOne(b => b.Trainer)
            .WithMany()
            .HasForeignKey("TrainerId")
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Client)
            .WithMany()
            .HasForeignKey("ClientId")
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(b => b.Hall)
            .WithMany()
            .HasForeignKey("HallId")
            .OnDelete(DeleteBehavior.Restrict);
    }
}