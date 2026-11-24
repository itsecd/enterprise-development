using Microsoft.AspNetCore.Mvc;
using Museum.Application.Services;
using Museum.Contracts;

namespace Museum.Api.Controllers;

/// <summary>
/// Предоставляет аналитические запросы по данным музея.
/// </summary>
[ApiController]
[Route("api/analytics")]
public class AnalyticsController(MuseumAnalyticsService analytics, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>Возвращает пять выставок с наибольшим числом посетителей.</summary>
    [HttpGet("top-exhibitions")]
    [ProducesResponseType(typeof(IReadOnlyList<ExhibitionVisitorsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<ExhibitionVisitorsResponse>> GetTopExhibitions()
    {
        try
        {
            return Ok(analytics.GetTopExhibitions());
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка аналитического запроса top-exhibitions");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Возвращает экскурсии с минимальным количеством участников.</summary>
    [HttpGet("min-participant-tours")]
    [ProducesResponseType(typeof(IReadOnlyList<TourParticipantsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<TourParticipantsResponse>> GetMinParticipantTours()
    {
        try
        {
            return Ok(analytics.GetMinParticipantTours());
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка аналитического запроса min-participant-tours");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Возвращает статистику посещаемости по тематике за период.</summary>
    [HttpGet("attendance-by-theme")]
    [ProducesResponseType(typeof(IReadOnlyList<ThemeAttendanceResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<ThemeAttendanceResponse>> GetAttendanceByTheme(
        [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        try
        {
            if (startDate == default || endDate == default || startDate > endDate)
                return BadRequest("Invalid date range.");
            return Ok(analytics.GetAttendanceByTheme(startDate, endDate));
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка аналитического запроса attendance-by-theme");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Возвращает экскурсии в выбранном зале за период.</summary>
    [HttpGet("tours-in-hall")]
    [ProducesResponseType(typeof(IReadOnlyList<HallTourResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<HallTourResponse>> GetToursInHall(
        [FromQuery] int hallNumber, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        try
        {
            if (hallNumber <= 0 || startDate == default || endDate == default || startDate > endDate)
                return BadRequest("Invalid hall number or date range.");
            return Ok(analytics.GetToursInHall(hallNumber, startDate, endDate));
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка аналитического запроса tours-in-hall");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }

    /// <summary>Возвращает посетителей экскурсии в порядке ФИО.</summary>
    [HttpGet("tours/{tourId:int}/visitors")]
    [ProducesResponseType(typeof(IReadOnlyList<TourVisitorResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<TourVisitorResponse>> GetTourVisitors(int tourId)
    {
        try
        {
            var visitors = analytics.GetTourVisitors(tourId);
            return visitors is null ? NotFound() : Ok(visitors);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка аналитического запроса visitors, TourId={TourId}", tourId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
        }
    }
}
