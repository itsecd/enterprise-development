using System.ComponentModel.DataAnnotations;

namespace CarWash.Contracts.Dto;

public class CarDto
{
    public int Id { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public string? Brand { get; set; }
}

public class CarEditDto
{
    [Required(ErrorMessage = "Госномер обязателен")]
    [MaxLength(9, ErrorMessage = "Госномер не может быть длиннее 9 символов")]
    public string LicensePlate { get; set; } = string.Empty;

    public string? Brand { get; set; }
}