using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Тренер (edit-модель)
/// </summary>
public class TrainerEditDto
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
    /// Идентификатор специализации
    /// </summary>
    [Range(0, int.MaxValue, ErrorMessage = "Укажите идентификатор специализации")]
    public int SpecializationId { get; set; }

    /// <summary>
    /// Стаж работы (лет)
    /// </summary>
    [Range(0, 100, ErrorMessage = "Стаж должен быть в диапазоне от 0 до 100")]
    public int ExperienceYears { get; set; }
}