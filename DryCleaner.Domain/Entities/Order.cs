using DryCleaner.Shared.Enums;

namespace DryCleaner.Domain.Entities;

/// <summary>
/// Заказ на химчистку
/// </summary>
public class Order
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Клиент, оформивший заказ
    /// </summary>
    public required Customer Customer { get; set; }
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int CustomerId { get; set; }
    /// <summary>
    /// Изделие
    /// </summary>
    public required Item Item { get; set; }
    /// <summary>
    /// Идентификатор изделия
    /// </summary>
    public int ItemId { get; set; }
    /// <summary>
    /// Время приёма заказа
    /// </summary>
    public DateOnly AcceptanceDate { get; set; }
    /// <summary>
    /// Время на выполнение заказа
    /// </summary>
    public int CompletionDays { get; set; }
    /// <summary>
    /// Состояние заказа
    /// </summary>
    public OrderStatus Status { get; set; }
}