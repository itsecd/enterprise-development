using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VetClinic.Domain.Models;

namespace VetClinic.Infrastructure.SqlServer.Configurations;

/// <summary>
/// Таблица записей на прием, записи удаляются вместе с питомцем или врачом
/// </summary>
public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.RoomNumber).HasMaxLength(10).IsRequired();
        builder.HasIndex(a => new { a.RoomNumber, a.DateTime });

        builder.HasOne(a => a.Pet)
            .WithMany()
            .HasForeignKey(a => a.PetId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.Vet)
            .WithMany()
            .HasForeignKey(a => a.VetId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
