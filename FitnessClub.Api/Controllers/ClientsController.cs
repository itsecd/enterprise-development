
using FitnessClub.Api.Dtos.Clients;
using FitnessClub.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для управления клиентами фитнес-клуба.
/// </summary>
[ApiController]
[Route("api/clients")]
public class ClientsController(ClientService service, ILogger<ClientsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить всех клиентов.
    /// </summary>
    [HttpGet]
    public ActionResult<IReadOnlyList<ClientDto>> GetAll()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получить клиентов с просроченными абонементами.
    /// </summary>
    [HttpGet("expired-subscriptions")]
    public ActionResult<IReadOnlyList<ClientDto>> GetExpiredSubscriptions()
    {
        return Ok(service.GetExpiredSubscriptions());
    }

    /// <summary>
    /// Получить клиента по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<ClientDto> GetById(int id)
    {
        var client = service.GetById(id);

        if (client is null)
        {
            return NotFound();
        }

        return Ok(client);
    }

    /// <summary>
    /// Создать нового клиента.
    /// </summary>
    [HttpPost]
    public ActionResult<ClientDto> Create(CreateClientDto dto)
    {
        try
        {
            var client = service.Create(dto);

            logger.LogInformation(
                "Создан клиент с ID {Id}", client.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = client.Id },
                client);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    /// <summary>
    /// Обновить данные клиента.
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, CreateClientDto dto)
    {
        try
        {
            if (!service.Update(id, dto))
            {
                return NotFound();
            }

            logger.LogInformation(
                "Обновлён клиент с ID {Id}", id);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    /// <summary>
    /// Удалить клиента.
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            if (!service.Delete(id))
            {
                return NotFound();
            }

            logger.LogInformation(
                "Удалён клиент с ID {Id}", id);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}
