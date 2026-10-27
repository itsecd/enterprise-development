using Hotel.Domain;
using Hotel.Domain.Data;
using Hotel.Domain.Entities;

namespace Hotel.Infrastructure.InMemory.Repositories;


/// <summary>
/// Репозиторий номеров гостиницы в памяти
/// </summary>
public class RoomRepository : IRepository<Room, int>
{
    /// <summary>
    /// Данные номеров гостиницы
    /// </summary>
    private readonly List<Room> _rooms;

    /// <summary>
    /// Инициализация репозитория данными
    /// </summary>
    public RoomRepository()
    {
        _rooms = HotelData.Rooms.ToList();
    }

    /// <summary>
    /// Получить все номера
    /// </summary>
    public IReadOnlyList<Room> GetAll()
    {
        return _rooms.AsReadOnly();
    }

    /// <summary>
    /// Получить номер по идентификатору
    /// </summary>
    public Room? GetById(int id)
    {
        return _rooms.FirstOrDefault(room => room.Id == id);
    }

    /// <summary>
    /// Добавить новый номер
    /// </summary>
    public Room Add(Room room)
    {
        _rooms.Add(room);

        return room;
    }

    /// <summary>
    /// Обновить существующий номер
    /// </summary>
    public Room? Update(Room room)
    {
        var existingRoom = GetById(room.Id);

        if (existingRoom is null) return null;

        existingRoom.RoomNumber = room.RoomNumber;
        existingRoom.Floor = room.Floor;
        existingRoom.HasBalcony = room.HasBalcony;
        existingRoom.RoomType = room.RoomType;

        return existingRoom;
    }

    /// <summary>
    /// Удалить номер по идентификатору
    /// </summary>
    public bool Delete(int id)
    {
        var room = GetById(id);

        if (room is null) return false;

        return _rooms.Remove(room);
    }
}