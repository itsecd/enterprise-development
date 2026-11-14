using System.ComponentModel.DataAnnotations;
using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Контроллер аналитических запросов по автомойке
/// </summary>
/// <param name="analyticsService">Сервис аналитики</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public class AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить топ-5 клиентов по количеству посещений
    /// </summary>
    /// <returns>Клиенты и количество их посещений</returns>
    [HttpGet("top-clients")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<TopClientDto>))]
    public async Task<ActionResult<IEnumerable<TopClientDto>>> GetTop5Clients()
    {
        try
        {
            logger.LogInformation("Запрос топ-5 клиентов по посещениям");
            return Ok(await analyticsService.GetTop5ClientsAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении топ-5 клиентов");
            return Problem(detail: "Не удалось получить топ клиентов", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить автомобили, находящиеся на мойке в указанный момент времени
    /// </summary>
    /// <param name="time">Момент времени (если не указан, используется текущее время)</param>
    /// <returns>Автомобили на мойке</returns>
    [HttpGet("current-cars")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CurrentCarDto>))]
    public async Task<ActionResult<IEnumerable<CurrentCarDto>>> GetCurrentCars([FromQuery] DateTime? time)
    {
        try
        {
            logger.LogInformation("Запрос автомобилей на мойке, время: {Time}", time);
            return Ok(await analyticsService.GetCurrentCarsAsync(time));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении автомобилей на мойке");
            return Problem(detail: "Не удалось получить автомобили на мойке", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить топ-5 наиболее популярных услуг
    /// </summary>
    /// <returns>Услуги и количество их заказов</returns>
    [HttpGet("top-services")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<TopServiceDto>))]
    public async Task<ActionResult<IEnumerable<TopServiceDto>>> GetTop5PopularServices()
    {
        try
        {
            logger.LogInformation("Запрос топ-5 популярных услуг");
            return Ok(await analyticsService.GetTop5PopularServicesAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении топ-5 услуг");
            return Problem(detail: "Не удалось получить топ услуг", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Узнать, с какого времени освобождается бокс мойки
    /// </summary>
    /// <param name="boxNumber">Номер бокса</param>
    /// <param name="time">Момент времени (если не указан, используется текущее время)</param>
    /// <returns>Время освобождения бокса</returns>
    [HttpGet("box-free-time")]
    [ProducesResponseType(200, Type = typeof(BoxFreeTimeDto))]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BoxFreeTimeDto>> GetBoxFreeTime(
        [FromQuery, Range(1, 100, ErrorMessage = "Номер бокса должен быть от 1 до 100")] int boxNumber,
        [FromQuery] DateTime? time)
    {
        try
        {
            logger.LogInformation("Запрос времени освобождения бокса {BoxNumber}, время: {Time}", boxNumber, time);
            return Ok(await analyticsService.GetBoxFreeTimeAsync(boxNumber, time));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении времени освобождения бокса {BoxNumber}", boxNumber);
            return Problem(detail: "Не удалось получить время освобождения бокса", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить суммарную выручку по каждой услуге
    /// </summary>
    /// <returns>Словарь: название услуги и суммарная выручка</returns>
    [HttpGet("revenue")]
    [ProducesResponseType(200, Type = typeof(Dictionary<string, decimal>))]
    public async Task<ActionResult<Dictionary<string, decimal>>> GetRevenueForEachService()
    {
        try
        {
            logger.LogInformation("Запрос суммарной выручки по услугам");
            return Ok(await analyticsService.GetRevenueForEachServiceAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении выручки по услугам");
            return Problem(detail: "Не удалось получить выручку по услугам", statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
