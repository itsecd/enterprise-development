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
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public ActionResult<IReadOnlyList<OrderItemDto>> GetAll(int orderId)
    {
        var items = service.GetAll(orderId);

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
    [HttpGet("{itemId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public ActionResult<OrderItemDto> GetById(int orderId, int itemId)
    {
        var item = service.GetById(orderId, itemId);

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
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, "application/problem+json")]
    public ActionResult<OrderItemDto> Create(
        int orderId,
        [FromBody] AddOrderItemRequest request)
    {
        var item = service.Create(orderId, request);

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
    [HttpPut("{itemId:int}")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status422UnprocessableEntity, "application/problem+json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public ActionResult<OrderItemDto> Update(
        int orderId,
        int itemId,
        [FromBody] UpdateOrderItemRequest request)
    {
        var item = service.Update(orderId, itemId, request);

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
    [HttpDelete("{itemId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int orderId, int itemId)
    {
        service.Delete(orderId, itemId);

        logger.LogInformation(
            "Выполнено удаление позиции {ItemId} заказа {OrderId}",
            itemId,
            orderId);

        return NoContent();
    }
}
