using System.ComponentModel.DataAnnotations;
using FitnessGym.Domain.Entities;

namespace FitnessGym.Contracts.Dto;

/// <summary>
/// DTO специализации тренера (элемент справочника).
/// </summary>
public class UpsertSpecializationDto
{
    /// <summary>
    /// Название специализации.
    /// </summary>
    [Required(ErrorMessage = "Не указано название специализации")]
    [MinLength(2, ErrorMessage = "Название специализации слишком короткое")]
    [MaxLength(TextLimits.SpecializationName, ErrorMessage = "Максимальная длина поля — {1} символов")]
    public string Name { get; init; } = string.Empty;
}
