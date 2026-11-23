using MuseumSystem.Shared.Enums;

namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Данные выставки для ответа клиенту
/// </summary>
public class ExhibitionResponse
{
    /// <summary>
    /// Уникальный идентификатор выставки
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название выставки
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Тематика выставки
    /// </summary>
    public ExhibitionTheme Theme { get; set; }

    /// <summary>
    /// Номер зала
    /// </summary>
    public int HallNumber { get; set; }

    /// <summary>
    /// Дата начала выставки
    /// </summary>
    public DateOnly StartDate { get; set; }

    /// <summary>
    /// Дата окончания выставки
    /// </summary>
    public DateOnly EndDate { get; set; }
}