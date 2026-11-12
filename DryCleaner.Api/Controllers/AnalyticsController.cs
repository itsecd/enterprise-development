using DryCleaner.Application.Contracts.Interfaces;
using DryCleaner.Application.Contracts.DTOs.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// API для получения аналитики
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Получает заказы, находящиеся в обработке
    /// </summary>
    [HttpGet("in-progress-orders")]
    [ProducesResponseType(typeof(IList<OrderInProgressDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<OrderInProgressDto>> GetInProgressOrders()
    {
        try
        {
            logger.LogInformation("Запрос на получение заказов в обработке");
            return Ok(analyticsService.GetOrdersInProgress());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка в получении заказов в обработке");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Получает ТОП-5 клиентов по числу сданных изделий за выбранный период
    /// </summary>
    [HttpGet("top-5-customers")]
    [ProducesResponseType(typeof(IList<CustomerOrderCountDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<CustomerOrderCountDto>> GetTop5Customers([FromQuery] DateOnly start, [FromQuery] DateOnly end)
    {
        if (end < start)
        {
            logger.LogWarning("Некорректный период: дата окончания раньше даты начала");
            return BadRequest("Дата окончания не может быть раньше даты начала");
        }
        try
        {
            logger.LogInformation("Запрос: Топ-5 клиентов за период");

            return Ok(analyticsService.GetTop5CustomersByItems(start, end));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка в получении Топ-5 клиентов за период");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Получает клиентов с наибольшим временем обработки заказов
    /// </summary>
    [HttpGet("longest-processing")]
    [ProducesResponseType(typeof(IList<OrderProcessingTimeDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<OrderProcessingTimeDto>> GetLongestProcessing()
    {
        try
        {
            logger.LogInformation("Запрос: Клиенты с наибольшим временем обработки заказов");
            return Ok(analyticsService.GetCustomersWithLongestProcessing());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка в получении клиентов с наибольшим временем обработки заказов");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Получает наиболее популярные категории
    /// </summary>
    [HttpGet("popular-categories")]
    [ProducesResponseType(typeof(IList<CategoryPopularityDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<CategoryPopularityDto>> GetPopularCategories([FromQuery] DateOnly start, [FromQuery] DateOnly end)
    {
        try
        {
            logger.LogInformation("Запрос: Наиболее популярные категории");
            return Ok(analyticsService.GetTop5PopularCategories(start, end));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка в получении наиболее популярных категорий");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Получает наименее популярные категории
    /// </summary>
    [HttpGet("unpopular-categories")]
    [ProducesResponseType(typeof(IList<CategoryPopularityDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<CategoryPopularityDto>> GetUnpopularCategories([FromQuery] DateOnly start, [FromQuery] DateOnly end)
    {
        try
        {
            logger.LogInformation("Запрос: Наименее популярные категории");
            return Ok(analyticsService.GetTop5UnpopularCategories(start, end));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении непопулярных категорий");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Получает клиента с наибольшими тратами
    /// </summary>
    [HttpGet("top-spender")]
    [ProducesResponseType(typeof(TotalSpentCustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TotalSpentCustomerDto> GetTopSpender()
    {
        try
        {
            logger.LogInformation("Запрос: Клиент с наибольшими тратами");
            var result = analyticsService.GetCustomerWithMaxTotalSpent();
            if (result is null) return NotFound("Клиент не найден");
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка в получении клиента с наибольшими тратами");
            return StatusCode(500, "Ошибка сервера");
        }
    }
}