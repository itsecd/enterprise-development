namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Ответ с информацией об экскурсии и количестве участников
/// </summary>
public class ExcursionWithParticipantsResponse
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
    /// Количество участников
    /// </summary>
    public int ParticipantCount { get; set; }
}