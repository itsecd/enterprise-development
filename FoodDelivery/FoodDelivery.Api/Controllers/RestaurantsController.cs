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
    [HttpGet("{id:int}/orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public ActionResult<IReadOnlyList<OrderDto>> GetOrders(int id)
    {
        var orders = orderService.GetByRestaurant(id);

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
    [HttpGet("{id:int}/dishes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public ActionResult<IReadOnlyList<DishDto>> GetDishes(int id)
    {
        var dishes = dishService.GetByRestaurant(id);

        logger.LogInformation(
            "Получены блюда ресторана {RestaurantId}, количество: {Count}",
            id,
            dishes.Count);

        return Ok(dishes);
    }

    /// <inheritdoc/>
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict, "application/problem+json")]
    public override IActionResult Delete(int id)
    {
        return base.Delete(id);
    }
}
