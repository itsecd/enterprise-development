using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции для работы с клиентами фитнес-клуба.
/// </summary>
[ApiController]
[Route("api/clients")]
[Produces("application/json")]
/// <param name="clientService">Сервис работы с клиентами.</param>
/// <param name="logger">Журнал событий контроллера.</param>
public class ClientsController(
    IClientService clientService,
    ILogger<ClientsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех клиентов.
    /// </summary>
    /// <returns>Список клиентов.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyCollection<ClientDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyCollection<ClientDto>>> GetAll()
    {
        var clients = await clientService.GetAllAsync();

        return Ok(clients);
    }

    /// <summary>
    /// Возвращает клиента по ID.
    /// </summary>
    /// <param name="id">ID клиента.</param>
    /// <returns>Данные клиента.</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        var client = await clientService.GetByIdAsync(id);

        if (client is null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    /// <summary>
    /// Создаёт нового клиента.
    /// </summary>
    /// <param name="dto">Данные нового клиента.</param>
    /// <returns>Созданный клиент.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ClientDto>> Create(
        [FromBody] CreateClientDto dto)
    {
        try
        {
            var client = await clientService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = client.Id },
                client);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Получены некорректные данные при создании клиента.");

            return BadRequest(new { message = exception.Message });
        }
    }

    /// <summary>
    /// Обновляет данные существующего клиента.
    /// </summary>
    /// <param name="id">ID клиента.</param>
    /// <param name="dto">Новые данные клиента.</param>
    /// <returns>Результат обновления.</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(
        int id,
        [FromBody] UpdateClientDto dto)
    {
        try
        {
            var updated = await clientService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Получены некорректные данные при обновлении клиента {ClientId}.",
                id);

            return BadRequest(new { message = exception.Message });
        }
    }

    /// <summary>
    /// Удаляет клиента.
    /// </summary>
    /// <param name="id">ID клиента.</param>
    /// <returns>Результат удаления.</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await clientService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}