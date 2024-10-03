namespace HotelBookingDetails.Domain;

/// <summary>
/// Отель
/// </summary>
public class Hotel
{
    /// <summary>
    /// Идентификатор отеля
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Адрес
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Город
    /// </summary>
    public required string City { get; set; }

}
