using FoodDelivery.Application.Contracts.Common;
using FoodDelivery.Application.Contracts.Customers;
using FoodDelivery.Application.Contracts.Orders;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции с клиентами
/// </summary>
/// <param name="service">Прикладная служба клиентов</param>
/// <param name="orderService">Прикладная служба заказов</param>
/// <param name="logger">Средство журналирования</param>
public sealed class CustomersController(
    ICrudService<CustomerDto, CustomerRequest> service,
    IOrderService orderService,
    ILogger<CustomersController> logger)
    : CrudControllerBase<CustomerDto, CustomerRequest>(service, logger)
{
    /// <summary>
    /// Возвращает заказы клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Заказы клиента</returns>
    [HttpGet("{id:int}/orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public ActionResult<IReadOnlyList<OrderDto>> GetOrders(int id)
    {
        var orders = orderService.GetByCustomer(id);

        logger.LogInformation(
            "Получены заказы клиента {CustomerId}, количество: {Count}",
            id,
            orders.Count);

        return Ok(orders);
    }

    /// <inheritdoc/>
    protected override int GetResourceId(CustomerDto resource)
    {
        return resource.Id;
    }

    /// <inheritdoc/>
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict, "application/problem+json")]
    public override IActionResult Delete(int id)
    {
        return base.Delete(id);
    }
}
