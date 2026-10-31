using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController(IOrderService orderService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<OrderDto>))]
    public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
    {
        return Ok(await orderService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(OrderDto))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<OrderDto>> GetById(int id)
    {
        var order = await orderService.GetByIdAsync(id);
        return order == null ? NotFound() : Ok(order);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(OrderDto))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<OrderDto>> Create([FromBody] OrderEditDto dto)
    {
        try
        {
            var created = await orderService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] OrderEditDto dto)
    {
        try
        {
            await orderService.UpdateAsync(id, dto);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id)
    {
        await orderService.DeleteAsync(id);
        return NoContent();
    }
}