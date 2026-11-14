using DryCleaning.Contracts.Dto;
using DryCleaning.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaning.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetAll() => Ok(await service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        var client = await service.GetByIdAsync(id);
        return client is null ? NotFound() : Ok(client);
    }

    [HttpGet("top")]
    public async Task<ActionResult<IReadOnlyList<TopClientDto>>> GetTopClients([FromQuery] DateTime start, [FromQuery] DateTime end) =>
        Ok(await service.GetTopClientsAsync(start, end));

    [HttpGet("top-spender")]
    public async Task<ActionResult<TopSpenderDto>> GetTopSpender()
    {
        var c = await service.GetTopSpenderAsync();
        return c is null ? NotFound() : Ok(c);
    }

    [HttpGet("longest-processing")]
    public async Task<ActionResult<LongestProcessingDto>> GetLongestProcessing()
    {
        var r = await service.GetLongestProcessingOrderAsync();
        return r is null ? NotFound() : Ok(r);
    }

    [HttpPost]
    public async Task<ActionResult<ClientDto>> Create(ClientEditDto request)
    {
        var c = await service.AddAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = c.ClientId }, c);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, ClientEditDto request) =>
        await service.UpdateAsync(id, request) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) =>
        await service.DeleteAsync(id) ? NoContent() : NotFound();
}
