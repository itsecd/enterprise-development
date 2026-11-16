namespace DryCleaning.Domain.Models;

/// <summary>
/// Категория изделия
/// </summary>

public class Category
{
    public int Id { get; set; }

    /// <summary>
    /// Название категории
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Рекомендуемый вид чистки
    /// </summary>
    public required string CleaningType { get; set; }

    /// <summary>
    /// Стоимость чистки за единицу
    /// </summary>
    public decimal Price { get; set; }
}
