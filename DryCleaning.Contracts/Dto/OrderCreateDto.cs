using System.ComponentModel.DataAnnotations;

namespace DryCleaning.Contracts.Dto;

public class OrderCreateDto
{
    [Required, Range(1, int.MaxValue)]
    public required int ClientId { get; set; }

    [Required, Range(1, int.MaxValue)]
    public required int ItemId { get; set; }

    [Required, Range(1, int.MaxValue)]
    public required int ProcessingDays { get; set; }
}
