using System.ComponentModel.DataAnnotations;

namespace CarWash.Contracts.Dto;

/// <summary>
/// Данные для создания и изменения автомобиля
/// </summary>
public class CarEditDto
{
    /// <summary>
    /// Государственный регистрационный номер
    /// </summary>
    [Required(ErrorMessage = "Госномер обязателен")]
    [MaxLength(9, ErrorMessage = "Госномер не может быть длиннее 9 символов")]
    public required string LicensePlate { get; set; }

    /// <summary>
    /// Марка автомобиля
    /// </summary>
    public string? Brand { get; set; }
}
