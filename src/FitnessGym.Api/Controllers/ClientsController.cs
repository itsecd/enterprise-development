using FitnessGym.Contracts.Dto;
using FitnessGym.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.Api.Controllers;

/// <summary>
/// CRUD-операции и запросы для сущностей Client.
/// </summary>
/// <param name="service">Сервис клиентов.</param>
[ApiController]
[Route("api/clients")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public class ClientsController(IClientService service) : ControllerBase
{
    /// <summary>
    /// Возвращает все записи.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<ClientDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetClients() =>
        Ok(await service.GetClientsAsync());

    /// <summary>
    /// Возвращает запись по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<ClientDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientDto>> GetClient(int id)
    {
        var client = await service.GetClientAsync(id);
        return client is null ? NotFound() : Ok(client);
    }

    /// <summary>
    /// Создаёт запись; идентификатор присваивается сервером.
    /// </summary>
    [HttpPost]
    [ProducesResponseType<ClientDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientDto>> AddClient(UpsertClientDto client)
    {
        var created = await service.AddClientAsync(client);
        return CreatedAtAction(nameof(GetClient), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновляет запись и возвращает обновлённые данные.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType<ClientDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientDto>> UpdateClient(int id, UpsertClientDto client)
    {
        var updated = await service.UpdateClientAsync(id, client);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// Удаляет запись по идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult> DeleteClient(int id) =>
        await service.DeleteClientAsync(id) ? NoContent() : NotFound();

    /// <summary>
    /// Возвращает клиентов с просроченным абонементом, упорядоченных по ФИО.
    /// </summary>
    [HttpGet("expired")]
    [ProducesResponseType<IReadOnlyList<ClientDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetClientsWithExpiredSubscription() =>
        Ok(await service.GetClientsWithExpiredSubscriptionAsync());
}
