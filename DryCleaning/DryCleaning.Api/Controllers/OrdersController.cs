using Microsoft.AspNetCore.Mvc;
using DryCleaning.Api.Services;
using DryCleaning.Domain.Models;

namespace DryCleaning.Api.Controllers;


/// <summary>
/// Контроллер заказов
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly InMemStore _store;

    public OrdersController(InMemStore store)
    {
        _store = store;
    }

    /// <summary>
    /// Получить все заказы
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Order>> GetAll()
    {
        return Ok(_store.Orders);
    }

    /// <summary>
    /// Получить заказ по Id
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<Order> GetById(int id)
    {
        var order = _store.Orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
        {
            return NotFound(new { message = $"Заказ с Id = {id} не найден" });
        }
        return Ok(order);
    }

    /// <summary>
    /// Создать новый заказ
    /// </summary>
    [HttpPost]
    public ActionResult<Order> Create([FromBody] Order newOrder)
    {
        var client = _store.Clients.FirstOrDefault(c => c.Id == newOrder.ClientId);
        if (client == null)
        {
            return BadRequest(new { message = $"Клиент с Id = {newOrder.ClientId} не найден" });
        }

        var item = _store.Items.FirstOrDefault(i => i.Id == newOrder.ItemId);
        if (item == null)
        {
            return BadRequest(new { message = $"Изделие с Id = {newOrder.ItemId} не найдено" });
        }

        var nextId = _store.Orders.Any() ? _store.Orders.Max(o => o.Id) + 1 : 1;
        newOrder.Id = nextId;
        newOrder.Client = client;
        newOrder.Item = item;

        if (newOrder.AcceptDate == default)
        {
            newOrder.AcceptDate = DateTime.Today;
        }

        _store.Orders.Add(newOrder);

        return CreatedAtAction(nameof(GetById), new { id = newOrder.Id }, newOrder);
    }

    /// <summary>
    /// Обновить параметры заказа
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Order updatedOrder)
    {
        var order = _store.Orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
        {
            return NotFound(new { message = $"Заказ с Id = {id} не найден" });
        }

        var client = _store.Clients.FirstOrDefault(c => c.Id == updatedOrder.ClientId);
        if (client == null)
        {
            return BadRequest(new { message = $"Клиент с Id = {updatedOrder.ClientId} не найден" });
        }

        var item = _store.Items.FirstOrDefault(i => i.Id == updatedOrder.ItemId);
        if (item == null)
        {
            return BadRequest(new { message = $"Изделие с Id = {updatedOrder.ItemId} не найдено" });
        }

        order.ClientId = updatedOrder.ClientId;
        order.Client = client;
        order.ItemId = updatedOrder.ItemId;
        order.Item = item;
        order.AcceptDate = updatedOrder.AcceptDate;
        order.ExeDays = updatedOrder.ExeDays;
        order.Status = updatedOrder.Status;

        return NoContent();
    }

    /// <summary>
    /// Удалить заказ
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var order = _store.Orders.FirstOrDefault(o => o.Id == id);
        if (order == null)
        {
            return NotFound(new { message = $"Заказ с Id = {id} не найден" });
        }

        _store.Orders.Remove(order);
        return NoContent();
    }
}
