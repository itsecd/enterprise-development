using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Application.Contracts.Dishes;
using FoodDelivery.Application.Contracts.Categories;
using FoodDelivery.Application.Contracts.Common;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции с категориями блюд
/// </summary>
/// <param name="service">Прикладная служба категорий</param>
/// <param name="dishService">Прикладная служба блюд</param>
/// <param name="logger">Средство журналирования</param>
public sealed class CategoriesController(
    ICrudService<CategoryDto, CategoryRequest> service,
    IDishService dishService,
    ILogger<CategoriesController> logger)
    : CrudControllerBase<CategoryDto, CategoryRequest>(service, logger)
{
    /// <inheritdoc/>
    protected override int GetResourceId(CategoryDto resource)
    {
        return resource.Id;
    }

    /// <summary>
    /// Возвращает блюда категории
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <returns>Блюда категории</returns>
    [HttpGet("{id:int}/dishes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public ActionResult<IReadOnlyList<DishDto>> GetDishes(int id)
    {
        var dishes = dishService.GetByCategory(id);

        logger.LogInformation(
            "Получены блюда категории {CategoryId}, количество: {Count}",
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
