using System.ComponentModel.DataAnnotations;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Аналитические запросы автомойки
/// </summary>
[ApiController]
[Route("api/analytics")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json")]
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить пять клиентов с наибольшим количеством посещений
    /// </summary>
    [HttpGet("top-customers")]
    [ProducesResponseType<IReadOnlyList<TopCustomerResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TopCustomerResponse>>> GetTopCustomers(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Запрос аналитики: пять клиентов с наибольшим количеством посещений");
        return Ok(await service.GetTopCustomersAsync(cancellationToken));
    }

    /// <summary>
    /// Получить автомобили, находящиеся сейчас на мойке
    /// </summary>
    [HttpGet("current-cars")]
    [ProducesResponseType<IReadOnlyList<CarResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<CarResponse>>> GetCurrentCars(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Запрос аналитики: автомобили, находящиеся сейчас на мойке");
        return Ok(await service.GetCurrentCarsAsync(cancellationToken));
    }

    /// <summary>
    /// Получить пять наиболее популярных услуг
    /// </summary>
    [HttpGet("top-services")]
    [ProducesResponseType<IReadOnlyList<PopularServiceResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PopularServiceResponse>>> GetTopServices(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Запрос аналитики: пять наиболее популярных услуг");
        return Ok(await service.GetTopServicesAsync(cancellationToken));
    }

    /// <summary>
    /// Получить время освобождения бокса
    /// </summary>
    [HttpGet("boxes/{boxNumber:int}/available-from")]
    [ProducesResponseType<BoxAvailabilityResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest, "application/problem+json")]
    public async Task<ActionResult<BoxAvailabilityResponse>> GetBoxAvailability([Range(1, int.MaxValue, ErrorMessage = "Номер бокса должен быть положительным.")] int boxNumber, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Запрос времени освобождения бокса {BoxNumber}", boxNumber);
        return Ok(await service.GetBoxAvailabilityAsync(boxNumber, cancellationToken));
    }

    /// <summary>
    /// Получить выручку по каждой услуге
    /// </summary>
    [HttpGet("revenue-per-service")]
    [ProducesResponseType<IReadOnlyList<ServiceRevenueResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ServiceRevenueResponse>>> GetRevenuePerService(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Запрос аналитики: выручку по каждой услуге");
        return Ok(await service.GetRevenuePerServiceAsync(cancellationToken));
    }
}
