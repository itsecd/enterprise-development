using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DistrictEnterpriseStatisticalData.Domain.Entity;

/// <summary>
///     Поставщик
/// </summary>
[Table("supplier")]
public class Supplier
{
    /// <summary>
    ///     Идентификатор поставщика
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    ///     Имя поставщика
    /// </summary>
    [Column("name")]
    [MaxLength(50)]
    public required string Name { get; set; }

    /// <summary>
    ///     Список поставок
    /// </summary>
    public virtual List<Supply> Supplies { get; set; } = [];
}