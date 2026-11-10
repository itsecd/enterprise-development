using System.ComponentModel.DataAnnotations;
using VetClinic.Domain.Enums;

namespace VetClinic.Application.Dto;

/// <summary>
/// Данные для создания или обновления специализации
/// </summary>
public class CreateSpecializationDto
{
    /// <summary>
    /// Название специализации
    /// </summary>
    [Required]
    [StringLength(150, MinimumLength = 1)]
    public required string Name { get; set; }

    /// <summary>
    /// Профильный вид животных
    /// </summary>
    [Required]
    public AnimalSpecies? TargetSpecies { get; set; }
}