using System.ComponentModel.DataAnnotations;

namespace Museum.Api.Contracts;

public class VisitorRequest
{
    [Required]
    public required string FullName { get; set; }

    [Required]
    public required string Phone { get; set; }

    public DateOnly BirthDate { get; set; }
}
