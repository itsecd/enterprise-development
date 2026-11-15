using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MuseumSystem.Contracts.Interfaces;
using MuseumSystem.Contracts.Models;

namespace MuseumSystem.Api.Controllers;

/// <summary>
/// Аналитические запросы по данным музея
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AnalyticsController(
    IAnalyticsService service,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Топ-5 выставок по количеству посетителей
    /// </summary>
    [HttpGet("top-exhibitions")]
    [ProducesResponseType(typeof(IEnumerable<TopExhibitionResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TopExhibitionResponse>>> GetTop5Exhibitions()
    {
        try
        {
            var result = await service.GetTop5ExhibitionsByVisitorsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении топ-5 выставок");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Экскурсии с минимальным числом участников
    /// </summary>
    [HttpGet("minimal-participants")]
    [ProducesResponseType(typeof(IEnumerable<ExcursionWithParticipantsResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExcursionWithParticipantsResponse>>> GetMinimalParticipants()
    {
        try
        {
            var result = await service.GetExcursionsWithMinimalParticipantsAsync();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении экскурсий с минимальным числом участников");
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Сводная информация по тематикам за период
    /// </summary>
    [HttpGet("theme-summary")]
    [ProducesResponseType(typeof(IEnumerable<ThemeSummaryResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ThemeSummaryResponse>>> GetThemeSummary(
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        try
        {
            var result = await service.GetThemeSummaryForPeriodAsync(from, to);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении сводки по тематикам за период с {From} по {To}", from, to);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Экскурсии в выбранном зале за период
    /// </summary>
    [HttpGet("excursions-in-hall")]
    [ProducesResponseType(typeof(IEnumerable<ExcursionInHallResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ExcursionInHallResponse>>> GetExcursionsInHall(
        [FromQuery] int hallNumber,
        [FromQuery] DateTime from,
        [FromQuery] DateTime to)
    {
        try
        {
            var result = await service.GetExcursionsInHallForPeriodAsync(hallNumber, from, to);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении экскурсий в зале {HallNumber} за период", hallNumber);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Посетители выбранной экскурсии
    /// </summary>
    [HttpGet("visitors-of-excursion/{excursionId}")]
    [ProducesResponseType(typeof(IEnumerable<VisitorResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<VisitorResponse>>> GetVisitorsOfExcursion(int excursionId)
    {
        try
        {
            var result = await service.GetVisitorsOfSelectedExcursionAsync(excursionId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении посетителей экскурсии с ID {ExcursionId}", excursionId);
            return StatusCode(StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера");
        }
    }
}