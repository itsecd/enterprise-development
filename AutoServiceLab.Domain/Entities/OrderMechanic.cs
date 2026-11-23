using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoServiceLab.Domain.Entities;

/// <summary>
/// Связь заказа и механика
/// </summary>
[Table("order_mechanics")]
public class OrderMechanic
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
    /// Идентификатор механика
    /// </summary>
    [Column("mechanic_id")]
    public int MechanicId { get; set; }

    /// <summary>
    /// Механик
    /// </summary>
    public required Mechanic Mechanic { get; set; }
}
