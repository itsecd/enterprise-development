using Carwash.Application.Contracts;
using Carwash.Application.Contracts.Analytics;
using Carwash.Application.Contracts.Cars;
using Microsoft.AspNetCore.Mvc;

namespace Carwash.Api.Controllers;

/// <summary>
/// Контроллер для аналитических запросов к данным приложения
/// </summary>
/// <param name="service">Служба аналитики</param>
/// <param name="logger">Логгер</param>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Получение времени освобождения выбранного бокса
    /// </summary>
    /// <param name="boxNumber">Номер бокса</param>
    /// <returns>Время освобождения</returns>
    [HttpGet("box-release-time/{boxNumber:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<DateTime?>> GetBoxReleaseTime(int boxNumber)
    {
        logger.LogInformation(
            "{method} method of {Pcontroller} is called with parameters: boxNumber={boxNumber}.",
            nameof(GetBoxReleaseTime), GetType().Name, boxNumber);

        try
        {
            var result = await service.GetBoxReleaseTime(boxNumber);

            if (result == null)
            {
                logger.LogWarning(
                    "{method} method of {Pcontroller} completed without a result. Box with number boxNumber={boxNumber} not in Orders.",
                    nameof(GetBoxReleaseTime), GetType().Name, boxNumber);

                return NotFound();
            }

            logger.LogInformation(
                "{method} method of {Pcontroller} completed successfully.",
                nameof(GetBoxReleaseTime), GetType().Name);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting box release time for boxNumber={boxNumber}.", boxNumber);
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Получение автомобилей, находящихся на мойке в данный момент
    /// </summary>
    /// <returns>Список автомобилей</returns>
    [HttpGet("cars-currently-at-car-wash")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<CarDto>>> GetCarsCurrentlyAtCarWash()
    {
        logger.LogInformation(
            "{method} method of {Pcontroller} is called.",
            nameof(GetCarsCurrentlyAtCarWash), GetType().Name);

        try
        {
            var result = await service.GetCarsCurrentlyAtCarWash();

            logger.LogInformation(
                "{method} method of {Pcontroller} completed successfully. Result count: {Count}.",
                nameof(GetCarsCurrentlyAtCarWash), GetType().Name, result?.Count ?? 0);

            return Ok(result ?? []);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting cars currently at car wash.");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Получение топ 5 клиентов по количеству посещений
    /// </summary>
    /// <returns>Список клиентов с количеством посещений</returns>
    [HttpGet("top-5-clients-by-visits")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<ClientVisitDto>>> GetTop5ClientsByVisits()
    {
        logger.LogInformation(
            "{method} method of {Pcontroller} is called.",
            nameof(GetTop5ClientsByVisits), GetType().Name);

        try
        {
            var result = await service.GetTop5ClientsByVisits();

            logger.LogInformation(
                "{method} method of {Pcontroller} completed successfully. Result count: {Count}.",
                nameof(GetTop5ClientsByVisits), GetType().Name, result.Count);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting top 5 clients by visits.");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Получение топ 5 наиболее популярных услуг
    /// </summary>
    /// <returns>Список услуг с количеством их выбора</returns>
    [HttpGet("top-5-popular-services")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<PopularServiceDto>>> GetTop5MostPopularServices()
    {
        logger.LogInformation(
            "{method} method of {Pcontroller} is called.",
            nameof(GetTop5MostPopularServices), GetType().Name);

        try
        {
            var result = await service.GetTop5MostPopularServices();

            logger.LogInformation(
                "{method} method of {Pcontroller} completed successfully. Result count: {Count}.",
                nameof(GetTop5MostPopularServices), GetType().Name, result.Count);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting top 5 popular services.");
            return StatusCode(500, "Internal server error");
        }
    }

    /// <summary>
    /// Получение суммарной выручки по каждой услуге
    /// </summary>
    /// <returns>Список услуг с суммарной выручкой для каждой услуги</returns>
    [HttpGet("total-revenue-by-services")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<ServiceRevenueDto>>> GetTotalRevenueByServices()
    {
        logger.LogInformation(
            "{method} method of {Pcontroller} is called.",
            nameof(GetTotalRevenueByServices), GetType().Name);

        try
        {
            var result = await service.GetTotalRevenueByServices();

            logger.LogInformation(
                "{method} method of {Pcontroller} completed successfully. Result count: {Count}.",
                nameof(GetTotalRevenueByServices), GetType().Name, result.Count);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error occurred while getting total revenue by services.");
            return StatusCode(500, "Internal server error");
        }
    }
}
