using Hotel.Domain.Data;
using Hotel.Domain.Entities;
using Hotel.Domain.Interfaces;

namespace Hotel.Infrastructure.InMemory;

/// <summary>
/// Сессия для репозиториев всех сущностей
/// </summary>
public class HotelSession
{
    public IRepository<Client> Clients { get; }
    public IRepository<Room> Rooms { get; }
    public IRepository<RoomType> RoomTypes { get; }
    public IRepository<Booking> Bookings { get; }

    /// <summary>
    /// Инициализирует новую сессию
    /// </summary>
    public HotelSession()
    {
        var seed = new HotelSeed();

        Clients = new InMemoryRepository<Client>(seed.Clients);
        Rooms = new InMemoryRepository<Room>(seed.Rooms);
        RoomTypes = new InMemoryRepository<RoomType>(seed.RoomTypes);
        Bookings = new InMemoryRepository<Booking>(seed.Bookings);
    }
}