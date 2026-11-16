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
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyList<RoomType>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult<IReadOnlyList<RoomType>>(_roomTypes.AsReadOnly());
    }

    /// <summary>
    /// Получить тип номера по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор типа номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<RoomType?> GetById(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var roomType = _roomTypes.FirstOrDefault(roomType => roomType.Id == id);

        return Task.FromResult(roomType);
    }

    /// <summary>
    /// Добавить новый тип номера
    /// </summary>
    /// <param name="roomType">Новый тип номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<RoomType> Add(RoomType roomType, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _roomTypes.Add(roomType);

        return Task.FromResult(roomType);
    }

    /// <summary>
    /// Обновить существующий тип номера
    /// </summary>
    /// <param name="roomType">Обновлённые данные типа номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<RoomType?> Update(RoomType roomType, CancellationToken cancellationToken = default)
    {
        var existingRoomType = await GetById(roomType.Id, cancellationToken);

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
    /// <param name="id">Идентификатор типа номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        var roomType = await GetById(id, cancellationToken);

        if (roomType is null) return false;

        return _roomTypes.Remove(roomType);
    }
}