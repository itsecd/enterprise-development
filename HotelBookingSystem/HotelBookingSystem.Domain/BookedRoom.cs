namespace HotelBookingSystem.Domain;

/// <summary>
/// Забронированный номер
/// </summary>
public class BookedRoom
{
    /// <summary>
    /// Идентификатор забронированного номера
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public required int ClientId { get; set; }

    /// <summary>
    /// Идентификатор номера в отеле
    /// </summary>
    public required int RoomId { get; set; }

    /// <summary>
    /// Дата въезда
    /// </summary>
    public required DateOnly EntryDate { get; set; }

    /// <summary>
    /// Дата выезда
    /// </summary>
    public required DateOnly DepartureDate { get; set; }

    /// <summary>
    /// Срок бронирования
    /// </summary>
    public required TimeSpan BookingPeriod { get; set; }
}