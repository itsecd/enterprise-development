namespace Hotel.Domain.Models;

/// <summary>
/// Гостиничный номер
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
    public required string RoomNumber { get; set; }

    /// <summary>
    /// Этаж
    /// </summary>
    public required int Floor { get; set; }

    /// <summary>
    /// Наличие балкона
    /// </summary>
    public required bool HasBalcony { get; set; }

    /// <summary>
    /// Ссылка на тип номера
    /// </summary>
    public required RoomType Type { get; set; }
}