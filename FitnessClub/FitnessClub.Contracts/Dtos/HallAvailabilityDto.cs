namespace FitnessClub.Contracts.Dtos;

/// <summary>
/// Результат проверки доступности зала.
/// </summary>
public class HallAvailabilityDto
{
    /// <summary>ID зала.</summary>
    public int HallId { get; set; }

    /// <summary>Название зала.</summary>
    public required string HallName { get; set; }

    /// <summary>Признак доступности зала в текущий момент.</summary>
    public bool IsAvailable { get; set; }
}