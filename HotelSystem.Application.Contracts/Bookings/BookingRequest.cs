using System.ComponentModel.DataAnnotations;

namespace HotelSystem.Application.Contracts.Bookings;

/// <summary>
/// Данные бронирования для создания и обновления
/// </summary>
public class BookingRequest
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int ClientId { get; init; }

    /// <summary>
    /// Идентификатор номера
    /// </summary>
    [Range(1, int.MaxValue)]
    public required int RoomId { get; init; }

    /// <summary>
    /// Дата заезда
    /// </summary>
    public required DateTime CheckInDate { get; init; }

    /// <summary>
    /// Количество суток проживания
    /// </summary>
    [Range(1, 365)]
    public required int NumberOfDays { get; init; }
}