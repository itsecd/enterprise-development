namespace DryCleaner.Domain.Entities;

/// <summary>
/// Изделие 
/// </summary>
public class Product
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Название изделия
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Материал изделия
    /// </summary>
    public required string Material { get; set; }

    /// <summary>
    /// Категория изделия 
    /// </summary>
    public required ProductCategory Category { get; set; }

}