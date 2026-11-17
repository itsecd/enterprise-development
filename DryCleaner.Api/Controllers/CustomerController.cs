using DryCleaner.Application.Contracts.DTOs.Item;
using DryCleaner.Application.Contracts.DTOs.ItemCategory;
using DryCleaner.Application.Contracts.DTOs.Customer;
using DryCleaner.Application.Contracts.DTOs.Order;
using DryCleaner.Application.Contracts.DTOs.Analytics;
using DryCleaner.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// HTTP-операции для работы с клиентами
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CustomerController(ICustomerService service, IOrderService orderService, ILogger<CustomerController> logger) : ControllerBase
{
    /// <summary>
    /// Получение списка всех клиентов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IList<CustomerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<CustomerDto>>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос всех клиентов");
            return Ok(await service.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении клиентов");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Получение клиента по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerDto>> Get(int id)
    {
        try
        {
            logger.LogInformation("Запрос клиента по идентификатору");

            var result = await service.GetAsync(id);
            if (result is null)
            {
                logger.LogWarning("Клиент с указанным идентификатором не найден");
                return NotFound("Клиент с указанным идентификатором не найден");
            }
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении клиента по указанному идентификатору");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Получение списка заказов клиента по его идентификатору
    /// </summary>
    [HttpGet("{id:int}/orders")]
    [ProducesResponseType(typeof(IList<OrderDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<OrderDto>>> GetCustomerOrders(int id)
    {
        try
        {
            logger.LogInformation("Запрос заказов для клиента с указанным идентификатором");

            var customerExists = await service.GetAsync(id) != null;
            if (!customerExists)
            {
                logger.LogWarning("Клиент с указанным идентификатором не найден");
                return NotFound("Клиент с указанным идентификатором не найден");
            }

            var orders = await orderService.GetByCustomerIdAsync(id);
            return Ok(orders);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка заказов для клиента с указанным идентификатором");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Создание клиента
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerDto>> Create([FromBody] CustomerCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Запрос на создание клиента");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании клиента");
            return StatusCode(500, "Ошибка сервера" );
        }
    }

    /// <summary>
    /// Обновление клиента
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CustomerDto>> Update(int id, [FromBody] CustomerCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Запрос на обновление клиента");

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await service.UpdateAsync(dto, id);
            return Ok(updated);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Клиент по указанному идентификатору не найден");
            return NotFound(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении клиента по указанному идентификатору");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Удаление клиента
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            logger.LogInformation("Запрос на удаление клиента");

            var deleted = await service.DeleteAsync(id);
            if (!deleted)
            {
                logger.LogWarning("Клиент не существует или уже удален");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении клиента");
            return StatusCode(500, "Ошибка сервера");
        }
    }
}