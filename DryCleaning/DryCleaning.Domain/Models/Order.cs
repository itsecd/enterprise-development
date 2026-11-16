namespace DryCleaning.Domain.Models;

/// <summary>
/// Заказ
/// </summary>

public class Order
{
    public int Id { get; set; }

    /// <summary>
    /// Id-клиента
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Информация клиента
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Id-изделия
    /// </summary>
    public int ItemId { get; set; }

    /// <summary>
    /// Информация изделия
    /// </summary>
    public required Item Item { get; set; }

    /// <summary>
    /// Дата приема заказа
    /// </summary>
    public DateTime AcceptDate { get; set; }

    /// <summary>
    /// Срок выполнения в днях
    /// </summary>
    public int ExeDays { get; set; }

    /// <summary>
    /// Текущий статус заказа
    /// </summary>
    public Status Status { get; set; }
}
