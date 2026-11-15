using MuseumSystem.Shared.Enums;

namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Сводная информация о посещаемости по тематике
/// </summary>
public class ThemeSummaryResponse
{
    /// <summary>
    /// Тематика выставки
    /// </summary>
    public ExhibitionTheme Theme { get; set; }

    /// <summary>
    /// Общее количество посетителей
    /// </summary>
    public int TotalVisitors { get; set; }

    /// <summary>
    /// Общая стоимость билетов
    /// </summary>
    public decimal TotalCost { get; set; }

    /// <summary>
    /// Среднее количество посетителей в день
    /// </summary>
    public double AvgVisitors { get; set; }

    /// <summary>
    /// Минимальное количество посетителей в день
    /// </summary>
    public int MinVisitors { get; set; }

    /// <summary>
    /// Максимальное количество посетителей в день
    /// </summary>
    public int MaxVisitors { get; set; }
}