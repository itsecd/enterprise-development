using Microsoft.Extensions.Logging;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Entities;
using MuseumSystem.Domain.Interfaces;
using MuseumSystem.Services.Mappers;

namespace MuseumSystem.Services.Services;

public class AnalyticsService(
    IRepository<Ticket> ticketRepository,
    IRepository<Excursion> excursionRepository,
    ILogger<AnalyticsService> logger) : IAnalyticsService
{
    public async Task<IEnumerable<TopExhibitionResponse>> GetTop5ExhibitionsByVisitorsAsync()
    {
        logger.LogInformation("Запрос топ-5 выставок по посетителям");
        var tickets = await ticketRepository.GetAllAsync();

        return tickets
            .SelectMany(t => t.Excursion.Exhibitions)
            .GroupBy(e => new { e.Id, e.Name })
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new TopExhibitionResponse
            {
                ExhibitionId = g.Key.Id,
                ExhibitionName = g.Key.Name,
                VisitorCount = g.Count()
            })
            .ToList();
    }

    public async Task<IEnumerable<ExcursionWithParticipantsResponse>> GetExcursionsWithMinimalParticipantsAsync()
    {
        logger.LogInformation("Запрос экскурсий с минимальным числом участников");
        var excursions = await excursionRepository.GetAllAsync();
        var tickets = await ticketRepository.GetAllAsync();

        if (!excursions.Any())
            return new List<ExcursionWithParticipantsResponse>();

        // Проходимся по всем экскурсиям, включая и с 0 билетами
        var excursionsWithCount = excursions
            .Select(e => new
            {
                Excursion = e,
                Count = tickets.Count(t => t.Excursion.Id == e.Id)
            })
            .ToList();

        var minCount = excursionsWithCount.Min(x => x.Count);

        return excursionsWithCount
            .Where(x => x.Count == minCount)
            .Select(x => new ExcursionWithParticipantsResponse
            {
                ExcursionId = x.Excursion.Id,
                StartsAt = x.Excursion.StartsAt,
                ParticipantCount = x.Count
            })
            .ToList();
    }

    public async Task<IEnumerable<ThemeSummaryResponse>> GetThemeSummaryForPeriodAsync(DateTime from, DateTime to)
    {
        logger.LogInformation("Запрос сводки по тематикам за период с {From} по {To}", from, to);
        var tickets = await ticketRepository.GetAllAsync();

        return tickets
            .Where(t => t.Excursion.StartsAt >= from && t.Excursion.StartsAt <= to)
            .SelectMany(t => t.Excursion.Exhibitions
                .Select(e => e.Theme)
                .Distinct()
                .Select(theme => new { Theme = theme, Ticket = t }))
            .GroupBy(x => x.Theme)
            .Select(g => new ThemeSummaryResponse
            {
                Theme = g.Key,
                TotalVisitors = g.Select(x => x.Ticket.Visitor.Id).Distinct().Count(),
                TotalCost = g.Sum(x => x.Ticket.Price),
                AvgVisitors = g.GroupBy(x => DateOnly.FromDateTime(x.Ticket.Excursion.StartsAt)).Average(d => d.Count()),
                MinVisitors = g.GroupBy(x => DateOnly.FromDateTime(x.Ticket.Excursion.StartsAt)).Min(d => d.Count()),
                MaxVisitors = g.GroupBy(x => DateOnly.FromDateTime(x.Ticket.Excursion.StartsAt)).Max(d => d.Count())
            })
            .OrderBy(s => s.Theme)
            .ToList();
    }

    public async Task<IEnumerable<ExcursionInHallResponse>> GetExcursionsInHallForPeriodAsync(int hallNumber, DateTime from, DateTime to)
    {
        logger.LogInformation("Запрос экскурсий в зале {HallNumber} за период", hallNumber);
        var excursions = await excursionRepository.GetAllAsync();

        return excursions
            .Where(e => e.Exhibitions.Any(ex => ex.HallNumber == hallNumber)
                        && e.StartsAt >= from
                        && e.StartsAt <= to)
            .Select(e => new ExcursionInHallResponse
            {
                ExcursionId = e.Id,
                StartsAt = e.StartsAt,
                Duration = TimeSpan.FromMinutes(e.DurationMinutes)
            })
            .OrderBy(x => x.ExcursionId)
            .ToList();
    }

    public async Task<IEnumerable<VisitorResponse>> GetVisitorsOfSelectedExcursionAsync(int excursionId)
    {
        logger.LogInformation("Запрос посетителей экскурсии {ExcursionId}", excursionId);
        var tickets = await ticketRepository.GetAllAsync();

        return tickets
            .Where(t => t.Excursion.Id == excursionId)
            .Select(t => t.Visitor.ToDto())
            .OrderBy(v => v.FullName)
            .ToList();
    }
}