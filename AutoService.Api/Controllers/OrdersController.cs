using AutoService.Contracts.Dtos;
using AutoService.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoService.Api.Controllers;

/// <summary>
/// Контроллер для управления заказами.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    /// <summary>
    /// Получить список всех заказов.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        return Ok(await orderService.GetAllAsync());
    }

    /// <summary>
    /// Получить заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDto>> GetById(Guid id)
    {
        var order = await orderService.GetByIdAsync(id);
        if (order == null) return NotFound();
        return Ok(order);
    }

    /// <summary>
    /// Добавить новый заказ.
    /// </summary>
    /// <param name="newOrder">Данные нового заказа.</param>
    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create([FromBody] OrderDto newOrder)
    {
        var created = await orderService.CreateAsync(newOrder);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновить данные заказа.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    /// <param name="updatedOrder">Новые данные заказа.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] OrderUpdateDto updatedOrder)
    {
        await orderService.UpdateAsync(id, updatedOrder);
        return NoContent();
    }

    /// <summary>
    /// Удалить заказ по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор заказа.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await orderService.DeleteAsync(id);
        return NoContent();
    }
}