using System.ComponentModel.DataAnnotations;

namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для создания или обновления тренера
/// </summary>
public record TrainerEditDto(
    [Required, MaxLength(20)]
    string PassportNumber,

    [Required, MaxLength(200)]
    string FullName,

    [Required]
    string Gender,

    [Required]
    DateOnly BirthDate,

    [Required]
    int SpecializationId,

    [Required, Range(0, 60)]
    int ExperienceYears);