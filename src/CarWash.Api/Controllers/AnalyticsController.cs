using CarWash.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService) : ControllerBase
{
    [HttpGet("top-clients")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetTop5Clients()
        => Ok(await analyticsService.GetTop5ClientsAsync());

    [HttpGet("current-cars")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetCurrentCars([FromQuery] DateTime? time)
        => Ok(await analyticsService.GetCurrentCarsAsync(time));

    [HttpGet("top-services")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetTop5PopularServices()
        => Ok(await analyticsService.GetTop5PopularServicesAsync());

    [HttpGet("box-free-time")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetBoxFreeTime([FromQuery] int boxNumber, [FromQuery] DateTime? time)
        => Ok(await analyticsService.GetBoxFreeTimeAsync(boxNumber, time));

    [HttpGet("revenue")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetRevenueForEachService()
        => Ok(await analyticsService.GetRevenueForEachServiceAsync());
}