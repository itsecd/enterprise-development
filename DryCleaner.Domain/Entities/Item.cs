namespace DryCleaner.Domain.Entities;

/// <summary>
/// Изделие в химчистке
/// </summary>
public class Item
{
    /// <summary>
    /// Идентификатор изделия
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название изделия
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Категория изделия
    /// </summary>
    public required ItemCategory Category { get; set; }
    /// <summary>
    /// Идентификатор изделия
    /// </summary>
    public int CategoryId { get; set; }
    /// <summary>
    /// Материал изделия
    /// </summary>
    public required string Material { get; set; }
}