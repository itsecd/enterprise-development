using Hotel.Domain.Shared.Enums;

namespace Hotel.Domain.Entities;

/// <summary>
/// Справочная информация о комнате
/// </summary>
public class RoomType
{
    /// <summary>
    /// Идентификатор комнаты
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Площадь комнаты
    /// </summary>
    public required double Area { get; set; }

    /// <summary>
    /// Колличество кроватей
    /// </summary>
    public required uint Beds { get; set; }

    /// <summary>
    /// Наличие ванной
    /// </summary>
    public required bool HasBath { get; set; }

    /// <summary>
    /// Наличие душа
    /// </summary>
    public required bool HasShower { get; set; }

    /// <summary>
    /// Цена прожиания за сутки
    /// </summary>
    public required double DayPrice { get; set; }

    /// <summary>
    /// Категория комнаты
    /// </summary>
    public required RoomCategory Category { get; set; }
}