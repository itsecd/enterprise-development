using Hotel.Domain.Data;
using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.Database;

/// <summary>
/// Контекст базы данных гостиницы для связи сущностей с таблицами
/// </summary>
public class HotelDbContext(DbContextOptions<HotelDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Клиенты
    /// </summary>
    public DbSet<Client> Clients => Set<Client>();

    /// <summary>
    /// Типы номеров
    /// </summary>
    public DbSet<RoomType> RoomTypes => Set<RoomType>();

    /// <summary>
    /// Номера гостиницы
    /// </summary>
    public DbSet<Room> Rooms => Set<Room>();

    /// <summary>
    /// Бронирования
    /// </summary>
    public DbSet<Booking> Bookings => Set<Booking>();

    /// <summary>
    /// Настройка типов данных при создании модели
    /// </summary>
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTime>()
            .HaveColumnType("timestamp without time zone");
    }

    /// <summary>
    /// Настройка таблиц и связей в базе данных
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Price).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.Patronymic).HasMaxLength(100);
            entity.Property(e => e.NumberPassport).HasMaxLength(50);
            entity.Property(e => e.Citizenship).HasMaxLength(50);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property<int>("RoomTypeId");
            entity.HasOne(r => r.RoomType)
                  .WithMany()
                  .HasForeignKey("RoomTypeId")
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);

            entity.Navigation(r => r.RoomType).AutoInclude();
        });

        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property<int>("ClientId");
            entity.Property<int>("RoomId");

            entity.HasOne(b => b.Client)
                  .WithMany()
                  .HasForeignKey("ClientId")
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(b => b.Room)
                  .WithMany()
                  .HasForeignKey("RoomId")
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Restrict);

            entity.Navigation(b => b.Client).AutoInclude();
            entity.Navigation(b => b.Room).AutoInclude();
        });

        SeedData(modelBuilder);
    }

    /// <summary>
    /// Заполнение базы данных тестовыми данными
    /// </summary>
    private static void SeedData(ModelBuilder modelBuilder)
    {
        var seed = new HotelSeed();

        modelBuilder.Entity<RoomType>().HasData(seed.RoomTypes);
        modelBuilder.Entity<Client>().HasData(seed.Clients);

        modelBuilder.Entity<Room>().HasData(seed.Rooms.Select(r => new
        {
            r.Id,
            r.Number,
            r.Floor,
            r.HasBalcony,
            RoomTypeId = r.RoomType.Id
        }));

        modelBuilder.Entity<Booking>().HasData(seed.Bookings.Select(b => new
        {
            b.Id,
            b.DateArrival,
            b.DayCount,
            ClientId = b.Client.Id,
            RoomId = b.Room.Id
        }));
    }
}
