using Microsoft.EntityFrameworkCore;
using MuseumSystem.Domain.Entities;

namespace MuseumSystem.Api.Data;

public class MuseumDbContext(DbContextOptions<MuseumDbContext> options) : DbContext(options)
{
    public DbSet<Exhibition> Exhibitions => Set<Exhibition>();
    public DbSet<Excursion> Excursions => Set<Excursion>();
    public DbSet<Visitor> Visitors => Set<Visitor>();
    public DbSet<Ticket> Tickets => Set<Ticket>();
    public DbSet<ExcursionExhibition> ExcursionExhibitions => Set<ExcursionExhibition>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Exhibition
        modelBuilder.Entity<Exhibition>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Theme).IsRequired();
            entity.Property(e => e.HallNumber).IsRequired();
            entity.Property(e => e.StartDate).IsRequired();
            entity.Property(e => e.EndDate).IsRequired();
        });

        // Visitor
        modelBuilder.Entity<Visitor>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Phone).IsRequired().HasMaxLength(20);
            entity.Property(e => e.BirthDate).IsRequired();
        });

        // Ticket
        modelBuilder.Entity<Ticket>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Type).IsRequired();
            entity.Property(e => e.Price).HasPrecision(18, 2);

            entity.HasOne(e => e.Excursion)
                  .WithMany()
                  .HasForeignKey(e => e.ExcursionId)
                  .IsRequired();

            entity.HasOne(e => e.Visitor)
                  .WithMany()
                  .HasForeignKey(e => e.VisitorId)
                  .IsRequired();
        });

        // Excursion + Настройка связи много-много
        modelBuilder.Entity<Excursion>(entity =>
        {
            entity.HasKey(e => e.Id);
            // время БЕЗ часового пояса для Постгре
            entity.Property(e => e.StartsAt).IsRequired().HasColumnType("timestamp without time zone");
            entity.Property(e => e.DurationMinutes).IsRequired();

            entity.HasMany(e => e.Exhibitions)
                  .WithMany()
                  .UsingEntity<ExcursionExhibition>(
                      j => j.HasOne(ee => ee.Exhibition).WithMany().HasForeignKey(ee => ee.ExhibitionId),
                      j => j.HasOne(ee => ee.Excursion).WithMany().HasForeignKey(ee => ee.ExcursionId),
                      j =>
                      {
                          j.HasKey(ee => new { ee.ExcursionId, ee.ExhibitionId });
                          j.HasData(SeedData.ExcursionExhibitions);
                      });
        });

        // Сидинг основных таблиц
        modelBuilder.Entity<Exhibition>().HasData(SeedData.Exhibitions);
        modelBuilder.Entity<Visitor>().HasData(SeedData.Visitors);
        modelBuilder.Entity<Excursion>().HasData(SeedData.Excursions);
        modelBuilder.Entity<Ticket>().HasData(SeedData.Tickets);
    }
}