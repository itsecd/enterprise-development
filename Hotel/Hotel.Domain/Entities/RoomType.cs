using Hotel.Domain.Shared.Enums;

namespace Hotel.Domain.Entities;

/// <summary>
/// Тип номера гостиницы
/// </summary>
public class RoomType
{
    /// <summary>
    /// Идентификатор типа номера
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Категория номера
    /// </summary>
    public RoomCategory Category { get; set; }

    /// <summary>
    /// Площадь номера в квадратных метрах
    /// </summary>
    public double Area { get; set; }

    /// <summary>
    /// Количество спальных мест
    /// </summary>
    public int BedCount { get; set; }

    /// <summary>
    /// Наличие ванной комнаты
    /// </summary>
    public bool HasBathroom { get; set; }

    /// <summary>
    /// Стоимость проживания за один день
    /// </summary>
    public decimal PricePerDay { get; set; }
}