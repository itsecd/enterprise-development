using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции для работы с заказами
/// </summary>
[ApiController]
[Route("api/orders")]
public class OrdersController(IOrderService orderService, ILogger<OrdersController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает все заказы
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IList<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<OrderDto>> GetAll()
    {
        try
        {
            logger.LogInformation("Получен запрос на получение всех заказов");
            return Ok(orderService.GetAll());
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при получении списка заказов");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Возвращает заказ по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<OrderDto> GetById(int id)
    {
        try
        {
            var order = orderService.Get(id);

            if (order is null)
            {
                return NotFound(new { message = "Заказ не найден" });
            }

            return Ok(order);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при получении заказа {OrderId}", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Создаёт заказ
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<OrderDto> Create(OrderCreateUpdateDto dto)
    {
        try
        {
            var order = orderService.Create(dto);
            logger.LogInformation("Создан заказ {OrderId}", order.Id);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(exception, "Не найдена связанная сущность при создании заказа");

            return BadRequest(new
            {
                message = "Указанный покупатель или ресторан не найдены"
            });
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Некорректные данные при создании заказа");
            return BadRequest(new { message = "Переданы некорректные данные" });
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при создании заказа");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Обновляет заказ
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<OrderDto> Update(int id, OrderCreateUpdateDto dto)
    {
        try
        {
            if (orderService.Get(id) is null)
            {
                return NotFound(new { message = "Заказ не найден" });
            }

            return Ok(orderService.Update(dto, id));
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(exception, "Не найдена связанная сущность при обновлении заказа {OrderId}", id);

            return BadRequest(new
            {
                message = "Указанный покупатель или ресторан не найдены"
            });
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Некорректные данные при обновлении заказа {OrderId}", id);
            return BadRequest(new { message = "Переданы некорректные данные" });
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при обновлении заказа {OrderId}", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Удаляет заказ
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            var deleted = orderService.Delete(id);

            if (deleted)
            {
                logger.LogInformation("Удалён заказ {OrderId}", id);
            }

            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при удалении заказа {OrderId}", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }
}