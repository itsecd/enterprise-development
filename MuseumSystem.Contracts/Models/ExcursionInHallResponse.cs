namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Информация об экскурсии в заданном зале
/// </summary>
public class ExcursionInHallResponse
{
    /// <summary>
    /// Идентификатор экскурсии
    /// </summary>
    public int ExcursionId { get; set; }

    /// <summary>
    /// Время начала экскурсии
    /// </summary>
    public DateTime StartsAt { get; set; }

    /// <summary>
    /// Продолжительность экскурсии
    /// </summary>
    public TimeSpan Duration { get; set; }
}