using DryCleaning.Contracts.Dto;
using DryCleaning.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaning.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetAll() => Ok(await service.GetAllAsync());

    [HttpGet("/api/clients/{clientId:int}/orders")]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetByClientId(int clientId)
    {
        var orders = await service.GetByClientIdAsync(clientId);
        return orders is null ? NotFound() : Ok(orders);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        var o = await service.GetByIdAsync(id);
        return o is null ? NotFound() : Ok(o);
    }

    [HttpGet("in-progress")]
    public async Task<ActionResult<IReadOnlyList<OrderDto>>> GetInProgress() => Ok(await service.GetInProgressAsync());

    [HttpPost]
    public async Task<ActionResult<OrderDto>> Create(OrderCreateDto request)
    {
        var order = await service.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, OrderEditDto request) =>
        await service.UpdateAsync(id, request) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await service.DeleteAsync(id) ? NoContent() : NotFound();
}
