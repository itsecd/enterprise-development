using System.ComponentModel.DataAnnotations;

namespace VetClinic.Contracts.Dto.Owners;

/// <summary>
/// Данные владельца для создания и изменения
/// </summary>
public class OwnerEditDto
{
    /// <summary>
    /// ФИО владельца
    /// </summary>
    [Required]
    public required string FullName { get; init; }

    /// <summary>
    /// Адрес проживания
    /// </summary>
    [Required]
    [MaxLength(250)]
    public required string Address { get; init; }

    /// <summary>
    /// Контактный телефон
    /// </summary>
    [Required]
    [Phone]
    [MaxLength(20)]
    public required string Phone { get; init; }
}
