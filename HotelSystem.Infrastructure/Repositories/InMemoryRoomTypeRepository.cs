using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.Storage;

namespace HotelSystem.Infrastructure.Repositories;

/// <summary>
/// Репозиторий типов номеров гостиницы с хранением данных в памяти
/// </summary>
public class InMemoryRoomTypeRepository(HotelInMemoryStorage storage)
    : IRepository<RoomType>
{
    /// <inheritdoc />
    public List<RoomType> GetAll() => storage.RoomTypes;

    /// <inheritdoc />
    public RoomType? GetById(int id) =>
        storage.RoomTypes.FirstOrDefault(roomType => roomType.Id == id);

    /// <inheritdoc />
    public RoomType Add(RoomType roomType)
    {
        roomType.Id = storage.RoomTypes.Count == 0
            ? 1
            : storage.RoomTypes.Max(existing => existing.Id) + 1;

        storage.RoomTypes.Add(roomType);

        return roomType;
    }

    /// <inheritdoc />
    public bool Update(int id, RoomType roomType)
    {
        var index = storage.RoomTypes.FindIndex(existing => existing.Id == id);

        if (index == -1)
        {
            return false;
        }

        roomType.Id = id;
        storage.RoomTypes[index] = roomType;

        return true;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var roomType = GetById(id);

        return roomType is not null && storage.RoomTypes.Remove(roomType);
    }
}