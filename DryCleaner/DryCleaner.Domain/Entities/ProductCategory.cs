namespace DryCleaner.Domain.Entities;

/// <summary>
/// Категория изделия 
/// </summary>
public class ProductCategory
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название категории 
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Рекомендуемый тип чистки
    /// </summary>
    public string? RecommendedCleaningType { get; set; }

    /// <summary>
    /// Цена чистки
    /// </summary>
    public required decimal CleaningPrice { get; set; }
}