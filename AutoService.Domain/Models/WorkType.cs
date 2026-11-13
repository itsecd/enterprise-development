namespace AutoService.Domain.Models;

/// <summary>
/// Вид работ в автосервисе
/// </summary>
public class WorkType
{
    /// <summary>
    /// Уникальный идентификатор вида работ
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Название работы
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Категория работы
    /// </summary>
    public required string Category { get; set; }

    /// <summary>
    /// Стоимость работы, р.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Продолжительность работы
    /// </summary>
    public TimeSpan Duration { get; set; }
}