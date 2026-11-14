using DryCleaning.Domain.Enum;

namespace DryCleaning.Contracts.Dto;

public class OrderDto
{
    public required int Id { get; set; }
    public required ClientDto Client { get; set; }
    public required ItemDto Item { get; set; }
    public required OrderStatus Status { get; set; }
    public int ProcessingDays { get; set; }
    public required DateTime AcceptDate { get; set; }
    public DateTime? CompletedDate { get; set; }
}
