using FoodDelivery.Application.Contracts.Analytics;
using FoodDelivery.Application.Contracts.Customers;
using FoodDelivery.Application.Contracts.Orders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpGet("top-restaurants")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<RestaurantOrderStatisticsDto>>> GetTopRestaurants(CancellationToken cancellationToken = default)
    {
        var result = await service.GetTopRestaurantsAsync(cancellationToken);

        logger.LogInformation(
            "Получен топ ресторанов, количество: {Count}",
            result.Count);

        return Ok(result);
    }

    /// <summary>
    /// Возвращает все заказы с минимальным временем доставки
    /// </summary>
    /// <returns>Заказы с минимальным временем доставки</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpGet("minimum-delivery-orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetMinimumDeliveryOrders(CancellationToken cancellationToken = default)
    {
        var result = await service.GetOrdersWithMinimumDeliveryTimeAsync(cancellationToken);

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
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpGet("restaurants/{restaurantId:int}/customers")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<IReadOnlyList<CustomerDto>>> GetRestaurantCustomers(
        int restaurantId,
        CancellationToken cancellationToken = default)
    {
        var result = await service.GetRestaurantCustomersAsync(restaurantId, cancellationToken);

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
    /// <param name="start">Начало периода</param>
    /// <param name="end">Конец периода</param>
    /// <returns>Статистика всех категорий, включая категории без заказов</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpGet("category-statistics")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    public async Task<ActionResult<IReadOnlyList<CategoryOrderStatisticsDto>>> GetCategoryStatistics(
        [FromQuery, BindRequired] DateTime start,
        [FromQuery, BindRequired] DateTime end,
        CancellationToken cancellationToken = default)
    {
        if (start > end)
        {
            ModelState.AddModelError(nameof(end), "Конец периода не может быть раньше начала");
            return ValidationProblem();
        }

        var result = await service.GetCategoryStatisticsAsync(start, end, cancellationToken);

        logger.LogInformation(
            "Получена статистика категорий за период {Start} — {End}",
            start,
            end);

        return Ok(result);
    }

    /// <summary>
    /// Возвращает клиента с наибольшей суммой заказов
    /// </summary>
    /// <returns>Сведения о клиенте и общая сумма его заказов</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpGet("top-spending-customer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<CustomerSpendingDto>> GetTopSpendingCustomer(CancellationToken cancellationToken = default)
    {
        var result = await service.GetTopSpendingCustomerAsync(cancellationToken);

        logger.LogInformation(
            "Получен клиент {CustomerId} с суммой заказов {TotalAmount}",
            result.Customer.Id,
            result.TotalAmount);

        return Ok(result);
    }
}
