using Museum.Tests.Fixtures;
using Museum.Domain.Enums;

namespace Museum.Tests.Api;

/// <summary>
/// Проверяет аналитические запросы сервера.
/// </summary>
public class AnalyticsServiceTests(MuseumFixture fixture) : IClassFixture<MuseumFixture>
{
    /// <summary>Проверяет топ-5 выставок по числу посетителей.</summary>
    [Fact]
    public void TopFiveExhibitionsByVisitorCount()
    {
        var expected = new[]
        {
            (1, 4),
            (2, 3),
            (3, 3),
            (5, 2),
            (4, 1)
        };

        var service = fixture.AnalyticsService;

        var result = service.GetTopExhibitions()
            .Select(x => (x.ExhibitionId, x.VisitorCount))
            .ToArray();

        Assert.Equal(expected, result);
    }

    /// <summary>Проверяет экскурсии с минимальным числом участников.</summary>
    [Fact]
    public void ToursWithMinimumParticipantCount()
    {
        var expected = new[]
        {
            (9, 0),
            (10, 0)
        };

        var service = fixture.AnalyticsService;

        var result = service.GetMinParticipantTours()
            .Select(x => (x.TourId, x.ParticipantCount))
            .ToArray();

        Assert.Equal(expected, result);
    }

    /// <summary>Проверяет статистику посещаемости по темам за период.</summary>
    [Fact]
    public void AttendanceSummaryByExhibitionTheme()
    {
        var expected = new[]
        {
            (ExhibitionTheme.History, 5, 600m, 2.5, 1, 4),
            (ExhibitionTheme.Art, 3, 300m, 1.5, 0, 3),
            (ExhibitionTheme.Archaeology, 0, 0m, 0.0, 0, 0),
            (ExhibitionTheme.Science, 0, 0m, 0.0, 0, 0),
            (ExhibitionTheme.Ethnography, 0, 0m, 0.0, 0, 0)
        };

        var service = fixture.AnalyticsService;

        var result = service.GetAttendanceByTheme(
                new DateOnly(2026, 1, 10),
                new DateOnly(2026, 1, 11))
            .Select(x => (
                x.Theme,
                x.TotalVisitors,
                x.TotalTicketCost,
                x.AverageVisitorsPerDay,
                x.MinVisitorsPerDay,
                x.MaxVisitorsPerDay
            ))
            .ToArray();

        Assert.Equal(expected, result);
    }

    /// <summary>Проверяет экскурсии в выбранном зале за период.</summary>
    [Fact]
    public void ToursInSelectedHallForPeriod()
    {
        var expected = new[]
        {
            1, 2, 3
        };

        var service = fixture.AnalyticsService;

        var result = service.GetToursInHall(
                1,
                new DateOnly(2026, 1, 10),
                new DateOnly(2026, 1, 12))
            .Select(x => x.TourId)
            .ToArray();

        Assert.Equal(expected, result);
    }

    /// <summary>Проверяет список посетителей экскурсии по ФИО.</summary>
    [Fact]
    public void VisitorsOfSelectedTourSortedByFullName()
    {
        var expected = new[]
        {
            (1, "Анна Иванова"),
            (2, "Борис Петров"),
            (3, "Вера Смирнова")
        };

        var service = fixture.AnalyticsService;

        var result = service.GetTourVisitors(1)!
            .Select(x => (x.VisitorId, x.FullName))
            .ToArray();

        Assert.Equal(expected, result);
    }
}
