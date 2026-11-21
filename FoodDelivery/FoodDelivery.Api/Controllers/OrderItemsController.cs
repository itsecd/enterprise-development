using FoodDelivery.Application.Contracts.Orders;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции с позициями заказа
/// </summary>
/// <param name="service">Прикладная служба позиций</param>
/// <param name="logger">Средство журналирования</param>
[ApiController]
[Route("api/orders/{orderId:int}/items")]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError, "application/problem+json")]
public sealed class OrderItemsController(
    IOrderItemService service,
    ILogger<OrderItemsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает позиции заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <returns>Позиции заказа</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<IReadOnlyList<OrderItemDto>>> GetAll(int orderId, CancellationToken cancellationToken = default)
    {
        var items = await service.GetAllAsync(orderId, cancellationToken);

        logger.LogInformation(
            "Получены позиции заказа {OrderId}, количество: {Count}",
            orderId,
            items.Count);

        return Ok(items);
    }

    /// <summary>
    /// Возвращает позицию заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    /// <returns>Позиция заказа</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpGet("{itemId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<OrderItemDto>> GetById(int orderId, int itemId, CancellationToken cancellationToken = default)
    {
        var item = await service.GetByIdAsync(orderId, itemId, cancellationToken);

        logger.LogInformation(
            "Получена позиция {ItemId} заказа {OrderId}",
            itemId,
            orderId);

        return Ok(item);
    }

    /// <summary>
    /// Добавляет блюдо в заказ
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="request">Данные позиции</param>
    /// <returns>Созданная позиция</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, "application/problem+json")]
    public async Task<ActionResult<OrderItemDto>> Create(
        int orderId,
        [FromBody] AddOrderItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var item = await service.CreateAsync(orderId, request, cancellationToken);

        logger.LogInformation(
            "Добавлена позиция {ItemId} в заказ {OrderId}",
            item.Id,
            orderId);

        return CreatedAtAction(
            nameof(GetById),
            new { orderId, itemId = item.Id },
            item);
    }

    /// <summary>
    /// Изменяет количество в позиции заказа
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    /// <param name="request">Новое количество</param>
    /// <returns>Изменённая позиция</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpPut("{itemId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<OrderItemDto>> Update(
        int orderId,
        int itemId,
        [FromBody] UpdateOrderItemRequest request,
        CancellationToken cancellationToken = default)
    {
        var item = await service.UpdateAsync(orderId, itemId, request, cancellationToken);

        logger.LogInformation(
            "Изменено количество в позиции {ItemId} заказа {OrderId}",
            itemId,
            orderId);

        return Ok(item);
    }

    /// <summary>
    /// Удаляет позицию, если она существует
    /// </summary>
    /// <param name="orderId">Идентификатор заказа</param>
    /// <param name="itemId">Идентификатор позиции</param>
    /// <returns>Ответ без содержимого</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpDelete("{itemId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int orderId, int itemId, CancellationToken cancellationToken = default)
    {
        await service.DeleteAsync(orderId, itemId, cancellationToken);

        logger.LogInformation(
            "Выполнено удаление позиции {ItemId} заказа {OrderId}",
            itemId,
            orderId);

        return NoContent();
    }
}
