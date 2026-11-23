using Hotel.Domain;
using Hotel.Domain.Entities;

/// <summary>
/// Наследник базового репозитория для бронирований с доп методом
/// </summary>
public interface IBookingRepository : IRepository<Booking, int>
{
    /// <summary>
    /// Проверка что номер свободен на заданное время
    /// </summary>
    /// <param name="roomId">Id номера</param>
    /// <param name="checkIn">Дата заезда</param>
    /// <param name="days">Кол-во дней проживания</param>
    /// <param name="ct">Токен отмены операции</param>
    /// <returns>Допустимость брони</returns>
    public Task<bool> HasOverlappingBookingAsync(int roomId, DateOnly checkIn, uint days, CancellationToken ct = default);
}