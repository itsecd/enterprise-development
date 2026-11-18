using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// CRUD-операции над клиентами
/// </summary>
/// <param name="service">Сервис клиентов</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/clients")]
public class ClientsController(IClientService service, ILogger<ClientsController> logger) : ControllerBase
{
    /// <summary>
    /// Создаёт нового клиента
    /// </summary>
    /// <param name="editDto">Данные клиента</param>
    /// <returns>Созданный клиент</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientDto>> Create(ClientEditDto editDto)
    {
        var created = await service.Create(editDto);
        logger.LogInformation("Создан клиент с id={Id}", created.Id);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Возвращает список всех клиентов
    /// </summary>
    /// <returns>Коллекция клиентов</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<ClientDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<ClientDto>>> GetAll() =>
        Ok(await service.ReadAll());

    /// <summary>
    /// Возвращает клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <returns>Клиент</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientDto>> GetById(int id) =>
        Ok(await service.Read(id));

    /// <summary>
    /// Обновляет данные клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="editDto">Новые данные клиента</param>
    /// <returns>Обновлённый клиент</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientDto>> Update(int id, ClientEditDto editDto)
    {
        var updated = await service.Update(id, editDto);
        logger.LogInformation("Обновлён клиент с id={Id}", updated.Id);
        return Ok(updated);
    }

    /// <summary>
    /// Удаляет клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.Delete(id);
        logger.LogInformation("Удалён клиент с id={Id}", id);
        return NoContent();
    }
}