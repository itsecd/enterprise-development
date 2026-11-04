using DryCleaner.Application.Contracts.DTOs.Item;
using DryCleaner.Application.Contracts.DTOs.ItemCategory;
using DryCleaner.Application.Contracts.DTOs.Customer;
using DryCleaner.Application.Contracts.DTOs.Order;
using DryCleaner.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// HTTP-операции для работы с изделиями
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ItemController(IItemService service, IOrderService orderService, ILogger<ItemController> logger) : ControllerBase
{
    /// <summary>
    /// Получение всех изделий
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IList<ItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<ItemDto>> GetAll()
    {
        try 
        {
            logger.LogInformation("Запрос на получение всех изделий");
            return Ok(service.GetAll()); 
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении изделий");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Получение изделия по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<ItemDto>> Get(int id)
    {
        try
        {
            logger.LogInformation("Запрос: Получение изделия по идентификатору");

            var result = service.Get(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении изделия по идентификатору");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Получение списка заказов, в которых используется конкретное изделие
    /// </summary>
    [HttpGet("{id:int}/orders")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<OrderDto>> GetItemOrders(int id)
    {
        try
        {
            logger.LogInformation("Запрос заказов для изделия с указанным идентификатором");

            var itemExists = service.Get(id) != null;
            if (!itemExists)
            {
                logger.LogWarning("Изделие с указанным идентификатором не найдено");
                return NotFound("Изделие с указанным идентификатором не найдено");
            }

            var orders = orderService.GetByItemId(id);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении заказов изделия с указанным идентификатором");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Создание изделия
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ItemDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<ItemDto>> Create([FromBody] ItemCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Запрос: Создание изделия");

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
            logger.LogError(ex, "Ошибка при создании изделия");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Обновление изделия
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ItemDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<ItemDto>> Update(int id, [FromBody] ItemCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Запрос: Обновление изделия");

            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(service.Update(dto, id));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении изделия");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Удаление изделия
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<ItemDto>> Delete(int id)
    {
        try
        {
            logger.LogInformation("Запрос: Удаление изделия");

            if (!service.Delete(id)) return NotFound();
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении изделия");
            return StatusCode(500, "Ошибка сервера");
        }
    }
}