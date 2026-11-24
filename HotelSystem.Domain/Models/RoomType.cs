using HotelSystem.Domain.Shared.Enums;

namespace HotelSystem.Domain.Models;

/// <summary>
/// Тип номера
/// </summary>
public class RoomType
{
    /// <summary>
    /// Уникальный идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Категория
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
    /// Наличие ванны или душа
    /// </summary>
    public bool HasBathOrShower { get; set; }

    /// <summary>
    /// Стоимость проживания за сутки
    /// </summary>
    public decimal PricePerDay { get; set; }
}
