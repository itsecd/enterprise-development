using MuseumSystem.Shared.Enums;

namespace MuseumSystem.Tests;

/// <summary>
/// Тесты аналитических запросов по предметной области «Музей»
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными</param>
public class MuseumTests(MuseumFixture fixture) : IClassFixture<MuseumFixture>
{
    /// <summary>
    /// Топ-5 выставок по количеству посетителей, упорядоченных по убыванию
    /// </summary>
    [Fact]
    public void GetTop5ExhibitionsByVisitors_HasTickets_ReturnsOrderedByVisitorCount()
    {
        // arrange
        var expectedTop5 = new[]
        {
            "Физика частиц",
            "Древний Египет",
            "Импрессионизм",
            "Авиация",
            "Космос",
        };

        // act
        var actualTop5 = fixture.Tickets
            .SelectMany(t => t.Excursion.Exhibitions)
            .GroupBy(e => e.Name)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();

        // assert
        Assert.Equal(expectedTop5, actualTop5);
    }

    /// <summary>
    /// Экскурсии с минимальным числом участников
    /// </summary>
    [Fact]
    public void GetExcursionsWithMinimalParticipants_HasTickets_ReturnsExcursionsWithMinCount()
    {
        // arrange
        var excursionsWithCount = fixture.Tickets
            .GroupBy(t => t.Excursion.Id)
            .Select(g => new { ExcursionId = g.Key, Count = g.Count() })
            .ToList();

        var minCount = excursionsWithCount.Min(x => x.Count);
        int[] expectedExcursionIds = [6];

        // act
        var actualExcursionIds = excursionsWithCount
            .Where(x => x.Count == minCount)
            .Select(x => x.ExcursionId)
            .OrderBy(id => id)
            .ToList();

        // assert
        Assert.Equal(1, minCount);
        Assert.Equal(expectedExcursionIds, actualExcursionIds);
    }

