using AutoService.Contracts.Dtos;
using AutoService.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoService.Api.Controllers;

/// <summary>
/// Контроллер для управления клиентами.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    /// <summary>
    /// Получить список всех клиентов.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll()
    {
        return Ok(await clientService.GetAllAsync());
    }

    /// <summary>
    /// Получить клиента по его идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDto>> GetById(Guid id)
    {
        var client = await clientService.GetByIdAsync(id);
        if (client == null) return NotFound();
        return Ok(client);
    }

    /// <summary>
    /// Добавить нового клиента.
    /// </summary>
    /// <param name="newClient">Данные нового клиента.</param>
    [HttpPost]
    public async Task<ActionResult<ClientDto>> Create([FromBody] ClientDto newClient)
    {
        var created = await clientService.CreateAsync(newClient);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновить данные существующего клиента.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <param name="updatedClient">Новые данные клиента.</param>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] ClientUpdateDto updatedClient)
    {
        await clientService.UpdateAsync(id, updatedClient);
        return NoContent();
    }

    /// <summary>
    /// Удалить клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await clientService.DeleteAsync(id);
        return NoContent();
    }
}