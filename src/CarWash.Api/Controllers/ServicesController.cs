using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServicesController(IServiceService serviceService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ServiceDto>))]
    public async Task<ActionResult<IEnumerable<ServiceDto>>> GetAll()
    {
        return Ok(await serviceService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(ServiceDto))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ServiceDto>> GetById(int id)
    {
        var service = await serviceService.GetByIdAsync(id);
        return service == null ? NotFound() : Ok(service);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(ServiceDto))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ServiceDto>> Create([FromBody] ServiceEditDto dto)
    {
        var created = await serviceService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] ServiceEditDto dto)
    {
        try
        {
            await serviceService.UpdateAsync(id, dto);
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
        await serviceService.DeleteAsync(id);
        return NoContent();
    }
}