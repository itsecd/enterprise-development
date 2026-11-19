namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для обновления данных вида работ.
/// </summary>
public class WorkTypeUpdateDto
{
    /// <summary>
    /// Название работы.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Категория работы.
    /// </summary>
    public required string Category { get; set; }

    /// <summary>
    /// Стоимость работы.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Продолжительность работы.
    /// </summary>
    public TimeSpan Duration { get; set; }
}
