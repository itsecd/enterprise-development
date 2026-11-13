using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dtos;

/// <summary>
/// Данные для обновления специализации.
/// </summary>
public class UpdateSpecializationDto
{
    /// <summary>Название специализации.</summary>
    [Required]
    [MaxLength(200)]
    public required string Name { get; set; }

    /// <summary>Описание специализации.</summary>
    [MaxLength(2000)]
    public string? Description { get; set; }
}