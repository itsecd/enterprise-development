using Hotel.Domain.Enums;
using Hotel.Domain.Interfaces;

namespace Hotel.Domain.Entities;

/// <summary>
/// Тип гостиничного номера
/// </summary>
public class RoomType : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Категория гостиничного номера
    /// </summary>
    public required RoomCategory Category { get; set; }

    /// <summary>
    /// Площадь гостиничного номера
    /// </summary>
    public required int RoomArea { get; set; }

    /// <summary>
    /// Количество кроватей
    /// </summary>
    public required int BedCount { get; set; }

    /// <summary>
    /// Наличие ванны/душа
    /// </summary>
    public required bool HasBathroom { get; set; }

    /// <summary>
    /// Цена комнаты за сутки проживания (в рублях)
    /// </summary>
    public required decimal Price { get; set; }

}
