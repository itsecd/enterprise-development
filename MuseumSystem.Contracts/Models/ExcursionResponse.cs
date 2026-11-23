namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Данные экскурсии для ответа
/// </summary>
public class ExcursionResponse
{
    /// <summary>
    /// Уникальный идентификатор экскурсии
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата и время начала экскурсии
    /// </summary>
    public DateTime StartsAt { get; set; }

    /// <summary>
    /// Продолжительность экскурсии
    /// </summary>
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Список ID выставок, которые посещаются
    /// </summary>
    public List<int> ExhibitionIds { get; set; } = new();
}