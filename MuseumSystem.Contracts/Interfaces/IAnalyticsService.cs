using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Contracts.Interfaces;

/// <summary>
/// Сервис аналитических запросов
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получить топ-5 выставок по количеству посетителей
    /// </summary>
    public Task<IEnumerable<TopExhibitionResponse>> GetTop5ExhibitionsByVisitorsAsync();

    /// <summary>
    /// Получить экскурсии с минимальным числом участников
    /// </summary>
    public Task<IEnumerable<ExcursionWithParticipantsResponse>> GetExcursionsWithMinimalParticipantsAsync();

    /// <summary>
    /// Получить сводную информацию по тематикам за период
    /// </summary>
    public Task<IEnumerable<ThemeSummaryResponse>> GetThemeSummaryForPeriodAsync(DateTime from, DateTime to);

    /// <summary>
    /// Получить экскурсии в выбранном зале за период
    /// </summary>
    public Task<IEnumerable<ExcursionInHallResponse>> GetExcursionsInHallForPeriodAsync(int hallNumber, DateTime from, DateTime to);

    /// <summary>
    /// Получить посетителей выбранной экскурсии
    /// </summary>
    public Task<IEnumerable<VisitorResponse>> GetVisitorsOfSelectedExcursionAsync(int excursionId);
}