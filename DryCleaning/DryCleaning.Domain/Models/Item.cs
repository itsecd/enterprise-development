namespace DryCleaning.Domain.Models;

/// <summary>
/// Изделие
/// </summary>

public class Item
{
    public int Id { get; set; }

    /// <summary>
    /// Наименование
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Материал
    /// </summary>
    public required string Material { get; set; }

    /// <summary>
    /// Id-категории
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Категория
    /// </summary>
    public required Category Category { get; set; }
}
