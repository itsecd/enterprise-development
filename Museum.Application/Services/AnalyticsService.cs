using Museum.Application.Mapping;
using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;
using Museum.Domain.Interfaces;

namespace Museum.Application.Services;

/// <summary>Выполняет аналитические запросы по выставкам, экскурсиям и посетителям</summary>
public sealed class AnalyticsService : IAnalyticsService
{
    private readonly IExhibitionRepository _exhibitionRepository;
    private readonly ITourRepository _tourRepository;
    private readonly ITicketRepository _ticketRepository;
    private readonly IVisitorRepository _visitorRepository;

    /// <summary>Создаёт сервис аналитики с необходимыми репозиториями</summary>
    public AnalyticsService(
        IExhibitionRepository exhibitionRepository,
        ITourRepository tourRepository,
        ITicketRepository ticketRepository,
        IVisitorRepository visitorRepository)
    {
        _exhibitionRepository = exhibitionRepository;
        _tourRepository = tourRepository;
        _ticketRepository = ticketRepository;
        _visitorRepository = visitorRepository;
    }

    /// <summary>Возвращает пять выставок с наибольшим количеством посетителей</summary>
    public async Task<IReadOnlyList<TopExhibitionDto>> GetTopExhibitionsAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync();

        return tickets
            .SelectMany(ticket => ticket.Tour.Exhibitions)
            .GroupBy(exhibition => exhibition.Id)
            .Select(group => new TopExhibitionDto
            {
                Exhibition = ExhibitionMapper.ToDto(group.First()),
                VisitorCount = group.Count()
            })
            .OrderByDescending(item => item.VisitorCount)
            .Take(5)
            .ToList();
    }

    /// <summary>Возвращает экскурсии с наименьшим количеством участников среди экскурсий с билетами</summary>
    public async Task<IReadOnlyList<TourWithVisitorCountDto>> GetToursWithMinVisitorsAsync()
    {
        var tickets = await _ticketRepository.GetAllAsync();

        var visitorCounts = tickets
            .GroupBy(ticket => ticket.Tour.Id)
            .Select(group => new
            {
                TourId = group.Key,
                VisitorCount = group.Count()
            })
            .ToList();

        if (visitorCounts.Count == 0)
            return [];

        var minVisitorCount = visitorCounts.Min(item => item.VisitorCount);
        var tours = await _tourRepository.GetAllAsync();

        return visitorCounts
            .Where(item => item.VisitorCount == minVisitorCount)
            .Join(
                tours,
                count => count.TourId,
                tour => tour.Id,
                (count, tour) => new TourWithVisitorCountDto
                {
                    Tour = TourMapper.ToDto(tour),
                    VisitorCount = count.VisitorCount
                })
            .ToList();
    }

    /// <summary>Формирует сводку посещаемости и выручки по темам выставок за период</summary>
    public async Task<IReadOnlyList<AttendanceSummaryDto>> GetAttendanceSummaryAsync(
        DateTime from,
        DateTime to)
    {
        var tickets = await _ticketRepository.GetAllAsync();

        var entries = tickets
            .Where(ticket => ticket.Tour.Date >= from && ticket.Tour.Date <= to)
            .SelectMany(ticket => ticket.Tour.Exhibitions.Select(exhibition => new
            {
                exhibition.Theme,
                VisitDate = ticket.Tour.Date.Date,
                ticket.Price
            }));

        return entries
            .GroupBy(entry => entry.Theme)
            .Select(group =>
            {
                var dailyCounts = group
                    .GroupBy(entry => entry.VisitDate)
                    .Select(day => day.Count())
                    .ToList();

                return new AttendanceSummaryDto
                {
                    Theme = group.Key,
                    TotalVisitors = group.Count(),
                    TotalPrice = group.Sum(entry => entry.Price),
                    AverageVisitorsPerDay = dailyCounts.Count == 0 ? 0 : dailyCounts.Average(),
                    MinVisitorsPerDay = dailyCounts.Count == 0 ? 0 : dailyCounts.Min(),
                    MaxVisitorsPerDay = dailyCounts.Count == 0 ? 0 : dailyCounts.Max()
                };
            })
            .ToList();
    }

    /// <summary>Возвращает экскурсии в выбранном зале за указанный период</summary>
    public async Task<IReadOnlyList<TourDto>> GetToursInHallAsync(
        int hall,
        DateTime from,
        DateTime to)
    {
        var tours = await _tourRepository.GetAllAsync();

        return tours
            .Where(tour =>
                tour.Date >= from &&
                tour.Date <= to &&
                tour.Exhibitions.Any(exhibition => exhibition.HallNumber == hall))
            .Select(TourMapper.ToDto)
            .ToList();
    }

    /// <summary>Возвращает отсортированный по имени список посетителей выбранной экскурсии</summary>
    public async Task<IReadOnlyList<VisitorDto>?> GetVisitorsByTourAsync(int tourId)
    {
        var tour = await _tourRepository.GetByIdAsync(tourId);

        if (tour is null)
            return null;

        var tickets = await _ticketRepository.GetAllAsync();

        return tickets
            .Where(ticket => ticket.Tour.Id == tourId)
            .Select(ticket => VisitorMapper.ToDto(ticket.Visitor))
            .OrderBy(visitor => visitor.FullName)
            .ToList();
    }
}
