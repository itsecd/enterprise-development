namespace BeautySalon.Contracts.Analytics;

/// <summary>
/// Свободный промежуток между записями специалиста
/// </summary>
public sealed class FreeWindowResponse
{
    /// <summary>
    /// Начало свободного промежутка
    /// </summary>
    public required DateTime Start { get; init; }

    /// <summary>
    /// Конец свободного промежутка
    /// </summary>
    public required DateTime End { get; init; }

    /// <summary>
    /// Продолжительность в минутах
    /// </summary>
    public double DurationMinutes => (End - Start).TotalMinutes;
}