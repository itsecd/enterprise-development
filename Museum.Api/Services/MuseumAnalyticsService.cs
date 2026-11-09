using Museum.Api.Contracts;
using Museum.Domain.Data;

namespace Museum.Api.Services;

/// <summary>
/// Выполняет аналитические запросы к данным музея.
/// </summary>
public class MuseumAnalyticsService(MuseumContext context)
{
    /// <summary>
    /// Возвращает пять выставок с наибольшим числом посетителей.
    /// </summary>
    public IReadOnlyList<ExhibitionVisitorsResponse> GetTopExhibitions()
    {
        lock (context)
        {
            return context.Exhibitions
                .Select(exhibition => new ExhibitionVisitorsResponse(
                    exhibition.Id,
                    exhibition.Name,
                    exhibition.TourExhibitions
                        .SelectMany(relation => relation.Tour.Tickets)
                        .Select(ticket => ticket.VisitorId)
                        .Distinct()
                        .Count()
                ))
                .OrderByDescending(x => x.VisitorCount)
                .ThenBy(x => x.ExhibitionId)
                .Take(5)
                .ToArray();
        }
    }

    /// <summary>
    /// Возвращает экскурсии с минимальным количеством участников.
    /// </summary>
    public IReadOnlyList<TourParticipantsResponse> GetMinParticipantTours()
    {
        lock (context)
        {
            var tours = context.Tours
                .Select(tour => new TourParticipantsResponse(
                    tour.Id,
                    tour.Tickets
                        .Select(ticket => ticket.VisitorId)
                        .Distinct()
                        .Count()
                ))
                .ToArray();

            if (tours.Length == 0)
                return [];

            var minimum = tours.Min(x => x.ParticipantCount);

            return tours
                .Where(x => x.ParticipantCount == minimum)
                .OrderBy(x => x.TourId)
                .ToArray();
        }
    }

    /// <summary>
    /// Возвращает статистику посещаемости по тематикам за период.
    /// </summary>
    public IReadOnlyList<ThemeAttendanceResponse> GetAttendanceByTheme(
        DateOnly periodStart,
        DateOnly periodEnd)
    {
        lock (context)
        {
            var daysCount = periodEnd.DayNumber - periodStart.DayNumber + 1;

            return context.Exhibitions
                .GroupBy(exhibition => exhibition.Theme)
                .OrderBy(group => group.Key)
                .Select(group =>
                {
                    var tickets = group
                        .SelectMany(exhibition => exhibition.TourExhibitions)
                        .Where(relation =>
                        {
                            var date = DateOnly.FromDateTime(
                                relation.Tour.StartDateTime);

                            return date >= periodStart && date <= periodEnd;
                        })
                        .SelectMany(relation => relation.Tour.Tickets)
                        .DistinctBy(ticket => ticket.Id)
                        .ToArray();

                    var visitorsByDay = tickets
                        .GroupBy(ticket => DateOnly.FromDateTime(
                            ticket.Tour.StartDateTime))
                        .ToDictionary(
                            day => day.Key,
                            day => day
                                .Select(ticket => ticket.VisitorId)
                                .Distinct()
                                .Count()
                        );

                    var totalVisitors = tickets
                        .Select(ticket => ticket.VisitorId)
                        .Distinct()
                        .Count();

                    var average = visitorsByDay.Values.Sum()
                        / (double)daysCount;

                    var minimum = visitorsByDay.Count < daysCount
                        ? 0
                        : visitorsByDay.Values.Min();

                    var maximum = visitorsByDay.Count == 0
                        ? 0
                        : visitorsByDay.Values.Max();

                    return new ThemeAttendanceResponse(
                        group.Key,
                        totalVisitors,
                        tickets.Sum(ticket => ticket.Price),
                        average,
                        minimum,
                        maximum
                    );
                })
                .ToArray();
        }
    }

    /// <summary>
    /// Возвращает экскурсии в выбранном зале за период.
    /// </summary>
    public IReadOnlyList<HallTourResponse> GetToursInHall(
        int hallNumber,
        DateOnly periodStart,
        DateOnly periodEnd)
    {
        lock (context)
        {
            return context.Tours
                .Where(tour =>
                    DateOnly.FromDateTime(tour.StartDateTime) >= periodStart &&
                    DateOnly.FromDateTime(tour.StartDateTime) <= periodEnd &&
                    tour.TourExhibitions.Any(relation =>
                        relation.Exhibition.HallNumber == hallNumber))
                .OrderBy(tour => tour.StartDateTime)
                .ThenBy(tour => tour.Id)
                .Select(tour => new HallTourResponse(
                    tour.Id,
                    tour.StartDateTime,
                    tour.Duration
                ))
                .ToArray();
        }
    }

    /// <summary>
    /// Возвращает посетителей экскурсии в порядке ФИО.
    /// </summary>
    public IReadOnlyList<TourVisitorResponse>? GetTourVisitors(int tourId)
    {
        lock (context)
        {
            if (!context.Tours.Any(tour => tour.Id == tourId))
                return null;

            return context.Tickets
                .Where(ticket => ticket.TourId == tourId)
                .Select(ticket => ticket.Visitor)
                .DistinctBy(visitor => visitor.Id)
                .OrderBy(visitor => visitor.FullName, StringComparer.Ordinal)
                .Select(visitor => new TourVisitorResponse(
                    visitor.Id,
                    visitor.FullName,
                    visitor.Phone,
                    visitor.BirthDate
                ))
                .ToArray();
        }
    }
}
