using Hotel.Domain.Interfaces;

namespace Hotel.Domain.Entities;

/// <summary>
/// Гостиничный номер
/// </summary>
public class Room : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Номер гостиничной комнаты
    /// </summary>
    public required int Number { get; set; }

    /// <summary>
    /// Этаж комнаты
    /// </summary>
    public required int Floor { get; set; }

    /// <summary>
    /// Наличие балкона
    /// </summary>
    public required bool HasBalcony { get; set; }

    /// <summary>
    /// Тип гостиничного номера
    /// </summary>
    public required RoomType RoomType { get; set; }

}
