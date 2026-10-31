using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ClientDto>))]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
    {
        return Ok(await clientService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(ClientDto))]
    [ProducesResponseType(404)]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        var client = await clientService.GetByIdAsync(id);
        return client == null ? NotFound() : Ok(client);
    }

    [HttpPost]
    [ProducesResponseType(201, Type = typeof(ClientDto))]
    [ProducesResponseType(400)]
    public async Task<ActionResult<ClientDto>> Create([FromBody] ClientEditDto dto)
    {
        var created = await clientService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, [FromBody] ClientEditDto dto)
    {
        try
        {
            await clientService.UpdateAsync(id, dto);
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
        await clientService.DeleteAsync(id);
        return NoContent();
    }
}