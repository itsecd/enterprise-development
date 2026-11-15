using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Museum.Domain;

namespace Museum.Infrastructure.MySql.Configurations;

/// <summary>
/// Настройки таблицы билетов, связей с экскурсиями и посетителями и начальные данные
/// </summary>
public sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
{
    /// <summary>
    /// Настраивает таблицу билетов
    /// </summary>
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.ToTable("Tickets");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Type).HasConversion<string>().HasMaxLength(20);
        builder.Property(t => t.Price).HasPrecision(10, 2);

        // Экскурсию с билетами и посетителя с билетами удалить нельзя
        builder
            .HasOne(t => t.Tour)
            .WithMany()
            .HasForeignKey("TourId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(t => t.Visitor)
            .WithMany()
            .HasForeignKey("VisitorId")
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasData(SeedSource.Data.Tickets.Select(ticket => new
        {
            ticket.Id,
            ticket.Type,
            ticket.Price,
            TourId = ticket.Tour.Id,
            VisitorId = ticket.Visitor.Id
        }));
    }
}
