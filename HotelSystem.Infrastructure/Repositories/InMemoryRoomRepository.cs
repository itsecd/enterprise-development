using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;
using HotelSystem.Infrastructure.Storage;

namespace HotelSystem.Infrastructure.Repositories;

/// <summary>
/// Репозиторий номеров гостиницы с хранением данных в памяти
/// </summary>
public class InMemoryRoomRepository(HotelInMemoryStorage storage)
    : IRepository<Room>
{
    /// <inheritdoc />
    public Task<List<Room>> GetAllAsync() =>
        Task.FromResult(storage.Rooms);

    /// <inheritdoc />
    public Task<Room?> GetByIdAsync(int id) =>
        Task.FromResult(
            storage.Rooms.FirstOrDefault(room => room.Id == id));

    /// <inheritdoc />
    public Task<Room> AddAsync(Room room)
    {
        room.Id = storage.Rooms.Count == 0
            ? 1
            : storage.Rooms.Max(existing => existing.Id) + 1;

        storage.Rooms.Add(room);

        return Task.FromResult(room);
    }

    /// <inheritdoc />
    public Task<bool> UpdateAsync(int id, Room room)
    {
        var index = storage.Rooms.FindIndex(existing => existing.Id == id);

        if (index == -1)
        {
            return Task.FromResult(false);
        }

        room.Id = id;
        storage.Rooms[index] = room;

        return Task.FromResult(true);
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int id)
    {
        var room = storage.Rooms.FirstOrDefault(
            existing => existing.Id == id);

        return Task.FromResult(
            room is not null && storage.Rooms.Remove(room));
    }
}