using System.ComponentModel.DataAnnotations;

namespace Hotel.Contracts.Request;

/// <summary>
/// Обработка запросов на создание/обновление бронирования
/// </summary>
public sealed class BookingRequest
{
    /// <summary>
    /// Дата заселения
    /// </summary>
    [Required]
    public DateTime? DateArrival { get; init; }

    /// <summary>
    /// Количество дней проживания
    /// </summary>
    [Required, Range(1, 3650)]
    public int? DayCount { get; init; }

    /// <summary>
    /// Id клиента
    /// </summary>
    [Required, Range(1, int.MaxValue)]
    public int? ClientId { get; init; }

    /// <summary>
    /// Id комнаты
    /// </summary>
    [Required, Range(1, int.MaxValue)]
    public int? RoomId { get; init; }
}