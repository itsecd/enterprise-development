using DryCleaner.Shared.Enums;

namespace DryCleaner.Domain.Entities;

/// <summary>
/// Заказ
/// </summary>
public class Order
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Изделие
    /// </summary>
    public required Product Product { get; set; }

    /// <summary>
    /// Время приема заказа
    /// </summary>
    public required DateTime AcceptedAt { get; set; }

    /// <summary>
    /// Срок выполнения в днях
    /// </summary>
    public int ProcessingDays { get; set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; set; }

}