namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Ответ для топ-5 выставок
/// </summary>
public class TopExhibitionResponse
{
    /// <summary>
    /// Идентификатор выставки
    /// </summary>
    public int ExhibitionId { get; set; }

    /// <summary>
    /// Название выставки
    /// </summary>
    public string ExhibitionName { get; set; } = string.Empty;

    /// <summary>
    /// Количество посетителей
    /// </summary>
    public int VisitorCount { get; set; }
}