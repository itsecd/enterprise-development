namespace HotelSystem.Domain.Models;

/// <summary>
/// Бронирование номера клиентом
/// </summary>
public class Booking
{
    /// <summary>
    /// Уникальный идентификатор бронирования
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Клиент, оформивший бронирование
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Забронированный номер
    /// </summary>
    public required Room Room { get; set; }

    /// <summary>
    /// Дата заселения клиента
    /// </summary>
    public DateTime CheckInDate { get; set; }

    /// <summary>
    /// Количество дней проживания
    /// </summary>
    public int NumberOfDays { get; set; }

    /// <summary>
    /// Полная стоимость проживания
    /// </summary>
    public decimal TotalCost =>
        Room.RoomType.PricePerDay * NumberOfDays;
}
