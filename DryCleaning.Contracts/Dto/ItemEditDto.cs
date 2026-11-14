using System.ComponentModel.DataAnnotations;

namespace DryCleaning.Contracts.Dto;

public class ItemEditDto
{
    [Required, MaxLength(100)]
    public required string Name { get; set; }

    [Required, MaxLength(100)]
    public required string Material { get; set; }

    [Required, Range(1, int.MaxValue)]
    public required int CategoryId { get; set; }
}
