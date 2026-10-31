using CarWash.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Host.Controllers;

/// <summary>
/// Контроллер аналитических запросов автомойки
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AnalyticsController(
    IAnalyticsService service,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Получает топ 5 клиентов по количеству посещений автомойки
    /// </summary>
    [HttpGet("top5-customers")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<TopCustomerDto>>
        GetTopFiveCustomersByVisitCount()
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller}",
            nameof(GetTopFiveCustomersByVisitCount), GetType().Name);

        try
        {
            var result = service.GetTopFiveCustomersByVisitCount();

            logger.LogInformation(
                "Метод {method} контроллера {controller} успешно выполнен",
                nameof(GetTopFiveCustomersByVisitCount), GetType().Name);

            return result.Count > 0 ? Ok(result) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при выполнении метода {method} контроллера {controller}",
                nameof(GetTopFiveCustomersByVisitCount), GetType().Name);

            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получает автомобили, находящиеся на мойке в данный момент
    /// </summary>
    [HttpGet("cars-at-wash")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<int>> GetCarsCurrentlyAtCarWash()
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller}",
            nameof(GetCarsCurrentlyAtCarWash), GetType().Name);

        try
        {
            var result = service.GetCarsCurrentlyAtCarWash();

            logger.LogInformation(
                "Метод {method} контроллера {controller} успешно выполнен",
                nameof(GetCarsCurrentlyAtCarWash), GetType().Name);

            return result.Count > 0 ? Ok(result) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при выполнении метода {method} контроллера {controller}",
                nameof(GetCarsCurrentlyAtCarWash), GetType().Name);

            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получает топ 5 наиболее популярных процедур автомойки
    /// </summary>
    [HttpGet("top5-procedures")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<List<int>> GetTopPopularProcedures()
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller}",
            nameof(GetTopPopularProcedures), GetType().Name);

        try
        {
            var result = service.GetTopPopularProcedures();

            logger.LogInformation(
                "Метод {method} контроллера {controller} успешно выполнен",
                nameof(GetTopPopularProcedures), GetType().Name);

            return result.Count > 0 ? Ok(result) : NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при выполнении метода {method} контроллера {controller}",
                nameof(GetTopPopularProcedures), GetType().Name);

            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получает время освобождения выбранного моечного бокса
    /// </summary>
    [HttpGet("wash-box-release-time")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<DateTime> GetWashBoxReleaseTime(
        [FromQuery] int washBoxNumber)
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller} с параметром {washBoxNumber}",
            nameof(GetWashBoxReleaseTime), GetType().Name, washBoxNumber);

        try
        {
            var result = service.GetWashBoxReleaseTime(washBoxNumber);

            logger.LogInformation(
                "Метод {method} контроллера {controller} успешно выполнен",
                nameof(GetWashBoxReleaseTime), GetType().Name);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при выполнении метода {method} контроллера {controller}",
                nameof(GetWashBoxReleaseTime), GetType().Name);

            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Рассчитывает суммарную выручку по выбранной процедуре
    /// </summary>
    [HttpGet("procedure-revenue")]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<decimal> CalculateProcedureRevenue(
        [FromQuery] int procedureId)
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller} с параметром {procedureId}",
            nameof(CalculateProcedureRevenue), GetType().Name, procedureId);

        try
        {
            var result = service.CalculateProcedureRevenue(procedureId);

            logger.LogInformation(
                "Метод {method} контроллера {controller} успешно выполнен",
                nameof(CalculateProcedureRevenue), GetType().Name);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при выполнении метода {method} контроллера {controller}",
                nameof(CalculateProcedureRevenue), GetType().Name);

            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}
