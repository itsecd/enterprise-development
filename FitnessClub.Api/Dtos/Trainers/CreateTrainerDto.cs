
using System.ComponentModel.DataAnnotations;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Api.Dtos.Trainers;

/// <summary>
/// Данные для создания и изменения тренера.
/// </summary>
public class CreateTrainerDto
{
    /// <summary>
    /// Номер паспорта тренера.
    /// </summary>
    [Required]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Фамилия, имя и отчество тренера.
    /// </summary>
    [Required]
    public required string FullName { get; set; }

    /// <summary>
    /// Пол тренера.
    /// </summary>
    [EnumDataType(typeof(Gender))]
    public Gender Gender { get; set; }

    /// <summary>
    /// Дата рождения тренера.
    /// </summary>
    public DateOnly BirthDate { get; set; }

    /// <summary>
    /// Идентификатор специализации тренера.
    /// </summary>
    [Range(1, int.MaxValue)]
    public int SpecializationId { get; set; }

    /// <summary>
    /// Стаж работы тренера в годах.
    /// </summary>
    [Range(0, int.MaxValue)]
    public int WorkExperience { get; set; }
}
