using System.ComponentModel.DataAnnotations;
using Hotel.Domain.Enums;

namespace Hotel.Contracts.Dto;

/// <summary>
/// Данные для создания или изменения типа номера
/// </summary>
public class RoomTypeEditDto
{
    /// <summary>
    /// Категория номера
    /// </summary>
    [EnumDataType(typeof(RoomCategory))]
    public required RoomCategory Category { get; set; }

    /// <summary>
    /// Площадь в квадратных метрах
    /// </summary>
    [Range(1, 1000)]
    public double Area { get; set; }

    /// <summary>
    /// Количество кроватей
    /// </summary>
    [Range(1, 20)]
    public int BedCount { get; set; }

    /// <summary>
    /// Наличие ванны или душа
    /// </summary>
    public bool HasBathroom { get; set; }

    /// <summary>
    /// Стоимость проживания за сутки
    /// </summary>
    [Range(typeof(decimal), "0.01", "100000000")]
    public decimal PricePerNight { get; set; }
}
