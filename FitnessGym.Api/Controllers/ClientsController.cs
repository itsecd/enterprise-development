using FitnessGym.Contracts.Dtos;
using FitnessGym.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FitnessGym.Api.Controllers;

/// <summary>
/// Работает с клиентами фитнес-клуба
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService service, ILogger<ClientsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает всех клиентов
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetAll()
    {
        try
        {
            var clients = await service.GetAllAsync();
            return Ok(clients);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(GetAll));
        }
    }

    /// <summary>
    /// Возвращает клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientDto>> GetById(int id)
    {
        try
        {
            var client = await service.GetByIdAsync(id);
            if (client is null)
            {
                logger.LogWarning("Клиент {ClientId} не найден", id);
                return NotFound();
            }
            return Ok(client);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(GetById));
        }
    }

    /// <summary>
    /// Создаёт нового клиента
    /// </summary>
    /// <param name="request">Данные для создания</param>
    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientDto>> Create([FromBody] ClientEditDto request)
    {
        try
        {
            var client = await service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(Create));
        }
    }

    /// <summary>
    /// Обновляет данные клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="request">Новые данные</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] ClientEditDto request)
    {
        try
        {
            var updated = await service.UpdateAsync(id, request);
            if (!updated)
            {
                logger.LogWarning("Клиент {ClientId} для обновления не найден", id);
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(Update));
        }
    }

    /// <summary>
    /// Удаляет клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await service.DeleteAsync(id);
            if (!deleted)
            {
                logger.LogWarning("Клиент {ClientId} для удаления не найден", id);
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(Delete));
        }
    }

    private ObjectResult ServerError(Exception exception, string operation)
    {
        logger.LogError(exception, "Ошибка при выполнении операции {Operation}", operation);
        return Problem(
            statusCode: StatusCodes.Status500InternalServerError,
            title: "Не удалось обработать запрос.");
    }
}