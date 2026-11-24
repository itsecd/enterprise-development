using Hotel.Domain.Enums;

namespace Hotel.Contracts.Dto;

/// <summary>
/// Данные типа номера в ответе API
/// </summary>
public class RoomTypeDto
{
    /// <summary>
    /// Идентификатор типа номера
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Категория номера
    /// </summary>
    public RoomCategory Category { get; set; }

    /// <summary>
    /// Площадь в квадратных метрах
    /// </summary>
    public double Area { get; set; }

    /// <summary>
    /// Количество кроватей
    /// </summary>
    public int BedCount { get; set; }

    /// <summary>
    /// Наличие ванны или душа
    /// </summary>
    public bool HasBathroom { get; set; }

    /// <summary>
    /// Стоимость проживания за сутки
    /// </summary>
    public decimal PricePerNight { get; set; }
}
