using Hotel.Domain.Data;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Models;

namespace Hotel.Infrastructure.InMemory.Repositories;

/// <summary>Репозиторий гостиничных номеров с хранением данных в памяти.</summary>
/// <param name="data">Общий набор данных гостиницы.</param>
public class RoomRepository(TestData data) : IRoomRepository
{
    private readonly TestData _data = data;

    public Task<IReadOnlyList<Room>> GetAllAsync()
    {
        lock (_data.Rooms)
            return Task.FromResult<IReadOnlyList<Room>>(_data.Rooms.ToList());
    }

    public Task<Room?> GetByIdAsync(int id)
    {
        lock (_data.Rooms)
            return Task.FromResult(_data.Rooms.FirstOrDefault(room => room.Id == id));
    }

    public Task<Room> AddAsync(Room room)
    {
        lock (_data.Rooms)
        {
            room.Id = _data.Rooms.Select(item => item.Id).DefaultIfEmpty(0).Max() + 1;
            _data.Rooms.Add(room);
        }

        return Task.FromResult(room);
    }

    public Task<bool> UpdateAsync(Room room)
    {
        lock (_data.Rooms)
        {
            var existing = _data.Rooms.FirstOrDefault(item => item.Id == room.Id);
            if (existing is null)
                return Task.FromResult(false);

            existing.RoomNumber = room.RoomNumber;
            existing.Floor = room.Floor;
            existing.HasBalcony = room.HasBalcony;
            existing.Type = room.Type;
            return Task.FromResult(true);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_data.Rooms)
        {
            var room = _data.Rooms.FirstOrDefault(item => item.Id == id);
            return Task.FromResult(room is not null && _data.Rooms.Remove(room));
        }
    }

    public Task<bool> HasRoomsForTypeAsync(int roomTypeId)
    {
        lock (_data.Rooms)
            return Task.FromResult(_data.Rooms.Any(room => room.Type.Id == roomTypeId));
    }
}
