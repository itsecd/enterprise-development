using Hotel.Domain.Data;
using Hotel.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hotel.Infrastructure.EfCore;

/// <summary>
/// Контекст базы данных гостиницы
/// </summary>
public class HotelDbContext(DbContextOptions<HotelDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Типы номеров
    /// </summary>
    public DbSet<RoomType> RoomTypes => Set<RoomType>();

    /// <summary>
    /// Номера гостиницы
    /// </summary>
    public DbSet<Room> Rooms => Set<Room>();

    /// <summary>
    /// Клиенты гостиницы
    /// </summary>
    public DbSet<HotelClient> HotelClients => Set<HotelClient>();

    /// <summary>
    /// Бронирования номеров
    /// </summary>
    public DbSet<Booking> Bookings => Set<Booking>();
    
    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Room>()
            .HasOne(room => room.RoomType)
            .WithMany()
            .HasForeignKey(room => room.RoomTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Booking>()
            .HasOne(booking => booking.Client)
            .WithMany()
            .HasForeignKey(booking => booking.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Booking>()
            .HasOne(booking => booking.Room)
            .WithMany()
            .HasForeignKey(booking => booking.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RoomType>().HasData(HotelData.RoomTypes);

        modelBuilder.Entity<HotelClient>().HasData(HotelData.Clients);

        modelBuilder.Entity<Room>().HasData(HotelData.Rooms.Select(room => new
        {
            room.Id,
            room.RoomNumber,
            room.Floor,
            room.HasBalcony,
            room.RoomTypeId
        }));

        modelBuilder.Entity<Booking>().HasData(HotelData.Bookings.Select(booking => new
        {
            booking.Id,
            booking.ClientId,
            booking.RoomId,
            booking.CheckInDate,
            booking.DaysCount
        }));
    }
}