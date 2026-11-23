namespace AutoServiceLab.Domain.Entities;

/// <summary>
/// Связь заказа и механика
/// </summary>
public class OrderMechanic
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int ServiceOrderId { get; set; }

    /// <summary>
    /// Заказ
    /// </summary>
    public required ServiceOrder ServiceOrder { get; set; }

    /// <summary>
    /// Идентификатор механика
    /// </summary>
    public int MechanicId { get; set; }

    /// <summary>
    /// Механик
    /// </summary>
    public required Mechanic Mechanic { get; set; }
}
