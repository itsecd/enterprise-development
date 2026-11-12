using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Application.Services;
using FoodDelivery.Contracts.Analytics;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Контроллер для получения аналитических отчётов по службе доставки.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(IAnalyticsService analyticsService, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить топ-5 ресторанов по количеству заказов.
    /// </summary>
    [HttpGet("top-restaurants")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<TopRestaurantDto>> GetTop5Restaurants()
    {
        try
        {
            logger.LogInformation("Запрос аналитики: топ-5 ресторанов");
            return Ok(analyticsService.GetTop5Restaurants());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при расчёте топ-5 ресторанов");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Получить заказы с минимальным временем доставки.
    /// </summary>
    [HttpGet("min-delivery-orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<MinDeliveryOrderDto>> GetOrdersWithMinDeliveryTime()
    {
        try
        {
            logger.LogInformation("Запрос аналитики: заказы с минимальным временем доставки");
            return Ok(analyticsService.GetOrdersWithMinDeliveryTime());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении заказов с минимальным временем доставки");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Получить клиентов, заказывавших еду в указанном ресторане.
    /// </summary>
    [HttpGet("clients-by-restaurant/{restaurantId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<ClientSummaryDto>> GetClientsByRestaurant(int restaurantId)
    {
        try
        {
            logger.LogInformation("Запрос аналитики: клиенты ресторана Id = {RestaurantId}", restaurantId);
            return Ok(analyticsService.GetClientsByRestaurant(restaurantId));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении клиентов для ресторана Id = {RestaurantId}", restaurantId);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Получить сводную статистику заказов по категориям блюд за выбранный период.
    /// </summary>
    [HttpGet("category-summary")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<CategorySummaryDto>> GetCategorySummary([FromQuery] DateTime start, [FromQuery] DateTime end)
    {
        try
        {
            if (start > end)
            {
                logger.LogWarning("Некорректный диапазон дат в запросе category-summary: {Start} > {End}", start, end);
                return BadRequest(new { Message = "Начальная дата не может быть позже конечной." });
            }

            logger.LogInformation("Запрос аналитики: сводка по категориям с {Start} по {End}", start, end);
            return Ok(analyticsService.GetCategorySummary(start, end));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при формировании сводки по категориям");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Получить клиентов с наибольшей общей суммой заказов.
    /// </summary>
    [HttpGet("top-spenders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<TopSpenderDto>> GetTopSpenders()
    {
        try
        {
            logger.LogInformation("Запрос аналитики: клиенты с наибольшими тратами");
            return Ok(analyticsService.GetTopSpenders());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при расчёте клиентов с наибольшими тратами");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }
}