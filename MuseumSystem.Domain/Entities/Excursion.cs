namespace MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Interfaces;

/// <summary>
/// Экскурсия в музее
/// </summary>
public class Excursion : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата и время начала экскурсии
    /// </summary>
    public required DateTime StartsAt { get; set; }

    /// <summary>
    /// Продолжительность экскурсии в минутах
    /// </summary>
    public required int DurationMinutes { get; set; }

    /// <summary>
    /// Список посещаемых выставок
    /// </summary>
    public List<Exhibition> Exhibitions { get; set; } = [];
}