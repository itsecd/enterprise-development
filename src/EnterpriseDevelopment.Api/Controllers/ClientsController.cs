using EnterpriseDevelopment.Application.Interfaces;
using EnterpriseDevelopment.Contracts.DTOs.Clients;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseDevelopment.Api.Controllers;

/// <summary>
/// Контроллер для управления клиентами
/// </summary>
[ApiController]
[Route("clients")]
public class ClientsController(
    IClientService service,
    ILogger<ClientsController> logger) : ControllerBase
{
    /// <summary>
    /// Получает всех клиентов
    /// </summary>
    /// <returns>Список клиентов</returns>
    [HttpGet]
    public ActionResult<IEnumerable<ClientDto>> GetAll()
    {
        logger.LogInformation("Запрошен список клиентов");

        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получает клиента по идентификатору
    /// </summary>
    /// <returns>Данные клиента</returns>
    [HttpGet("{id:int}")]
    public ActionResult<ClientDto> GetById(int id)
    {
        logger.LogInformation("Запрошен клиент с ID {ClientId}", id);

        var client = service.GetById(id);

        if (client is null)
        {
            logger.LogWarning("Клиент с ID {ClientId} не найден", id);
            return NotFound();
        }

        return Ok(client);
    }

    /// <summary>
    /// Создаёт клиента
    /// </summary>
    /// <returns>Данные созданного клиента</returns>
    [HttpPost]
    public ActionResult<ClientDto> Create(CreateClientDto dto)
    {
        var client = service.Create(dto);

        logger.LogInformation("Создан клиент с ID {ClientId}", client.Id);

        return CreatedAtAction(
            nameof(GetById),
            new { id = client.Id },
            client);
    }

    /// <summary>
    /// Обновляет клиента
    /// </summary>
    /// <returns>Результат обновления</returns>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UpdateClientDto dto)
    {
        if (!service.Update(id, dto))
        {
            logger.LogWarning("Клиент с ID {ClientId} не найден", id);
            return NotFound();
        }

        logger.LogInformation("Обновлён клиент с ID {ClientId}", id);

        return NoContent();
    }

    /// <summary>
    /// Удаляет клиента
    /// </summary>
    /// <returns>Результат удаления</returns>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        service.Delete(id);

        logger.LogInformation(
            "Обработано удаление клиента с ID {ClientId}", id);

        return NoContent();
    }
}
