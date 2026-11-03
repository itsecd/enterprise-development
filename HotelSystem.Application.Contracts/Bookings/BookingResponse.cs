namespace HotelSystem.Application.Contracts.Bookings;

/// <summary>
/// Данные бронирования, возвращаемые API
/// </summary>
public class BookingResponse : IEntityResponse
{
    /// <summary>
    /// Идентификатор бронирования
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; init; }

    /// <summary>
    /// Идентификатор номера
    /// </summary>
    public int RoomId { get; init; }

    /// <summary>
    /// Дата заезда
    /// </summary>
    public DateTime CheckInDate { get; init; }

    /// <summary>
    /// Количество суток проживания
    /// </summary>
    public int NumberOfDays { get; init; }

    /// <summary>
    /// Итоговая стоимость проживания
    /// </summary>
    public decimal TotalCost { get; init; }
}