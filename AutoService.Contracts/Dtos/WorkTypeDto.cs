namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для передачи данных о виде работ.
/// </summary>
public class WorkTypeDto
{
    /// <summary>
    /// Идентификатор вида работ.
    /// </summary>
    public Guid Id { get; set; }

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
