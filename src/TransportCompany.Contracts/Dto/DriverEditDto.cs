using System.ComponentModel.DataAnnotations;
using TransportCompany.Domain.Enums;

namespace TransportCompany.Contracts.Dto;

/// <summary>
/// DTO для создания или редактирования водителя
/// </summary>
public class DriverEditDto
{
    /// <summary>
    /// Серия и номер паспорта
    /// </summary>
    /// <example>1234 567890</example>
    [Required(ErrorMessage = "Номер паспорта обязателен")]
    [MaxLength(20, ErrorMessage = "Номер паспорта не может превышать 20 символов")]
    public required string PassportNumber { get; init; }

    /// <summary>
    /// ФИО водителя
    /// </summary>
    /// <example>Мамонтов Мамонт Мамонтович</example>
    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(100, ErrorMessage = "ФИО не может превышать 100 символов")]
    public required string FullName { get; init; }

    /// <summary>
    /// Стаж вождения в годах
    /// </summary>
    [Range(0, 70, ErrorMessage = "Стаж должен быть от 0 до 70 лет")]
    public required int Experience { get; init; }

    /// <summary>
    /// Водительское удостоверение
    /// </summary>
    [Range(0, 15, ErrorMessage = "Номер категории водительского удостоверения должен быть от 0 до 15")]
    public required DrivingLicense License { get; init; }
}