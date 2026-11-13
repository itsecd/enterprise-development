using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalon.Domain.Entities;

/// <summary>
/// Услуга
/// </summary>
[Table("services")]
public class Service
{

    /// <summary>
    /// Идентификатор услуги 
    /// </summary>
    [Column("id")]
    public required int Id {  get; set; }

    /// <summary>
    /// Название услуги
    /// </summary>
    [Column("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Категория услуги
    /// </summary>
    [Column("category")]
    public required string Category { get; set; }

    /// <summary>
    /// Стоимость
    /// </summary>
    [Column("price")]
    public required decimal Price { get; set; }

    /// <summary>
    /// Длительность
    /// </summary>
    [Column("duration")]
    public required int Duration { get; set; }
}
