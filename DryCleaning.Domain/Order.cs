using DryCleaning.Domain.Enum;

namespace DryCleaning.Domain;

/// <summary>Заказ.</summary>
public class Order
{
    public int Id { get; set; }
    public required Client Client { get; set; }
    public required Item Item { get; set; }
    public required DateTime AcceptDate { get; set; }
    public required int ProcessingDays { get; set; }
    public required OrderStatus Status { get; set; }
    public DateTime? CompletedDate { get; set; }
}
