using FoodDelivery.Application.Contracts.Dishes;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции с блюдами
/// </summary>
/// <param name="service">Прикладная служба блюд</param>
/// <param name="logger">Средство журналирования</param>
[Route("api/dishes")]
public sealed class DishesController(
    IDishService service,
    ILogger<DishesController> logger)
    : CrudControllerBase<DishDto, DishRequest>(service, logger)
{
    /// <inheritdoc/>
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, "application/problem+json")]
    public override async Task<ActionResult<DishDto>> Create(
        [FromBody] DishRequest request, CancellationToken cancellationToken = default)
    {
        var resource = await Service.CreateAsync(request, cancellationToken);
        var id = GetResourceId(resource);

        Logger.LogInformation("Создан ресурс {ResourceType} с идентификатором {Id}", nameof(DishDto), id);

        return CreatedAtAction(nameof(GetById), new { id }, resource);
    }

    /// <inheritdoc/>
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, "application/problem+json")]
    public override async Task<ActionResult<DishDto>> Update(
        int id, [FromBody] DishRequest request, CancellationToken cancellationToken = default)
    {
        var resource = await Service.UpdateAsync(id, request, cancellationToken);

        Logger.LogInformation("Изменён ресурс {ResourceType} с идентификатором {Id}", nameof(DishDto), id);

        return Ok(resource);
    }

    /// <inheritdoc/>
    protected override int GetResourceId(DishDto resource)
    {
        return resource.Id;
    }
}
