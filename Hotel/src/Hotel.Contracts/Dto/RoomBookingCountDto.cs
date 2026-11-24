namespace Hotel.Contracts.Dto;

/// <summary>
/// Количество бронирований гостиничного номера
/// </summary>
public class RoomBookingCountDto
{
    /// <summary>
    /// Идентификатор номера
    /// </summary>
    public int RoomId { get; set; }

    /// <summary>
    /// Номер комнаты
    /// </summary>
    public required string RoomNumber { get; set; }

    /// <summary>
    /// Количество бронирований
    /// </summary>
    public int BookingCount { get; set; }
}
