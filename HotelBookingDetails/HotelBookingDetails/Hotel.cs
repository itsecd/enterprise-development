namespace HotelBookingDetails.Domain;

/// <summary>
///     Отель
/// </summary>
public class Hotel

{
    /// <summary>
    ///     Название
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Адрес
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    ///    Город
    /// </summary>
    public required string City { get; set; }

    /// <summary>
    ///     Список комнат
    /// </summary>
    public List<Room> Rooms { get; set; } = [];
}
