using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DistrictEnterpriseStatisticalData.Domain.Entity;

/// <summary>
///     Форма собственности
/// </summary>
[Table("form_of_ownership")]
public class FormOfOwnership
{
    [Key] 
    public int Id { get; set; }
    
    [Column("form")]
    [MaxLength(50)]
    public required string Form { get; set; }

    public virtual List<Enterprise> Enterprises { get; set; } = [];
}