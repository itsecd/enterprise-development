using Museum.Domain;

namespace Museum.Contracts.Dto;

/// <summary>
/// Хранит статистику посещаемости по тематике выставок
/// </summary>
public sealed class AttendanceSummaryDto
{
    /// <summary>
    /// Тематика выставок
    /// </summary>
    public ExhibitionTheme Theme { get; init; }

    /// <summary>
    /// Общее количество посетителей
    /// </summary>
    public int TotalVisitors { get; init; }

    /// <summary>
    /// Общая стоимость проданных билетов
    /// </summary>
    public decimal TotalPrice { get; init; }

    /// <summary>
    /// Среднее количество посетителей в день
    /// </summary>
    public double AverageVisitorsPerDay { get; init; }

    /// <summary>
    /// Минимальное количество посетителей за день
    /// </summary>
    public int MinVisitorsPerDay { get; init; }

    /// <summary>
    /// Максимальное количество посетителей за день
    /// </summary>
    public int MaxVisitorsPerDay { get; init; }
}
