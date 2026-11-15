using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Museum.Domain;

namespace Museum.Infrastructure.MySql.Configurations;

/// <summary>
/// Настройки таблицы экскурсий, связи с выставками и начальные данные
/// </summary>
public sealed class TourConfiguration : IEntityTypeConfiguration<Tour>
{
    /// <summary>
    /// Настраивает таблицу экскурсий и связь «многие ко многим» с выставками
    /// </summary>
    public void Configure(EntityTypeBuilder<Tour> builder)
    {
        builder.ToTable("Tours");
        builder.HasKey(t => t.Id);

        builder
            .HasMany(t => t.Exhibitions)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "TourExhibitions",
                right => right
                    .HasOne<Exhibition>()
                    .WithMany()
                    .HasForeignKey("ExhibitionId")
                    .OnDelete(DeleteBehavior.Restrict),
                left => left
                    .HasOne<Tour>()
                    .WithMany()
                    .HasForeignKey("TourId")
                    .OnDelete(DeleteBehavior.Cascade),
                join =>
                {
                    join.HasKey("TourId", "ExhibitionId");
                    join.HasData(SeedSource.Data.Tours.SelectMany(tour =>
                        tour.Exhibitions.Select(exhibition => new Dictionary<string, object>
                        {
                            ["TourId"] = tour.Id,
                            ["ExhibitionId"] = exhibition.Id
                        })));
                });

        // Связанные выставки передаются через таблицу связи, поэтому здесь только собственные поля
        builder.HasData(SeedSource.Data.Tours.Select(tour => new
        {
            tour.Id,
            tour.Date,
            tour.StartTime,
            tour.DurationMinutes
        }));
    }
}
