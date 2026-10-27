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
    public IReadOnlyList<RoomType> GetAll()
    {
        return _roomTypes.AsReadOnly();
    }

    /// <summary>
    /// Получить тип номера по идентификатору
    /// </summary>
    public RoomType? GetById(int id)
    {
        return _roomTypes.FirstOrDefault(roomType => roomType.Id == id);
    }

    /// <summary>
    /// Добавить новый тип номера
    /// </summary>
    public RoomType Add(RoomType roomType)
    {
        _roomTypes.Add(roomType);

        return roomType;
    }

    /// <summary>
    /// Обновить существующий тип номера
    /// </summary>
    public RoomType? Update(RoomType roomType)
    {
        var existingRoomType = GetById(roomType.Id);

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
    public bool Delete(int id)
    {
        var roomType = GetById(id);

        if (roomType is null) return false;

        return _roomTypes.Remove(roomType);
    }
}