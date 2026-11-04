using Microsoft.AspNetCore.Mvc;
using Museum.Contracts.Dto;
using Museum.Contracts.Interfaces;

namespace Museum.Api.Controllers;

/// <summary>Обрабатывает запросы аналитики по деятельности музея</summary>
[ApiController]
[Route("api/[controller]")]
public sealed class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    /// <summary>Возвращает пять самых посещаемых выставок</summary>
    [HttpGet("top-exhibitions")]
    public async Task<ActionResult<IReadOnlyList<TopExhibitionDto>>> GetTopExhibitions()
    {
        var result = await analyticsService.GetTopExhibitionsAsync();
        return Ok(result);
    }

    /// <summary>Возвращает экскурсии с минимальным количеством участников</summary>
    [HttpGet("tours-with-min-visitors")]
    public async Task<ActionResult<IReadOnlyList<TourWithVisitorCountDto>>> GetToursWithMinVisitors()
    {
        var result = await analyticsService.GetToursWithMinVisitorsAsync();
        return Ok(result);
    }

    /// <summary>Возвращает сводку посещаемости и выручки по темам выставок за период</summary>
    [HttpGet("attendance-summary")]
    public async Task<ActionResult<IReadOnlyList<AttendanceSummaryDto>>> GetAttendanceSummary(
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        if (from > to)
            return BadRequest("Начало периода не может быть позже его окончания");

        var result = await analyticsService.GetAttendanceSummaryAsync(from, to);
        return Ok(result);
    }

    /// <summary>Возвращает экскурсии, проходящие в выбранном зале за период</summary>
    [HttpGet("tours-in-hall")]
    public async Task<ActionResult<IReadOnlyList<TourDto>>> GetToursInHall(
        [FromQuery] int hall,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        if (hall <= 0)
            return BadRequest("Номер зала должен быть положительным");

        if (from > to)
            return BadRequest("Начало периода не может быть позже его окончания");

        var result = await analyticsService.GetToursInHallAsync(hall, from, to);
        return Ok(result);
    }

    /// <summary>Возвращает посетителей выбранной экскурсии по алфавиту</summary>
    [HttpGet("visitors-by-tour/{tourId:int}")]
    public async Task<ActionResult<IReadOnlyList<VisitorDto>>> GetVisitorsByTour(int tourId)
    {
        var result = await analyticsService.GetVisitorsByTourAsync(tourId);

        if (result is null)
            return NotFound();

        return Ok(result);
    }
}