    /// <summary>
    /// Сводная информация по каждой тематике за период
    /// </summary>
    [Fact]
    public void GetThemeSummaryForPeriod_HasTickets_ReturnsAggregatedDataPerTheme()
    {
        // arrange
        var from = new DateTime(2026, 1, 1);
        var to = new DateTime(2026, 3, 31, 23, 59, 59);

        var expected = new[]
        {
            new ThemeSummary(ExhibitionTheme.Historical,    7, 3750m, 3.0, 1, 5),
            new ThemeSummary(ExhibitionTheme.Art,          11, 5250m, 4.0, 4, 4),
            new ThemeSummary(ExhibitionTheme.Science,       7, 3750m, 4.5, 3, 6),
            new ThemeSummary(ExhibitionTheme.Ethnographic,  3, 2000m, 2.0, 2, 2),
            new ThemeSummary(ExhibitionTheme.Technical,     6, 3500m, 4.0, 3, 5),
        };

        // act
        var actual = fixture.Tickets
            .Where(t => t.Excursion.StartsAt >= from && t.Excursion.StartsAt <= to)
            .SelectMany(t => t.Excursion.Exhibitions
                .Select(e => e.Theme)
                .Distinct()
                .Select(theme => new { Theme = theme, Ticket = t }))
            .GroupBy(x => x.Theme)
            .Select(g => new ThemeSummary(
                Theme: g.Key,
                TotalVisitors: g.Select(x => x.Ticket.Visitor.Id).Distinct().Count(),
                TotalCost: g.Sum(x => x.Ticket.Price),
                AverageVisitorsPerDay: g.GroupBy(x => DateOnly.FromDateTime(x.Ticket.Excursion.StartsAt)).Average(d => d.Count()),
                MinVisitorsPerDay: g.GroupBy(x => DateOnly.FromDateTime(x.Ticket.Excursion.StartsAt)).Min(d => d.Count()),
                MaxVisitorsPerDay: g.GroupBy(x => DateOnly.FromDateTime(x.Ticket.Excursion.StartsAt)).Max(d => d.Count())))
            .OrderBy(s => s.Theme)
            .ToList();

        // assert
        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Экскурсии в выбранном зале за указанный период
    /// </summary>
    [Fact]
    public void GetExcursionsInSelectedHallForPeriod_HasExcursions_ReturnsFilteredExcursions()
    {
        // arrange
        const int hallNumber = 1;
        var from = new DateTime(2026, 1, 1);
        var to = new DateTime(2026, 3, 31, 23, 59, 59);
        int[] expectedIds = [1, 6, 11];

        // act
        var actualIds = fixture.Excursions
            .Where(e => e.Exhibitions.Any(ex => ex.HallNumber == hallNumber)
                        && e.StartsAt >= from
                        && e.StartsAt <= to)
            .Select(e => e.Id)
            .OrderBy(id => id)
            .ToList();

        // assert
        Assert.Equal(expectedIds, actualIds);
    }

    /// <summary>
    /// Посетители выбранной экскурсии, упорядоченные по ФИО
    /// </summary>
    [Fact]
    public void GetVisitorsOfSelectedExcursion_HasTickets_ReturnsOrderedByFullName()
    {
        // arrange
        const int excursionId = 3;
        var expectedNames = new[]
        {
            "Андреев Алексей Сергеевич",
            "Борисова Марина Владимировна",
            "Васильев Дмитрий Олегович",
            "Козлова Мария Дмитриевна",
            "Лебедев Артём Игоревич",
            "Морозова Татьяна Львовна",
        };

        // act
        var actualNames = fixture.Tickets
            .Where(t => t.Excursion.Id == excursionId)
            .Select(t => t.Visitor.FullName)
            .OrderBy(name => name)
            .ToList();

        // assert
        Assert.Equal(expectedNames, actualNames);
    }

    /// <summary>
    /// Сводная информация по одной тематике (класс с равенством по значению)
    /// </summary>
    private sealed class ThemeSummary : IEquatable<ThemeSummary>
    {
        public ThemeSummary(
            ExhibitionTheme Theme,
            int TotalVisitors,
            decimal TotalCost,
            double AverageVisitorsPerDay,
            int MinVisitorsPerDay,
            int MaxVisitorsPerDay)
        {
            this.Theme = Theme;
            this.TotalVisitors = TotalVisitors;
            this.TotalCost = TotalCost;
            this.AverageVisitorsPerDay = AverageVisitorsPerDay;
            this.MinVisitorsPerDay = MinVisitorsPerDay;
            this.MaxVisitorsPerDay = MaxVisitorsPerDay;
        }

        public ExhibitionTheme Theme { get; }
        public int TotalVisitors { get; }
        public decimal TotalCost { get; }
        public double AverageVisitorsPerDay { get; }
        public int MinVisitorsPerDay { get; }
        public int MaxVisitorsPerDay { get; }

        public bool Equals(ThemeSummary? other) =>
            other is not null
            && Theme == other.Theme
            && TotalVisitors == other.TotalVisitors
            && TotalCost == other.TotalCost
            && AverageVisitorsPerDay.Equals(other.AverageVisitorsPerDay)
            && MinVisitorsPerDay == other.MinVisitorsPerDay
            && MaxVisitorsPerDay == other.MaxVisitorsPerDay;

        public override bool Equals(object? obj) => Equals(obj as ThemeSummary);

        public override int GetHashCode() =>
            HashCode.Combine(Theme, TotalVisitors, TotalCost, AverageVisitorsPerDay, MinVisitorsPerDay, MaxVisitorsPerDay);

        public override string ToString() =>
            $"ThemeSummary {{ Theme = {Theme}, TotalVisitors = {TotalVisitors}, TotalCost = {TotalCost}, AverageVisitorsPerDay = {AverageVisitorsPerDay}, MinVisitorsPerDay = {MinVisitorsPerDay}, MaxVisitorsPerDay = {MaxVisitorsPerDay} }}";
    }
}