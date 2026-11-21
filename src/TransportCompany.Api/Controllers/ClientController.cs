using Microsoft.AspNetCore.Mvc;
using TransportCompany.Contracts.Dto;
using TransportCompany.Contracts.Interfaces;

namespace TransportCompany.Api.Controllers;

/// <summary>
/// Контроллер для управления клиентами транспортной компании
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientController(IClientService clientService, ILogger<ClientController> logger) : ControllerBase
{
    private readonly IClientService _service = clientService;
    private readonly ILogger<ClientController> _logger = logger;

    /// <summary>
    /// Получить список всех клиентов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClientDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<ClientDto>>> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var clients = await _service.GetAllAsync(cancellationToken);
            return Ok(clients);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка клиентов");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ClientDto>> GetById(int id, CancellationToken cancellationToken)
    {
        try
        {
            var client = await _service.GetByIdAsync(id, cancellationToken);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении клиента с Id {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<ClientDto>> Create([FromBody] ClientEditDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var createdClient = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = createdClient.Id }, createdClient);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при создании клиента");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Обновить данные
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Update(int id, [FromBody] ClientEditDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var updated = await _service.UpdateAsync(id, dto, cancellationToken);
            if (updated == null)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при обновлении сущности с Id {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удалить сущность
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        try
        {
            var isDeleted = await _service.DeleteAsync(id, cancellationToken);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении сущности с Id {Id}", id);
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}