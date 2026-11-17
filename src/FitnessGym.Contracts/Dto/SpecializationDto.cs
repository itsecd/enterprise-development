namespace FitnessGym.Contracts.Dto;

/// <summary>
/// DTO специализации тренера (элемент справочника).
/// </summary>
public class SpecializationDto
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Название специализации.
    /// </summary>
    public string Name { get; init; } = string.Empty;
}
