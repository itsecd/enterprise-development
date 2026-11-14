using System.ComponentModel.DataAnnotations;

namespace DryCleaning.Contracts.Dto;

public class ClientEditDto
{
    [Required, MaxLength(100)]
    public required string FullName { get; set; }

    [Required, MaxLength(20), Phone]
    public required string PhoneNumber { get; set; }
}
