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
    public Task<IReadOnlyList<Room>> GetAll()
    {
        return Task.FromResult<IReadOnlyList<Room>>(_rooms.AsReadOnly());
    }

    /// <summary>
    /// Получить номер по идентификатору
    /// </summary>
    public Task<Room?> GetById(int id)
    {
        return Task.FromResult(_rooms.FirstOrDefault(room => room.Id == id));
    }

    /// <summary>
    /// Добавить новый номер
    /// </summary>
    public Task<Room> Add(Room room)
    {
        _rooms.Add(room);

        return Task.FromResult(room);
    }

    /// <summary>
    /// Обновить существующий номер
    /// </summary>
    public async Task<Room?> Update(Room room)
    {
        var existingRoom = await GetById(room.Id);

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
    public async Task<bool> Delete(int id)
    {
        var room = await GetById(id);

        if (room is null) return false;

        return _rooms.Remove(room);
    }
}