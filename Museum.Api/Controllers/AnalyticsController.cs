using Microsoft.AspNetCore.Mvc;
using Museum.Api.Storage;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/analytics")]
public class AnalyticsController(MuseumStore store) : ControllerBase
{
    [HttpGet("top-exhibitions")]
    public IActionResult GetTopExhibitions()
    {
        var result = store.Tickets
            .SelectMany(t => t.Tour.Exhibitions)
            .GroupBy(e => e.Id)
            .Select(g => new { Exhibition = g.First(), VisitorCount = g.Count() })
            .OrderByDescending(x => x.VisitorCount)
            .Take(5)
            .ToList();

        return Ok(result);
    }

    [HttpGet("tours-with-min-visitors")]
    public IActionResult GetToursWithMinVisitors()
    {
        var counts = store.Tickets
            .GroupBy(t => t.Tour.Id)
            .Select(g => new { Tour = g.First().Tour, VisitorCount = g.Count() })
            .ToList();

        if (counts.Count == 0)
        {
            return Ok(counts);
        }

        var minCount = counts.Min(x => x.VisitorCount);
        var result = counts.Where(x => x.VisitorCount == minCount).ToList();

        return Ok(result);
    }

    [HttpGet("attendance-summary")]
    public IActionResult GetAttendanceSummary(DateTime from, DateTime to)
    {
        var result = store.Tickets
            .Where(t => t.Tour.Date >= from && t.Tour.Date <= to)
            .SelectMany(t => t.Tour.Exhibitions.Select(e => new { e.Theme, Ticket = t }))
            .GroupBy(x => x.Theme)
            .Select(g =>
            {
                var dailyCounts = g
                    .GroupBy(x => x.Ticket.Tour.Date.Date)
                    .Select(day => day.Count())
                    .ToList();

                return new
                {
                    Theme = g.Key,
                    TotalVisitors = g.Count(),
                    TotalPrice = g.Sum(x => x.Ticket.Price),
                    AverageVisitorsPerDay = dailyCounts.Average(),
                    MinVisitorsPerDay = dailyCounts.Min(),
                    MaxVisitorsPerDay = dailyCounts.Max()
                };
            })
            .ToList();

        return Ok(result);
    }

    [HttpGet("tours-in-hall")]
    public IActionResult GetToursInHall(int hall, DateTime from, DateTime to)
    {
        var result = store.Tours
            .Where(t => t.Date >= from && t.Date <= to)
            .Where(t => t.Exhibitions.Any(e => e.HallNumber == hall))
            .ToList();

        return Ok(result);
    }

    [HttpGet("visitors-by-tour/{tourId:int}")]
    public IActionResult GetVisitorsByTour(int tourId)
    {
        if (store.Tours.All(t => t.Id != tourId))
        {
            return NotFound();
        }

        var result = store.Tickets
            .Where(t => t.Tour.Id == tourId)
            .Select(t => t.Visitor)
            .OrderBy(v => v.FullName)
            .ToList();

        return Ok(result);
    }
}
