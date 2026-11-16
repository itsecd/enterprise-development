using Microsoft.AspNetCore.Mvc;
using DryCleaning.Infrastructure.Repositories;
using DryCleaning.Domain.Models;
using DryCleaning.Api.Dtos;

namespace DryCleaning.Api.Controllers;


/// <summary>
/// Клиентский контроллер
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientRepository repository, ILogger<ClientsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех клиентов
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<ClientDto>> GetAll()
    {
        logger.LogInformation("Запрос всего списка объектов(клиентов)");
        var clients = repository.GetAll();
        var result = clients.Select(c => new ClientDto(c.Id, c.FullName, c.PhoneNumber));
        return Ok(result);
    }

    /// <summary>
    /// Получить клиента по Id
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<ClientDto> GetById(int id)
    {
        logger.LogInformation("Запрос конкретного объекта(клиента) из доступных");
        var client = repository.GetById(id);
        if (client == null)
        {
            logger.LogWarning("Клиент с Id = {id} отсутствует", id);
            return NotFound(new { message = $"Клиент с Id = {id} не найден" });
        }

        var result = new ClientDto(client.Id, client.FullName, client.PhoneNumber);
        return Ok(result);
    }

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    [HttpPost]
    public ActionResult<ClientDto> Create([FromBody] CreateClientDto dto)
    {
        logger.LogInformation("Создание нового объекта(клиента)");
        var client = new Client
        {
            FullName = dto.FullName,
            PhoneNumber = dto.PhoneNumber
        };

        repository.Add(client);

        var result = new ClientDto(client.Id, client.FullName, client.PhoneNumber);
        return CreatedAtAction(nameof(GetById), new { id = client.Id }, result);
    }

    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] UpdateClientDto dto)
    {
        logger.LogInformation("Обновление объекта(клиента)");
        var client = repository.GetById(id);
        if (client == null)
        {
            logger.LogWarning("Клиент с Id = {id} отсутствует", id);
            return NotFound(new { message = $"Клиент с Id = {id} не найден" });
        }

        client.FullName = dto.FullName;
        client.PhoneNumber = dto.PhoneNumber;

        repository.Update(client);
        return NoContent();
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        logger.LogInformation("Удаление объекта(клиента)");
        var client = repository.GetById(id);
        if (client == null)
        {
            logger.LogWarning("Клиент с Id = {id} отсутствует", id);
            return NotFound(new { message = $"Клиент с Id = {id} не найден" });
        }

        repository.Delete(id);
        return NoContent();
    }
}
