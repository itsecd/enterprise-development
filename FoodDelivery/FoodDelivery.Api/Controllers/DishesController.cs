using FoodDelivery.Application.Contracts.Common;
using FoodDelivery.Application.Contracts.Dishes;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции с блюдами
/// </summary>
/// <param name="service">Прикладная служба блюд</param>
/// <param name="logger">Средство журналирования</param>
public sealed class DishesController(
    ICrudService<DishDto, DishRequest> service,
    ILogger<DishesController> logger)
    : CrudControllerBase<DishDto, DishRequest>(service, logger)
{
    /// <inheritdoc/>
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, "application/problem+json")]
    public override ActionResult<DishDto> Create([FromBody] DishRequest request)
    {
        return base.Create(request);
    }

    /// <inheritdoc/>
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict, "application/problem+json")]
    public override ActionResult<DishDto> Update(
        int id,
        [FromBody] DishRequest request)
    {
        return base.Update(id, request);
    }

    /// <inheritdoc/>
    protected override int GetResourceId(DishDto resource)
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
