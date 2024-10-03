namespace HotelBookingDetails.Domain;

/// <summary>
/// Комната
/// </summary>
public class Room
{
    /// <summary>
    /// Идентификатор комнаты
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Тип номера
    /// </summary>
    public required TypeRoom Type { get; set; }

    /// <summary>
    /// Вместимость
    /// </summary>
    public required int Capacity { get; set; }

    /// <summary>
    /// Цена
    /// </summary>
    public required int Cost { get; set; }

    /// <summary>
    /// Идентификатор отеля
    /// </summary>
    public required int HotelId { get; set; }
}
