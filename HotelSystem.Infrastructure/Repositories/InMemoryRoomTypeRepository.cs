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
    public Task<List<RoomType>> GetAllAsync() =>
        Task.FromResult(storage.RoomTypes);

    /// <inheritdoc />
    public Task<RoomType?> GetByIdAsync(int id) =>
        Task.FromResult(
            storage.RoomTypes.FirstOrDefault(roomType => roomType.Id == id));

    /// <inheritdoc />
    public Task<RoomType> AddAsync(RoomType roomType)
    {
        roomType.Id = storage.RoomTypes.Count == 0
            ? 1
            : storage.RoomTypes.Max(existing => existing.Id) + 1;

        storage.RoomTypes.Add(roomType);

        return Task.FromResult(roomType);
    }

    /// <inheritdoc />
    public Task<bool> UpdateAsync(int id, RoomType roomType)
    {
        var index = storage.RoomTypes.FindIndex(existing => existing.Id == id);

        if (index == -1)
        {
            return Task.FromResult(false);
        }

        roomType.Id = id;
        storage.RoomTypes[index] = roomType;

        return Task.FromResult(true);
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int id)
    {
        var roomType = storage.RoomTypes.FirstOrDefault(
            existing => existing.Id == id);

        return Task.FromResult(
            roomType is not null && storage.RoomTypes.Remove(roomType));
    }
}