namespace HotelSystem.Application.Contracts.Analytics;

/// <summary>
/// Информация о количестве бронирований номера
/// </summary>
public class RoomBookingCountResponse
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