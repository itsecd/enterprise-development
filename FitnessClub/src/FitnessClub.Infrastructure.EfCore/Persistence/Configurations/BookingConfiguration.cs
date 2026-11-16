using FitnessClub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FitnessClub.Infrastructure.EfCore.Persistence.Configurations;

/// <summary>
/// Конфигурация маппинга бронирования в базу данных
/// </summary>
public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
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
        builder.Navigation(booking => booking.Client).AutoInclude();
        builder.Navigation(booking => booking.Trainer).AutoInclude();   
        builder.Navigation(booking => booking.Hall).AutoInclude();

        builder.ToTable("bookings");
    }
}