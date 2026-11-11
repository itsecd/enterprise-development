using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dtos;

/// <summary>
/// Данные для обновления специализации.
/// </summary>
public class UpdateSpecializationDto
{
    /// <summary>Название специализации.</summary>
    [Required]
    public required string Name { get; set; }

    /// <summary>Описание специализации.</summary>
    public string? Description { get; set; }
}