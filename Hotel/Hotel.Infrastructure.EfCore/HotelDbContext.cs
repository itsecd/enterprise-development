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
}