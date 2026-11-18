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
    protected override int GetResourceId(DishDto resource)
    {
        return resource.Id;
    }
}
