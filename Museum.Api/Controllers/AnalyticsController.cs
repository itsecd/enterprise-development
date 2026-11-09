using Microsoft.AspNetCore.Mvc;
using Museum.Api.Contracts;
using Museum.Api.Services;

namespace Museum.Api.Controllers;

[ApiController]
[Route("api/analytics")]
public class AnalyticsController(
    MuseumAnalyticsService analytics) : ControllerBase
{
    [HttpGet("top-exhibitions")]
    public ActionResult<IReadOnlyList<ExhibitionVisitorsResponse>>
        GetTopExhibitions()
    {
        return Ok(analytics.GetTopExhibitions());
    }

    [HttpGet("min-participant-tours")]
    public ActionResult<IReadOnlyList<TourParticipantsResponse>>
        GetMinParticipantTours()
    {
        return Ok(analytics.GetMinParticipantTours());
    }

    [HttpGet("attendance-by-theme")]
    public ActionResult<IReadOnlyList<ThemeAttendanceResponse>>
        GetAttendanceByTheme(
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate)
    {
        if (startDate == default ||
            endDate == default ||
            startDate > endDate)
        {
            return BadRequest("Invalid date range.");
        }

        return Ok(analytics.GetAttendanceByTheme(startDate, endDate));
    }

    [HttpGet("tours-in-hall")]
    public ActionResult<IReadOnlyList<HallTourResponse>>
        GetToursInHall(
            [FromQuery] int hallNumber,
            [FromQuery] DateOnly startDate,
            [FromQuery] DateOnly endDate)
    {
        if (hallNumber <= 0 ||
            startDate == default ||
            endDate == default ||
            startDate > endDate)
        {
            return BadRequest("Invalid hall number or date range.");
        }

        return Ok(analytics.GetToursInHall(
            hallNumber,
            startDate,
            endDate
        ));
    }

    [HttpGet("tours/{tourId:int}/visitors")]
    public ActionResult<IReadOnlyList<TourVisitorResponse>>
        GetTourVisitors(int tourId)
    {
        var visitors = analytics.GetTourVisitors(tourId);

        if (visitors is null)
            return NotFound();

        return Ok(visitors);
    }
}
