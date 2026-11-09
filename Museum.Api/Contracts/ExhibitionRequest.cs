using System.ComponentModel.DataAnnotations;
using Museum.Domain.Enums;

namespace Museum.Api.Contracts;

public class ExhibitionRequest
{
    [Required]
    public required string Name { get; set; }

    public ExhibitionTheme Theme { get; set; }

    [Range(1, int.MaxValue)]
    public int HallNumber { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }
}
