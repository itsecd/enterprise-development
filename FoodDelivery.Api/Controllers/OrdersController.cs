using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Application.Services;
using FoodDelivery.Contracts.Orders;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Контроллер для управления заказами.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService, ILogger<OrdersController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех заказов.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<OrderDto>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение списка всех заказов");
            return Ok(orderService.GetAll());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка заказов");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Получить заказ по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<OrderDto> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос заказа с Id = {Id}", id);
            var order = orderService.GetById(id);
            if (order is null)
            {
                logger.LogWarning("Заказ с Id = {Id} не найден", id);
                return NotFound(new { Message = $"Заказ с Id = {id} не найден." });
            }
            return Ok(order);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении заказа с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Создать новый заказ.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<OrderDto> Create([FromBody] OrderCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Попытка создания заказа для клиента {ClientId} в ресторане {RestaurantId}", dto.ClientId, dto.RestaurantId);
            var created = orderService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning("Ошибка валидации при создании заказа: {Message}", ex.Message);
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Непредвиденная ошибка при создании заказа");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Обновить существующий заказ.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(int id, [FromBody] OrderCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Попытка обновления заказа с Id = {Id}", id);
            var updated = orderService.Update(id, dto);
            if (!updated)
            {
                logger.LogWarning("Заказ с Id = {Id} не найден для обновления", id);
                return NotFound(new { Message = $"Заказ с Id = {id} не найден." });
            }
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning("Ошибка валидации при обновлении заказа Id = {Id}: {Message}", id, ex.Message);
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении заказа с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Удалить заказ по идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            logger.LogInformation("Запрос на удаление заказа с Id = {Id}", id);
            orderService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении заказа с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }
}