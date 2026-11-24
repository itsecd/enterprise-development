using HotelSystem.Domain.Data;
using HotelSystem.Domain.Models;

namespace HotelSystem.Infrastructure.Storage;

/// <summary>
/// Единое хранилище данных гостиницы в памяти
/// </summary>
public class HotelInMemoryStorage
{
    /// <summary>
    /// Список клиентов гостиницы
    /// </summary>
    public List<Client> Clients { get; } = HotelTestData.Clients;

    /// <summary>
    /// Список типов номеров
    /// </summary>
    public List<RoomType> RoomTypes { get; } = HotelTestData.RoomTypes;

    /// <summary>
    /// Список номеров гостиницы
    /// </summary>
    public List<Room> Rooms { get; } = HotelTestData.Rooms;

    /// <summary>
    /// Список бронирований
    /// </summary>
    public List<Booking> Bookings { get; } = HotelTestData.Bookings;
}