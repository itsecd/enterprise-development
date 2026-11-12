namespace Hotel.Domain.Entities;

/// <summary>
/// Бронирование номера гостиницы клиентом
/// </summary>
public class Booking
{
    /// <summary>
    /// Идентификатор бронирования
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор клиента, который забронировал номер
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Клиент, который забронировал номер
    /// </summary>
    public required HotelClient Client { get; set; }

    /// <summary>
    /// Идентификатор забронированного номера
    /// </summary>
    public int RoomId { get; set; }

    /// <summary> 
    /// Забронированный номер гостиницы
    /// </summary>
    public required Room Room { get; set; }

    /// <summary>
    /// Дата заселения клиента
    /// </summary>
    public DateOnly CheckInDate { get; set; }

    /// <summary>
    /// Количество дней проживания
    /// </summary>
    public int DaysCount { get; set; }
}
