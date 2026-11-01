namespace AutoServiceLab.Domain.Entities;

/// <summary>
/// Связь заказа и вида работ
/// </summary>
public class OrderWork
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
    /// Идентификатор вида работ
    /// </summary>
    public int WorkTypeId { get; set; }

    /// <summary>
    /// Вид работ
    /// </summary>
    public required WorkType WorkType { get; set; }
}
