using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции для работы с позициями заказов
/// </summary>
[ApiController]
[Route("api/order-items")]
public class OrderItemsController(
    IOrderItemService orderItemService,
    ILogger<OrderItemsController> logger)
    : ControllerBase
{
    /// <summary>
    /// Возвращает все позиции заказов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(
        typeof(IList<OrderItemDto>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<OrderItemDto>>> GetAll()
    {
        try
        {
            logger.LogInformation(
                "Получен запрос на получение всех позиций заказов");

            var orderItems =
                await orderItemService.GetAllAsync();

            return Ok(orderItems);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при получении списка позиций заказов");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Возвращает позицию заказа по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(
        typeof(OrderItemDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderItemDto>> GetById(int id)
    {
        try
        {
            var orderItem =
                await orderItemService.GetAsync(id);

            if (orderItem is null)
            {
                return NotFound(new
                {
                    message = "Позиция заказа не найдена"
                });
            }

            return Ok(orderItem);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при получении позиции заказа {OrderItemId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Создаёт позицию заказа
    /// </summary>
    [HttpPost]
    [ProducesResponseType(
        typeof(OrderItemDto),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderItemDto>> Create(
        OrderItemCreateUpdateDto dto)
    {
        try
        {
            var orderItem =
                await orderItemService.CreateAsync(dto);

            logger.LogInformation(
                "Создана позиция заказа {OrderItemId}",
                orderItem.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = orderItem.Id },
                orderItem);
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(
                exception,
                "Не найдены связанные сущности при создании позиции заказа");

            return BadRequest(new
            {
                message = "Указанный заказ или блюдо не найдены"
            });
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Некорректные данные при создании позиции заказа");

            return BadRequest(new
            {
                message = "Переданы некорректные данные"
            });
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при создании позиции заказа");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Обновляет позицию заказа
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(
        typeof(OrderItemDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderItemDto>> Update(
        int id,
        OrderItemCreateUpdateDto dto)
    {
        try
        {
            var existingOrderItem =
                await orderItemService.GetAsync(id);

            if (existingOrderItem is null)
            {
                return NotFound(new
                {
                    message = "Позиция заказа не найдена"
                });
            }

            var updatedOrderItem =
                await orderItemService.UpdateAsync(dto, id);

            logger.LogInformation(
                "Обновлена позиция заказа {OrderItemId}",
                id);

            return Ok(updatedOrderItem);
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(
                exception,
                "Не найдены связанные сущности при обновлении позиции заказа {OrderItemId}",
                id);

            return BadRequest(new
            {
                message = "Указанный заказ или блюдо не найдены"
            });
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Некорректные данные при обновлении позиции заказа {OrderItemId}",
                id);

            return BadRequest(new
            {
                message = "Переданы некорректные данные"
            });
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при обновлении позиции заказа {OrderItemId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Удаляет позицию заказа
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted =
                await orderItemService.DeleteAsync(id);

            if (deleted)
            {
                logger.LogInformation(
                    "Удалена позиция заказа {OrderItemId}",
                    id);
            }

            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при удалении позиции заказа {OrderItemId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }
}