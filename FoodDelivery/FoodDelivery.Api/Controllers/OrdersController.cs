using FoodDelivery.Application.Contracts.Orders;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции с заказами
/// </summary>
/// <param name="service">Прикладная служба заказов</param>
/// <param name="logger">Средство журналирования</param>
[Route("api/orders")]
public sealed class OrdersController(
    IOrderService service,
    ILogger<OrdersController> logger)
    : CrudControllerBase<OrderDto, OrderRequest>(service, logger)
{
    /// <inheritdoc/>
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, "application/problem+json")]
    public override async Task<ActionResult<OrderDto>> Create(
        [FromBody] OrderRequest request, CancellationToken cancellationToken = default)
    {
        var resource = await Service.CreateAsync(request, cancellationToken);
        var id = GetResourceId(resource);

        Logger.LogInformation("Создан ресурс {ResourceType} с идентификатором {Id}", nameof(OrderDto), id);

        return CreatedAtAction(nameof(GetById), new { id }, resource);
    }

    /// <inheritdoc/>
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, "application/problem+json")]
    public override async Task<ActionResult<OrderDto>> Update(
        int id, [FromBody] OrderRequest request, CancellationToken cancellationToken = default)
    {
        var resource = await Service.UpdateAsync(id, request, cancellationToken);

        Logger.LogInformation("Изменён ресурс {ResourceType} с идентификатором {Id}", nameof(OrderDto), id);

        return Ok(resource);
    }

    /// <inheritdoc/>
    protected override int GetResourceId(OrderDto resource)
    {
        return resource.Id;
    }
}
