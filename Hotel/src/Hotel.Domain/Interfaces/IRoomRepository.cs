using Hotel.Domain.Models;

namespace Hotel.Domain.Interfaces;

/// <summary>
/// Описывает доступ к номерам гостиницы
/// </summary>
public interface IRoomRepository
{
    /// <summary>
    /// Возвращает все номера
    /// </summary>
    public Task<IReadOnlyList<Room>> GetAllAsync();

    /// <summary>
    /// Возвращает номер по идентификатору
    /// </summary>
    public Task<Room?> GetByIdAsync(int id);

    /// <summary>
    /// Добавляет номер
    /// </summary>
    public Task<Room> AddAsync(Room room);

    /// <summary>
    /// Обновляет номер
    /// </summary>
    public Task<bool> UpdateAsync(Room room);

    /// <summary>
    /// Удаляет номер
    /// </summary>
    public Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Проверяет, используется ли тип номера
    /// </summary>
    public Task<bool> HasRoomsForTypeAsync(int roomTypeId);
}
