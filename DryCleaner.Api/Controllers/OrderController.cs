using DryCleaner.Application.Contracts.DTOs.Item;
using DryCleaner.Application.Contracts.DTOs.ItemCategory;
using DryCleaner.Application.Contracts.DTOs.Customer;
using DryCleaner.Application.Contracts.DTOs.Order;
using DryCleaner.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// HTTP-операции для работы с заказами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService service, ILogger<OrderController> logger) : ControllerBase
{
    /// <summary>
    /// Получение всех заказов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IList<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<OrderDto>> GetAll()
    {
        try 
        {
            logger.LogInformation("Запрос на получение всех заказов");
            return Ok(service.GetAll()); 
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении заказов");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Получение заказов по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<OrderDto>> Get(int id)
    {
        try
        {
            logger.LogInformation("Запрос: Получение заказа по идентификатору");

            var result = service.Get(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении заказа");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создание заказа
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<OrderDto>> Create([FromBody] OrderCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Запрос: Создание заказа");

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = service.Create(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании заказа");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Обновление заказа
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<OrderDto>> Update(int id, [FromBody] OrderCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Запрос: Обновление заказа");

            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(service.Update(dto, id));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении заказа");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Удаление заказа
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<OrderDto>> Delete(int id)
    {
        try
        {
            logger.LogInformation("Запрос: Удаление заказа");

            if (!service.Delete(id)) return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении заказа");
            return StatusCode(500, "Ошибка сервера");
        }
    }
}