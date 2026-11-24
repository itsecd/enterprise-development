namespace HotelSystem.Domain.Models;

/// <summary>
/// Номер гостиницы
/// </summary>
public class Room
{
    /// <summary>
    /// Уникальный идентификатор номера
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер комнаты
    /// </summary>
    public int RoomNumber { get; set; }

    /// <summary>
    /// Этаж, на котором расположен номер
    /// </summary>
    public int Floor { get; set; }

    /// <summary>
    /// Наличие балкона
    /// </summary>
    public bool HasBalcony { get; set; }

    /// <summary>
    /// Тип номера
    /// </summary>
    public required RoomType RoomType { get; set; }
}
