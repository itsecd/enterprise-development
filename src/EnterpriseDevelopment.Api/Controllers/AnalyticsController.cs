using EnterpriseDevelopment.Application.Interfaces;
using EnterpriseDevelopment.Contracts.DTOs.Analytics;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseDevelopment.Api.Controllers;

/// <summary>
/// Контроллер аналитики службы доставки еды
/// </summary>
[ApiController]
[Route("analytics")]
public class AnalyticsController(
    IAnalyticsService service,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Получает пять ресторанов с наибольшим количеством заказов
    /// </summary>
    /// <returns>Список ресторанов с количеством заказов</returns>
    [HttpGet("top-restaurants")]
    public ActionResult<IEnumerable<TopRestaurantDto>> GetTopRestaurants()
    {
        logger.LogInformation("Запрошена аналитика популярных ресторанов");

        return Ok(service.GetTopRestaurants());
    }

    /// <summary>
    /// Получает заказы с минимальным временем доставки
    /// </summary>
    /// <returns>Список самых быстрых заказов</returns>
    [HttpGet("fastest-orders")]
    public ActionResult<IEnumerable<FastestOrderDto>> GetFastestOrders()
    {
        logger.LogInformation("Запрошена аналитика времени доставки");

        return Ok(service.GetFastestOrders());
    }

    /// <summary>
    /// Получает уникальных клиентов ресторана
    /// </summary>
    /// <returns>Список клиентов ресторана</returns>
    [HttpGet("restaurants/{restaurantId:int}/clients")]
    public ActionResult<IEnumerable<RestaurantClientDto>> GetRestaurantClients(
        int restaurantId)
    {
        logger.LogInformation(
            "Запрошены клиенты ресторана {RestaurantId}", restaurantId);

        return Ok(service.GetRestaurantClients(restaurantId));
    }

    /// <summary>
    /// Получает статистику блюд по категориям за период
    /// </summary>
    /// <returns>Статистика категорий блюд</returns>
    [HttpGet("category-statistics")]
    public ActionResult<IEnumerable<CategoryStatisticsDto>> GetCategoryStatistics(
        [FromQuery] DateTimeOffset startDate,
        [FromQuery] DateTimeOffset endDate)
    {
        logger.LogInformation(
            "Запрошена статистика категорий за период с {StartDate} по {EndDate}",
            startDate,
            endDate);

        return Ok(service.GetCategoryStatistics(startDate, endDate));
    }

    /// <summary>
    /// Получает клиента с наибольшей суммой заказов
    /// </summary>
    /// <returns>Список клиентов с максимальной суммой заказов</returns>
    [HttpGet("top-clients")]
    public ActionResult<IEnumerable<TopClientDto>> GetTopClients()
    {
        logger.LogInformation("Запрошена аналитика наиболее активных клиентов");

        return Ok(service.GetTopClients());
    }
}
