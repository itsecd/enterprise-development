using Museum.Domain.Enums;

namespace Museum.Contracts;

/// <summary>
/// Статистика посещаемости по тематике выставок.
/// </summary>
/// <param name="Theme">Тематика выставки.</param>
/// <param name="TotalVisitors">Количество уникальных посетителей.</param>
/// <param name="TotalTicketCost">Суммарная стоимость билетов.</param>
/// <param name="AverageVisitorsPerDay">Среднее число посетителей в день.</param>
/// <param name="MinVisitorsPerDay">Минимальное число посетителей в день.</param>
/// <param name="MaxVisitorsPerDay">Максимальное число посетителей в день.</param>
public record ThemeAttendanceResponse(
    ExhibitionTheme Theme,
    int TotalVisitors,
    decimal TotalTicketCost,
    double AverageVisitorsPerDay,
    int MinVisitorsPerDay,
    int MaxVisitorsPerDay
);
