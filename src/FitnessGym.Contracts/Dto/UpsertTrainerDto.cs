using System.ComponentModel.DataAnnotations;
using FitnessGym.Domain.Enums;

namespace FitnessGym.Contracts.Dto;

/// <summary>
/// DTO тренера фитнес-клуба: данные для создания и обновления.
/// </summary>
public class UpsertTrainerDto
{
    /// <summary>
    /// Номер паспорта.
    /// </summary>
    [Required(ErrorMessage = "Не указан номер паспорта")]
    public string PassportNumber { get; init; } = string.Empty;

    /// <summary>
    /// ФИО.
    /// </summary>
    [Required(ErrorMessage = "Не указано ФИО")]
    public string FullName { get; init; } = string.Empty;

    /// <summary>
    /// Пол.
    /// </summary>
    [Required(ErrorMessage = "Не указан пол")]
    [EnumDataType(typeof(Gender), ErrorMessage = "Некорректное значение пола")]
    public Gender? Gender { get; init; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    [Required(ErrorMessage = "Не указана дата рождения")]
    public DateOnly? BirthDate { get; init; }

    /// <summary>
    /// Идентификатор специализации тренера (ссылка на справочник).
    /// </summary>
    [Required(ErrorMessage = "Не указана специализация")]
    [Range(0, int.MaxValue, ErrorMessage = "Некорректный идентификатор специализации")]
    public int? SpecializationId { get; init; }

    /// <summary>
    /// Стаж работы в годах.
    /// </summary>
    [Range(0, 100, ErrorMessage = "Стаж работы должен быть от 0 до 100 лет")]
    public int ExperienceYears { get; init; }
}
