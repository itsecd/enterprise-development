namespace Hotel.Domain.Entities;

/// <summary>
/// Информация о комнате
/// </summary>
public class Room
{
    /// <summary>
    /// Идентификатор комнаты
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер комнаты
    /// </summary>
    public required uint Number { get; set; }

    /// <summary>
    /// Этаж расположения
    /// </summary>
    public required uint Floor { get; set; }

    /// <summary>
    /// Наличие балкона
    /// </summary>
    public required bool HasBalcony { get; set; }

    /// <summary>
    /// Тип комнаты
    /// </summary>
    public required RoomType Type { get; set; }
}