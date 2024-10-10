using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DistrictEnterpriseStatisticalData.Domain.Entity;

/// <summary>
///     Форма собственности
/// </summary>
[Table("form_of_ownership")]
public class FormOfOwnership
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key] 
    public int Id { get; set; }
    
    /// <summary>
    /// Форма владения
    /// </summary>
    [Column("form")]
    [MaxLength(50)]
    public required string Form { get; set; }

    public virtual List<Enterprise> Enterprises { get; set; } = [];
}