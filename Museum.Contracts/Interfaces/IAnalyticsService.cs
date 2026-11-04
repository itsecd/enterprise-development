using Museum.Contracts.Dto;

namespace Museum.Contracts.Interfaces;

/// <summary>
/// Описывает аналитические запросы музея
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Возвращает пять самых посещаемых выставок
    /// </summary>
    Task<IReadOnlyList<TopExhibitionDto>> GetTopExhibitionsAsync();

    /// <summary>
    /// Возвращает экскурсии с наименьшим количеством участников
    /// </summary>
    Task<IReadOnlyList<TourWithVisitorCountDto>> GetToursWithMinVisitorsAsync();

    /// <summary>
    /// Возвращает статистику посещаемости по тематикам за период
    /// </summary>
    Task<IReadOnlyList<AttendanceSummaryDto>> GetAttendanceSummaryAsync(
        DateTime from,
        DateTime to);

    /// <summary>
    /// Возвращает экскурсии в выбранном зале за указанный период
    /// </summary>
    Task<IReadOnlyList<TourDto>> GetToursInHallAsync(
        int hall,
        DateTime from,
        DateTime to);

    /// <summary>
    /// Возвращает посетителей выбранной экскурсии
    /// </summary>
    Task<IReadOnlyList<VisitorDto>?> GetVisitorsByTourAsync(int tourId);
}
