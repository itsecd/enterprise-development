namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для обновления данных вида работ.
/// </summary>
public class WorkTypeUpdateDto
{
    /// <summary>
    /// Название работы.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Категория работы.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Стоимость работы.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Продолжительность работы.
    /// </summary>
    public TimeSpan Duration { get; set; }
}