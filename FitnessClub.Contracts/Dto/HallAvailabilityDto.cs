namespace FitnessClub.Contracts.Dto;

/// <summary>
/// Результат проверки доступности зала
/// </summary>
public class HallAvailabilityDto
{
    /// <summary>
    /// Название зала
    /// </summary>
    public string Hall { get; init; } = string.Empty;

    /// <summary>
    /// Проверяемый момент времени
    /// </summary>
    public DateTime CheckedTime { get; init; }

    /// <summary>
    /// Доступен ли зал
    /// </summary>
    public bool IsAvailable { get; init; }
}