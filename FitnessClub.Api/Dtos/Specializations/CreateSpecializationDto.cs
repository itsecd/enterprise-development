
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Api.Dtos.Specializations;

/// <summary>
/// Данные для создания и изменения специализации.
/// </summary>
public class CreateSpecializationDto
{
    /// <summary>
    /// Название специализации тренера.
    /// </summary>
    [Required]
    public required string Name { get; set; }
}
