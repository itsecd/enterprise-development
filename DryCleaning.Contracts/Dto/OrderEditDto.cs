using System.ComponentModel.DataAnnotations;
using DryCleaning.Domain.Enum;

namespace DryCleaning.Contracts.Dto;

public class OrderEditDto
{
    [Required, Range(1, int.MaxValue)]
    public required int ClientId { get; set; }

    [Required, Range(1, int.MaxValue)]
    public required int ItemId { get; set; }

    [Required, Range(1, int.MaxValue)]
    public required int ProcessingDays { get; set; }

    [Required, EnumDataType(typeof(OrderStatus))]
    public required OrderStatus Status { get; set; }

    public DateTime? CompletedDate { get; set; }
}
