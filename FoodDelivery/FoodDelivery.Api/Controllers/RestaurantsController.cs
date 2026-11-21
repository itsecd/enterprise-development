using FoodDelivery.Application.Contracts.Dishes;
using FoodDelivery.Application.Contracts.Common;
using FoodDelivery.Application.Contracts.Orders;
using FoodDelivery.Application.Contracts.Restaurants;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции с ресторанами
/// </summary>
/// <param name="service">Прикладная служба ресторанов</param>
/// <param name="orderService">Прикладная служба заказов</param>
/// <param name="dishService">Прикладная служба блюд</param>
/// <param name="logger">Средство журналирования</param>
[Route("api/restaurants")]
public sealed class RestaurantsController(
    ICrudService<RestaurantDto, RestaurantRequest> service,
    IOrderService orderService,
    IDishService dishService,
    ILogger<RestaurantsController> logger)
    : CrudControllerBase<RestaurantDto, RestaurantRequest>(service, logger)
{
    /// <summary>
    /// Возвращает заказы ресторана
    /// </summary>
    /// <param name="id">Идентификатор ресторана</param>
    /// <returns>Заказы ресторана</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpGet("{id:int}/orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetOrders(int id, CancellationToken cancellationToken = default)
    {
        var orders = await orderService.GetByRestaurantAsync(id, cancellationToken);

        logger.LogInformation(
            "Получены заказы ресторана {RestaurantId}, количество: {Count}",
            id,
            orders.Count);

        return Ok(orders);
    }

    /// <inheritdoc/>
    protected override int GetResourceId(RestaurantDto resource)
    {
        return resource.Id;
    }

    /// <summary>
    /// Возвращает блюда ресторана
    /// </summary>
    /// <param name="id">Идентификатор ресторана</param>
    /// <returns>Блюда ресторана</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpGet("{id:int}/dishes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<IReadOnlyList<DishDto>>> GetDishes(int id, CancellationToken cancellationToken = default)
    {
        var dishes = await dishService.GetByRestaurantAsync(id, cancellationToken);

        logger.LogInformation(
            "Получены блюда ресторана {RestaurantId}, количество: {Count}",
            id,
            dishes.Count);

        return Ok(dishes);
    }
}
