namespace HotelBookingSystem.Domain;

/// <summary>
/// Номер в отеле
/// </summary>
public class Room
{
    /// <summary>
    /// Идентификатор номера
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Тип номера
    /// </summary>
    public required string TypeRoom { get; set; }

    /// <summary>
    /// Количество номеров такого типа
    /// </summary>
    public required int Number { get; set; }

    /// <summary>
    /// Cтоимость за ночь
    /// </summary>
    public required int Price { get; set; }

    /// <summary>
    /// Идентификатор отеля
    /// </summary>
    public required int HotelID { get; set; }

    /// <summary>
    /// Бронь этого номера 
    /// </summary>
    public List<BookedRoom> BookedRooms { get; set; } = [];
}