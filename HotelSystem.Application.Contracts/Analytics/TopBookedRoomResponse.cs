namespace HotelSystem.Application.Contracts.Analytics;

/// <summary>
/// Информация о номере и количестве его бронирований
/// </summary>
public class TopBookedRoomResponse
{
    /// <summary>
    /// Номер комнаты в гостинице
    /// </summary>
    public int RoomNumber { get; set; }

    /// <summary>
    /// Количество бронирований номера
    /// </summary>
    public int BookingCount { get; set; }
}