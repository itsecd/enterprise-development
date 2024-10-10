using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DistrictEnterpriseStatisticalData.Domain.Entity;

/// <summary>
///     Поставка
/// </summary>
[Table("supply")]
public class Supply
{
    /// <summary>
    ///     Идентификатор поставки
    /// </summary>
    [Key]
    public int SupplyId { get; set; }

    /// <summary>
    ///     Количество поставляемого сырья
    /// </summary>
    [Column("quantity")]
    public required int Quantity { get; set; }

    /// <summary>
    ///     Дата поставки
    /// </summary>
    [Column("date")]
    public required DateOnly Date { get; set; }

    /// <summary>
    ///     Предприятие, которому осуществлялась поставка
    /// </summary>
    [Column("enterprise_registration_number")]
    public int EnterpriseRegistrationNumber { get; set; }

    public virtual Enterprise Enterprise { get; set; } = null!;

    /// <summary>
    ///     Поставщик
    /// </summary>
    public int SupplierId { get; set; }

    public virtual Supplier Supplier { get; set; } = null!;
}