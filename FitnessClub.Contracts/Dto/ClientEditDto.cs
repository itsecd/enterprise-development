using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Клиент (edit-модель)
/// </summary>
public class ClientEditDto
{
    /// <summary>
    /// Номер паспорта
    /// </summary>
    [Required(ErrorMessage = "Номер паспорта обязателен")]
    [RegularExpression(@"^\d{4}\s\d{6}$", ErrorMessage = "Номер паспорта должен быть в формате '1234 567890'")]
    public string PassportNumber { get; set; } = string.Empty;

    /// <summary>
    /// ФИО полностью
    /// </summary>
    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(150, ErrorMessage = "ФИО не должно превышать 150 символов")]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Пол
    /// </summary>
    [Required(ErrorMessage = "Пол обязателен")]
    [EnumDataType(typeof(Gender), ErrorMessage = "Недопустимое значение пола")]
    public Gender? Gender { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    [Required(ErrorMessage = "Дата рождения обязательна")]
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Телефон
    /// </summary>
    [Required(ErrorMessage = "Телефон обязателен")]
    [Phone(ErrorMessage = "Некорректный номер телефона")]
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Дата начала абонемента
    /// </summary>
    [Required(ErrorMessage = "Дата начала абонемента обязательна")]
    public DateTime SubscriptionStart { get; set; }

    /// <summary>
    /// Дата окончания абонемента
    /// </summary>
    [Required(ErrorMessage = "Дата окончания абонемента обязательна")]
    public DateTime? SubscriptionEnd { get; set; }
}