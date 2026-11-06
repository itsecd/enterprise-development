using FoodDelivery.Application.Contracts.Analytics;
using FoodDelivery.Application.Contracts.Customers;
using FoodDelivery.Application.Contracts.Orders;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет аналитические запросы службы доставки
/// </summary>
/// <param name="service">Прикладная служба аналитики</param>
/// <param name="logger">Средство журналирования</param>
[ApiController]
[Route("api/analytics")]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError, "application/problem+json")]
public sealed class AnalyticsController(
    IAnalyticsService service,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает пять ресторанов с наибольшим количеством заказов
    /// </summary>
    /// <returns>Рестораны и количество заказов</returns>
    [HttpGet("top-restaurants")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<RestaurantOrderStatisticsDto>> GetTopRestaurants()
    {
        var result = service.GetTopRestaurants();

        logger.LogInformation(
            "Получен топ ресторанов, количество: {Count}",
            result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Возвращает все заказы с минимальным временем доставки
    /// </summary>
    /// <returns>Заказы с минимальным временем доставки</returns>
    [HttpGet("minimum-delivery-orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<OrderDto>> GetMinimumDeliveryOrders()
    {
        var result = service.GetOrdersWithMinimumDeliveryTime();

        logger.LogInformation(
            "Получены заказы с минимальным временем доставки, количество: {Count}",
            result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Возвращает клиентов выбранного ресторана, упорядоченных по ФИО
    /// </summary>
    /// <param name="restaurantId">Идентификатор ресторана</param>
    /// <returns>Клиенты ресторана без повторений</returns>
    [HttpGet("restaurants/{restaurantId:int}/customers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public ActionResult<IReadOnlyList<CustomerDto>> GetRestaurantCustomers(
        int restaurantId)
    {
        var result = service.GetRestaurantCustomers(restaurantId);

        logger.LogInformation(
            "Получены клиенты ресторана {RestaurantId}, количество: {Count}",
            restaurantId,
            result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Возвращает статистику заказов по категориям за включительный период
    /// </summary>
    /// <remarks>
    /// Учитывается полная сумма каждого заказа, содержащего блюда категории
    /// Смешанный заказ может учитываться в нескольких категориях
    /// </remarks>
    /// <param name="request">Границы периода по времени оформления заказа</param>
    /// <returns>Статистика всех категорий, включая категории без заказов</returns>
    [HttpGet("category-statistics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    public ActionResult<IReadOnlyList<CategoryOrderStatisticsDto>> GetCategoryStatistics(
        [FromQuery] OrderPeriodRequest request)
    {
        var result = service.GetCategoryStatistics(
            request.Start!.Value,
            request.End!.Value);

        logger.LogInformation(
            "Получена статистика категорий за период {Start} — {End}",
            request.Start,
            request.End);

        return Ok(result);
    }

    /// <summary>
    /// Возвращает клиента с наибольшей суммой заказов
    /// </summary>
    /// <returns>Сведения о клиенте и общая сумма его заказов</returns>
    [HttpGet("top-spending-customer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public ActionResult<CustomerSpendingDto> GetTopSpendingCustomer()
    {
        var result = service.GetTopSpendingCustomer();

        if (result is null)
        {
            logger.LogInformation(
                "Клиент с наибольшей суммой заказов отсутствует, заказов нет");

            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "В системе нет заказов");
        }

        logger.LogInformation(
            "Получен клиент {CustomerId} с суммой заказов {TotalAmount}",
            result.Customer.Id,
            result.TotalAmount);

        return Ok(result);
    }
}
