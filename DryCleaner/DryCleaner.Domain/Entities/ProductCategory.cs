using System.ComponentModel.DataAnnotations.Schema;

namespace DryCleaner.Domain.Entities;

/// <summary>
/// Категория изделия 
/// </summary>
[Table("product_category")]
public class ProductCategory
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Название категории 
    /// </summary>
    [Column("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Рекомендуемый тип чистки
    /// </summary>
    [Column("recommended_cleaning_type")]
    public string? RecommendedCleaningType { get; set; }

    /// <summary>
    /// Цена чистки
    /// </summary>
    [Column("cleaning_price")]
    public required decimal CleaningPrice { get; set; }
}