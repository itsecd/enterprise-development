using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dtos;

/// <summary>
/// Данные для создания специализации.
/// </summary>
public class CreateSpecializationDto
{
    /// <summary>
    /// Название специализации.
    /// </summary>
    [Required]
    public required string Name { get; set; }

    /// <summary>
    /// Описание специализации.
    /// </summary>
    public string? Description { get; set; }
}