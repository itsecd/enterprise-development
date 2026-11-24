
using Hotel.Domain.Data;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Models;

namespace Hotel.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий типов номеров с хранением данных в памяти.
/// </summary>
/// <param name="data">Общий набор данных гостиницы.</param>
public class RoomTypeRepository(TestData data) : IRoomTypeRepository
{
    private readonly TestData _data = data;

    public Task<IReadOnlyList<RoomType>> GetAllAsync()
    {
        lock (_data.RoomTypes)
            return Task.FromResult<IReadOnlyList<RoomType>>(
                _data.RoomTypes.ToList());
    }

    public Task<RoomType?> GetByIdAsync(int id)
    {
        lock (_data.RoomTypes)
            return Task.FromResult(
                _data.RoomTypes.FirstOrDefault(type => type.Id == id));
    }

    public Task<RoomType> AddAsync(RoomType roomType)
    {
        lock (_data.RoomTypes)
        {
            roomType.Id = _data.RoomTypes
                .Select(item => item.Id)
                .DefaultIfEmpty(0)
                .Max() + 1;

            _data.RoomTypes.Add(roomType);
        }

        return Task.FromResult(roomType);
    }

    public Task<bool> UpdateAsync(RoomType roomType)
    {
        lock (_data.RoomTypes)
        {
            var existing = _data.RoomTypes
                .FirstOrDefault(item => item.Id == roomType.Id);

            if (existing is null)
                return Task.FromResult(false);

            existing.Category = roomType.Category;
            existing.Area = roomType.Area;
            existing.BedCount = roomType.BedCount;
            existing.HasBathroom = roomType.HasBathroom;
            existing.PricePerNight = roomType.PricePerNight;

            return Task.FromResult(true);
        }
    }

    public Task<bool> DeleteAsync(int id)
    {
        lock (_data.RoomTypes)
        {
            var roomType = _data.RoomTypes
                .FirstOrDefault(item => item.Id == id);

            return Task.FromResult(
                roomType is not null && _data.RoomTypes.Remove(roomType));
        }
    }
}
