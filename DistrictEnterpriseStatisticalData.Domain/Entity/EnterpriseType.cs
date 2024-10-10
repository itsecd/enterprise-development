using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DistrictEnterpriseStatisticalData.Domain.Entity;

/// <summary>
///     Типы предприятий
/// </summary>
[Table("enterprise_type")]
public class EnterpriseType
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key] 
    public int Id { get; set; }
    
    /// <summary>
    /// Тип предприятия
    /// </summary>
    [Column("type")]
    [MaxLength(50)]
    public required string Type { get; set; }
    
    public virtual List<Enterprise> Enterprises { get; set; } = [];
}