using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции для работы с заказами
/// </summary>
[ApiController]
[Route("api/orders")]
public class OrdersController(
    IOrderService orderService,
    ILogger<OrdersController> logger)
    : ControllerBase
{
    /// <summary>
    /// Возвращает все заказы
    /// </summary>
    [HttpGet]
    [ProducesResponseType(
        typeof(IList<OrderDto>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<OrderDto>>> GetAll()
    {
        try
        {
            logger.LogInformation(
                "Получен запрос на получение всех заказов");

            var orders = await orderService.GetAllAsync();

            return Ok(orders);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при получении списка заказов");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Возвращает заказ по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(
        typeof(OrderDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        try
        {
            var order = await orderService.GetAsync(id);

            if (order is null)
            {
                return NotFound(new
                {
                    message = "Заказ не найден"
                });
            }

            return Ok(order);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при получении заказа {OrderId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Создаёт заказ
    /// </summary>
    [HttpPost]
    [ProducesResponseType(
        typeof(OrderDto),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderDto>> Create(
        OrderCreateUpdateDto dto)
    {
        try
        {
            var order = await orderService.CreateAsync(dto);

            logger.LogInformation(
                "Создан заказ {OrderId}",
                order.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = order.Id },
                order);
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(
                exception,
                "Не найдена связанная сущность при создании заказа");

            return BadRequest(new
            {
                message = "Указанный покупатель или ресторан не найдены"
            });
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Некорректные данные при создании заказа");

            return BadRequest(new
            {
                message = "Переданы некорректные данные"
            });
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при создании заказа");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Обновляет заказ
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(
        typeof(OrderDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<OrderDto>> Update(
        int id,
        OrderCreateUpdateDto dto)
    {
        try
        {
            var existingOrder =
                await orderService.GetAsync(id);

            if (existingOrder is null)
            {
                return NotFound(new
                {
                    message = "Заказ не найден"
                });
            }

            var updatedOrder =
                await orderService.UpdateAsync(dto, id);

            logger.LogInformation(
                "Обновлён заказ {OrderId}",
                id);

            return Ok(updatedOrder);
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(
                exception,
                "Не найдена связанная сущность при обновлении заказа {OrderId}",
                id);

            return BadRequest(new
            {
                message = "Указанный покупатель или ресторан не найдены"
            });
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Некорректные данные при обновлении заказа {OrderId}",
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
                "Ошибка при обновлении заказа {OrderId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Удаляет заказ
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted =
                await orderService.DeleteAsync(id);

            if (deleted)
            {
                logger.LogInformation(
                    "Удалён заказ {OrderId}",
                    id);
            }

            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при удалении заказа {OrderId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }
}