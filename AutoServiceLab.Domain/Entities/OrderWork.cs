using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoServiceLab.Domain.Entities;

/// <summary>
/// Связь заказа и вида работ
/// </summary>
[Table("order_works")]
public class OrderWork
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    [Column("service_order_id")]
    public int ServiceOrderId { get; set; }

    /// <summary>
    /// Заказ
    /// </summary>
    public required ServiceOrder ServiceOrder { get; set; }

    /// <summary>
    /// Идентификатор вида работ
    /// </summary>
    [Column("work_type_id")]
    public int WorkTypeId { get; set; }

    /// <summary>
    /// Вид работ
    /// </summary>
    public required WorkType WorkType { get; set; }
}
