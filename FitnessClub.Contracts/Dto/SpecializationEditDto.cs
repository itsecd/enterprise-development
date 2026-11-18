using System.ComponentModel.DataAnnotations;

namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Специализация (edit-модель)
/// </summary>
public class SpecializationEditDto
{
    /// <summary>
    /// Название
    /// </summary>
    [Required(ErrorMessage = "Название специализации обязательно")]
    [MaxLength(100, ErrorMessage = "Название не должно превышать 100 символов")]
    public string Name { get; set; } = string.Empty;
}