using CarWash.Domain.Entities;
using CarWash.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(InMemoryDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Order>> GetAll() => Ok(dataStore.Orders);

    [HttpGet("{id}")]
    public ActionResult<Order> GetById(int id)
    {
        var order = dataStore.Orders.FirstOrDefault(o => o.Id == id);
        return order is null ? NotFound() : Ok(order);
    }

    [HttpPost]
    public ActionResult<Order> Create([FromBody] Order order)
    {
        order.Client = dataStore.Clients.FirstOrDefault(c => c.Id == order.Client.Id)!;
        order.Car = dataStore.Cars.FirstOrDefault(c => c.Id == order.Car.Id)!;
        order.Service = dataStore.Services.FirstOrDefault(s => s.Id == order.Service.Id)!;

        if (order.Client is null || order.Car is null || order.Service is null)
        {
            return BadRequest("Указан несуществующий клиент, машина или услуга.");
        }

        order.Id = dataStore.Orders.Any() ? dataStore.Orders.Max(o => o.Id) + 1 : 1;
        dataStore.Orders.Add(order);

        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Order updatedOrder)
    {
        var order = dataStore.Orders.FirstOrDefault(o => o.Id == id);
        if (order is null) return NotFound();

        var client = dataStore.Clients.FirstOrDefault(c => c.Id == updatedOrder.Client.Id);
        var car = dataStore.Cars.FirstOrDefault(c => c.Id == updatedOrder.Car.Id);
        var service = dataStore.Services.FirstOrDefault(s => s.Id == updatedOrder.Service.Id);

        if (client is null || car is null || service is null)
        {
            return BadRequest("Указан несуществующий клиент, машина или услуга.");
        }

        order.Client = client;
        order.Car = car;
        order.Service = service;
        order.StartTime = updatedOrder.StartTime;
        order.BoxNumber = updatedOrder.BoxNumber;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var order = dataStore.Orders.FirstOrDefault(o => o.Id == id);
        if (order is null) return NotFound();

        dataStore.Orders.Remove(order);
        return NoContent();
    }
}