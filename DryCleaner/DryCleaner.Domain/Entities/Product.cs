using System.ComponentModel.DataAnnotations.Schema;

namespace DryCleaner.Domain.Entities;

/// <summary>
/// Изделие 
/// </summary>
[Table("product")]
public class Product
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Название изделия
    /// </summary>
    [Column("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Материал изделия
    /// </summary>
    [Column("material")]
    public required string Material { get; set; }

    /// <summary>
    /// Категория изделия 
    /// </summary>
    [Column("category")]
    public required ProductCategory Category { get; set; }

}