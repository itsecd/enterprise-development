using FoodDelivery.Application.Contracts.Orders;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции с заказами
/// </summary>
/// <param name="service">Прикладная служба заказов</param>
/// <param name="logger">Средство журналирования</param>
public sealed class OrdersController(
    IOrderService service,
    ILogger<OrdersController> logger)
    : CrudControllerBase<OrderDto, OrderRequest>(service, logger)
{
    /// <inheritdoc/>
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, "application/problem+json")]
    public override ActionResult<OrderDto> Create([FromBody] OrderRequest request)
    {
        return base.Create(request);
    }

    /// <inheritdoc/>
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict, "application/problem+json")]
    public override ActionResult<OrderDto> Update(
        int id,
        [FromBody] OrderRequest request)
    {
        return base.Update(id, request);
    }

    /// <inheritdoc/>
    protected override int GetResourceId(OrderDto resource)
    {
        return resource.Id;
    }
}
