using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Контроллер для работы с заказами
/// </summary>
/// <param name="orderService">Сервис для работы с заказами</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public class OrdersController(IOrderService orderService, ILogger<OrdersController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех заказов
    /// </summary>
    /// <returns>Список заказов</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос списка заказов");
            return Ok(await orderService.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка заказов");
            return Problem(detail: "Не удалось получить список заказов", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить заказ по ID
    /// </summary>
    /// <param name="id">ID заказа</param>
    /// <returns>Данные заказа</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(OrderDto))]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос заказа с ID {Id}", id);
            var item = await orderService.GetByIdAsync(id);
            if (item == null)
            {
                logger.LogWarning("Заказ с ID {Id} не найден", id);
                return Problem(detail: $"Заказ с ID {id} не найден", statusCode: StatusCodes.Status404NotFound);
            }

            return Ok(item);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении заказа с ID {Id}", id);
            return Problem(detail: "Не удалось получить заказ", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Создать заказ
    /// </summary>
    /// <param name="dto">Данные заказа</param>
    /// <returns>Созданный объект</returns>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(OrderDto))]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OrderDto>> Create([FromBody] OrderEditDto dto)
    {
        try
        {
            var created = await orderService.CreateAsync(dto);
            logger.LogInformation("Создан заказ с ID {Id}", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не удалось создать заказ: {Message}", ex.Message);
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status400BadRequest);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании заказа");
            return Problem(detail: "Не удалось создать заказ", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Изменить заказ
    /// </summary>
    /// <param name="id">ID заказа</param>
    /// <param name="dto">Новые данные заказа</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] OrderEditDto dto)
    {
        try
        {
            await orderService.UpdateAsync(id, dto);
            logger.LogInformation("Обновлён заказ с ID {Id}", id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не удалось изменить заказ с ID {Id}: {Message}", id, ex.Message);
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при изменении заказа с ID {Id}", id);
            return Problem(detail: "Не удалось изменить заказ", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Удалить заказ
    /// </summary>
    /// <param name="id">ID заказа</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await orderService.DeleteAsync(id);
            logger.LogInformation("Удалён заказ с ID {Id}", id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не удалось удалить заказ с ID {Id}: {Message}", id, ex.Message);
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении заказа с ID {Id}", id);
            return Problem(detail: "Не удалось удалить заказ", statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
