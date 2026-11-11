using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dtos;

/// <summary>
/// Данные для обновления зала.
/// </summary>
public class UpdateHallDto
{
    /// <summary>Название зала.</summary>
    [Required]
    public required string Name { get; set; }

    /// <summary>Вместимость зала.</summary>
    [Range(1, 1000)]
    public int Capacity { get; set; }
}