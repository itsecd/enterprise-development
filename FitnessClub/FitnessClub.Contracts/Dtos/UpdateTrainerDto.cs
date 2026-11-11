using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dtos;

/// <summary>
/// Данные для обновления тренера.
/// </summary>
public class UpdateTrainerDto
{
    /// <summary>Номер паспорта тренера.</summary>
    [Required]
    public required string PassportNumber { get; set; }

    /// <summary>ФИО тренера.</summary>
    [Required]
    public required string FullName { get; set; }

    /// <summary>Пол тренера.</summary>
    [Required]
    [EnumDataType(typeof(GenderDto))]
    public GenderDto? Gender { get; set; }

    /// <summary>Дата рождения.</summary>
    public DateTime BirthDate { get; set; }

    /// <summary>ID специализации.</summary>
    [Range(1, int.MaxValue)]
    public int SpecializationId { get; set; }

    /// <summary>Стаж работы в годах.</summary>
    [Range(0, 80)]
    public int WorkExperienceYears { get; set; }
}