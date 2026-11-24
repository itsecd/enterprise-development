using Hotel.Domain.Models;

namespace Hotel.Domain.Interfaces;

/// <summary>
/// Описывает доступ к бронированиям
/// </summary>
public interface IBookingRepository
{
    /// <summary>
    /// Возвращает все бронирования
    /// </summary>
    public Task<IReadOnlyList<Booking>> GetAllAsync();

    /// <summary>
    /// Возвращает бронирование по идентификатору
    /// </summary>
    public Task<Booking?> GetByIdAsync(int id);

    /// <summary>
    /// Добавляет бронирование
    /// </summary>
    public Task<Booking> AddAsync(Booking booking);

    /// <summary>
    /// Обновляет бронирование
    /// </summary>
    public Task<bool> UpdateAsync(Booking booking);

    /// <summary>
    /// Удаляет бронирование
    /// </summary>
    public Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Проверяет наличие бронирований клиента
    /// </summary>
    public Task<bool> HasBookingsForClientAsync(int clientId);

    /// <summary>
    /// Проверяет наличие бронирований номера
    /// </summary>
    public Task<bool> HasBookingsForRoomAsync(int roomId);
}
