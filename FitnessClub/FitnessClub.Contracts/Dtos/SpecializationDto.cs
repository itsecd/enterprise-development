namespace FitnessClub.Contracts.Dtos;

/// <summary>
/// Данные специализации тренера.
/// </summary>
public class SpecializationDto
{
    /// <summary>
    /// ID специализации.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название специализации.
    /// </summary>
    /// <example>Персональный тренинг</example>
    public required string Name { get; set; }

    /// <summary>
    /// Описание специализации.
    /// </summary>
    public string? Description { get; set; }
}