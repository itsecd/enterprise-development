using DryCleaning.Domain.Enum;

namespace DryCleaning.Domain;

/// <summary>
/// Заказ на обработку изделия в химчистке.
/// Содержит уникальный идентификатор, клиента, изделие, статус,
/// срок обработки, дату приёма и фактическую дату завершения.
/// Дата завершения может быть null, если заказ ещё не завершён.
/// </summary>
public class Order
{
    /// <summary>
    /// Уникальный идентификатор заказа       
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Идентификатор клиента, оформившего заказ
    /// </summary>
    public int ClientId { get; set; }
    /// <summary>
    /// Клиент 
    /// </summary>
    public Client Client { get; set; } = null!;
    /// <summary>
    /// Идентификатор изделия
    /// </summary>
    public int ItemId { get; set; }
    /// <summary>
    /// Изделие
    /// </summary>
    public Item Item { get; set; } = null!;
    /// <summary>
    /// Статус заказа
    /// </summary>
    public OrderStatus Status { get; set; }
    /// <summary>
    /// Срок обработки заказа в днях
    /// </summary>  
    public int ProcessingDays { get; set; }
    /// <summary>
    /// Дата приема заказа
    /// </summary>
    public required DateTime AcceptDate { get; set; }
    /// <summary>
    /// Дата завершения заказа
    /// </summary>
    public DateTime? CompletedDate { get; set; }
}
