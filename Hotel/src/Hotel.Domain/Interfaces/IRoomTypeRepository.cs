using Hotel.Domain.Models;

namespace Hotel.Domain.Interfaces;

/// <summary>
/// Описывает доступ к справочнику типов номеров
/// </summary>
public interface IRoomTypeRepository
{
    /// <summary>
    /// Возвращает все типы номеров
    /// </summary>
    public Task<IReadOnlyList<RoomType>> GetAllAsync();

    /// <summary>
    /// Возвращает тип номера по идентификатору
    /// </summary>
    public Task<RoomType?> GetByIdAsync(int id);

    /// <summary>
    /// Добавляет тип номера
    /// </summary>
    public Task<RoomType> AddAsync(RoomType roomType);

    /// <summary>
    /// Обновляет тип номера
    /// </summary>
    public Task<bool> UpdateAsync(RoomType roomType);

    /// <summary>
    /// Удаляет тип номера
    /// </summary>
    public Task<bool> DeleteAsync(int id);
}
