namespace Hotel.Domain.Entities;

/// <summary>
/// Номер в гостинице
/// </summary>
public class Room
{
    /// <summary>
    /// Идентификатор номера
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Номер комнаты
    /// </summary>
    public int RoomNumber { get; set; }

    /// <summary>
    /// Этаж, на котором расположен номер 
    /// </summary>
    public int Floor { get; set; }

    /// <summary>
    /// Наличие балкона в номере
    /// </summary>
    public bool HasBalcony { get; set; }

    /// <summary>
    /// Тип номера
    /// </summary>
    public required RoomType RoomType { get; set; }
}