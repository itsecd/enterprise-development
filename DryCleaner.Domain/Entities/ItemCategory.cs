using DryCleaner.Shared.Enums;

namespace DryCleaner.Domain.Entities;

/// <summary>
/// Категория изделия
/// </summary>
public class ItemCategory
{
    /// <summary>
    /// Идентификатор категории изделия
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название изделия
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Рекомендованный вид чистки
    /// </summary>
    public CleaningType RecommendedCleaning { get; set; }
    /// <summary>
    /// Цена за химчистку
    /// </summary>
    public decimal Price { get; set; }
}