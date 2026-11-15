using System.ComponentModel.DataAnnotations;

namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Данные для обновления экскурсии
/// </summary>
public class ExcursionUpdateRequest
{
    /// <summary>
    /// Дата и время начала экскурсии
    /// </summary>
    [Required]
    public DateTime StartsAt { get; set; }

    /// <summary>
    /// Продолжительность экскурсии
    /// </summary>
    [Required]
    public TimeSpan Duration { get; set; }

    /// <summary>
    /// Список ID выставок, которые посещаются
    /// </summary>
    public List<int> ExhibitionIds { get; set; } = new();
}