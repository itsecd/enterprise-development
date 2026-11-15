using Museum.Domain;
using Museum.Shared;

namespace Museum.Tests;

/// <summary>
/// Тесты LINQ-запросов над тестовыми данными музея
/// </summary>
public class ExhibitionTests(MuseumFixture fixture) : IClassFixture<MuseumFixture>
{
    /// <summary>
    /// Результат не должен превышать 5 выставок и должен быть отсортирован по убыванию посетителей
    /// </summary>
    [Fact]
    public void GetTopExhibitions_ByVisitorCount_ReturnsTopFive()
    {
        string[] expected = ["Барокко", "Древний Египет", "Космос", "Дикая природа", "Художники"];

        var result = fixture.Tickets
            .SelectMany(t => t.Tour.Exhibitions.Select(e => new { Exhibition = e, Ticket = t }))
            .GroupBy(x => x.Exhibition)
            .Select(g => new { Exhibition = g.Key, VisitorCount = g.Count() })
            .OrderByDescending(x => x.VisitorCount)
            .Take(5)
            .ToList();

        var actual = result.Select(x => x.Exhibition.Name).ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Все экскурсии в выборке должны иметь одинаковое число участников, равное минимальному
    /// </summary>
    [Fact]
    public void GetTours_WithMinVisitors_ReturnsLowestCount()
    {
        int[] expected = [3, 4, 5, 6, 7, 8, 9, 10];

        var tourVisitorCounts = fixture.Tickets
            .GroupBy(t => t.Tour)
            .Select(g => new { Tour = g.Key, VisitorCount = g.Count() })
            .ToList();

        var minCount = tourVisitorCounts.Min(x => x.VisitorCount);

        var result = tourVisitorCounts
            .Where(x => x.VisitorCount == minCount)
            .Select(x => x.Tour)
            .ToList();

        var actual = result.Select(t => t.Id).ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Для каждой тематики среднее число посетителей в день должно быть не больше максимального и не меньше минимального
    /// </summary>
    [Fact]
    public void GetAttendanceSummary_ByTheme_ReturnsGroupedStats()
    {
        List<(ExhibitionTheme Theme, int TotalVisitors, decimal TotalPrice, double AverageVisitorsPerDay, int MinVisitorsPerDay, int MaxVisitorsPerDay)> expected =
        [
            (ExhibitionTheme.Art, 5, 2150m, 1.25, 1, 2),
            (ExhibitionTheme.History, 7, 2800m, 1.75, 1, 2),
            (ExhibitionTheme.Science, 3, 1250m, 1.5, 1, 2),
            (ExhibitionTheme.Nature, 3, 1600m, 1.0, 1, 1)
        ];

        var startDate = new DateTime(2025, 1, 1);
        var endDate = new DateTime(2025, 12, 31);

        var ticketsInPeriod = fixture.Tickets
            .Where(t => t.Tour.Date >= startDate && t.Tour.Date <= endDate)
            .ToList();

        var result = ticketsInPeriod
            .SelectMany(t => t.Tour.Exhibitions.Select(e => new { Theme = e.Theme, Ticket = t }))
            .GroupBy(x => x.Theme)
            .Select(g => new
            {
                Theme = g.Key,
                TotalVisitors = g.Count(),
                TotalPrice = g.Sum(x => x.Ticket.Price),
                DailyVisitorCounts = g
                    .GroupBy(x => x.Ticket.Tour.Date.Date)
                    .Select(dayGroup => dayGroup.Count())
                    .ToList()
            })
            .Select(x => new
            {
                x.Theme,
                x.TotalVisitors,
                x.TotalPrice,
                AverageVisitorsPerDay = x.DailyVisitorCounts.Average(),
                MinVisitorsPerDay = x.DailyVisitorCounts.Min(),
                MaxVisitorsPerDay = x.DailyVisitorCounts.Max()
            })
            .ToList();

        var actual = result
            .Select(x => (x.Theme, x.TotalVisitors, x.TotalPrice, x.AverageVisitorsPerDay, x.MinVisitorsPerDay, x.MaxVisitorsPerDay))
            .ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// У каждой экскурсии в выборке должна быть хотя бы одна выставка из нужного зала
    /// </summary>
    [Fact]
    public void GetTours_InHall_ReturnsMatchingTours()
    {
        int[] expected = [1, 4, 5, 11];

        var hallNumber = 1;
        var startDate = new DateTime(2025, 1, 1);
        var endDate = new DateTime(2025, 12, 31);

        var result = fixture.Tours
            .Where(t => t.Date >= startDate && t.Date <= endDate)
            .Where(t => t.Exhibitions.Any(e => e.HallNumber == hallNumber))
            .ToList();

        var actual = result.Select(t => t.Id).ToList();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Список посетителей должен быть отсортирован по ФИО по возрастанию
    /// </summary>
    [Fact]
    public void GetVisitors_ForTour_ReturnsSortedByName()
    {
        string[] expected = ["Иванов Иван Иванович", "Петрова Мария Сергеевна"];

        var selectedTourId = fixture.Tours.First().Id;

        var result = fixture.Tickets
            .Where(t => t.Tour.Id == selectedTourId)
            .Select(t => t.Visitor)
            .OrderBy(v => v.FullName)
            .ToList();

        var actual = result.Select(v => v.FullName).ToList();

        Assert.Equal(expected, actual);
    }
}
