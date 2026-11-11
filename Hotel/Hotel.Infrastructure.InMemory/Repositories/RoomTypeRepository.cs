using Hotel.Domain;
using Hotel.Domain.Data;
using Hotel.Domain.Entities;

namespace Hotel.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий типов номеров в памяти
/// </summary>
public class RoomTypeRepository : IRepository<RoomType, int>
{
    /// <summary>
    /// Данные типов номеров гостиницы
    /// </summary>
    private readonly List<RoomType> _roomTypes;

    /// <summary>
    /// Инициализация репозитория исходными данными
    /// </summary>
    public RoomTypeRepository()
    {
        _roomTypes = HotelData.RoomTypes.ToList();
    }

    /// <summary>
    /// Получить все типы номеров
    /// </summary>
    public Task<IReadOnlyList<RoomType>> GetAll()
    {
        return Task.FromResult<IReadOnlyList<RoomType>>(_roomTypes.AsReadOnly());
    }

    /// <summary>
    /// Получить тип номера по идентификатору
    /// </summary>
    public Task<RoomType?> GetById(int id)
    {
        var roomType = _roomTypes.FirstOrDefault(roomType => roomType.Id == id);

        return Task.FromResult(roomType);
    }

    /// <summary>
    /// Добавить новый тип номера
    /// </summary>
    public Task<RoomType> Add(RoomType roomType)
    {
        _roomTypes.Add(roomType);

        return Task.FromResult(roomType);
    }

    /// <summary>
    /// Обновить существующий тип номера
    /// </summary>
    public async Task<RoomType?> Update(RoomType roomType)
    {
        var existingRoomType = await GetById(roomType.Id);

        if (existingRoomType is null) return null;

        existingRoomType.Category = roomType.Category;
        existingRoomType.Area = roomType.Area;
        existingRoomType.BedCount = roomType.BedCount;
        existingRoomType.HasBathroom = roomType.HasBathroom;
        existingRoomType.PricePerDay = roomType.PricePerDay;

        return existingRoomType;
    }

    /// <summary>
    /// Удалить тип номера по идентификатору
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var roomType = await GetById(id);

        if (roomType is null) return false;

        return _roomTypes.Remove(roomType);
    }
}