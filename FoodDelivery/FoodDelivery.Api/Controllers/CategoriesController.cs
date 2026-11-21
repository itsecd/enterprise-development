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
[Route("api/categories")]
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
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpGet("{id:int}/dishes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<IReadOnlyList<DishDto>>> GetDishes(int id, CancellationToken cancellationToken = default)
    {
        var dishes = await dishService.GetByCategoryAsync(id, cancellationToken);

        logger.LogInformation(
            "Получены блюда категории {CategoryId}, количество: {Count}",
            id,
            dishes.Count);

        return Ok(dishes);
    }
}
