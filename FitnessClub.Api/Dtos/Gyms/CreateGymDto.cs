
using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Api.Dtos.Gyms;

/// <summary>
/// Данные для создания и изменения зала.
/// </summary>
public class CreateGymDto
{
    /// <summary>
    /// Название зала.
    /// </summary>
    [Required]
    public required string Name { get; set; }
}
