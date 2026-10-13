using Microsoft.AspNetCore.Mvc;
using DryCleaning.Api.Services;
using DryCleaning.Domain.Models;

namespace DryCleaning.Api.Controllers;


/// <summary>
/// Клиентский контроллер
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController : ControllerBase
{
    private readonly InMemStore _store;

    public ClientsController(InMemStore store)
    {
        _store = store;
    }

    /// <summary>
    /// Получить список всех клиентов
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Client>> GetAll()
    {
        return Ok(_store.Clients);
    }

    /// <summary>
    /// Получить клиента по Id
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<Client> GetById(int id)
    {
        var client = _store.Clients.FirstOrDefault(c => c.Id == id);
        if (client == null)
        {
            return NotFound(new { message = $"Клиент с Id = {id} не найден" });
        }
        return Ok(client);
    }

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    [HttpPost]
    public ActionResult<Client> Create([FromBody] Client newClient)
    {
        var nextId = _store.Clients.Any() ? _store.Clients.Max(c => c.Id) + 1 : 1;
        newClient.Id = nextId;

        _store.Clients.Add(newClient);

        return CreatedAtAction(nameof(GetById), new { id = newClient.Id }, newClient);
    }

    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Client updatedClient)
    {
        var client = _store.Clients.FirstOrDefault(c => c.Id == id);
        if (client == null)
        {
            return NotFound(new { message = $"Клиент с Id = {id} не найден" });
        }

        client.FullName = updatedClient.FullName;
        client.PhoneNumber = updatedClient.PhoneNumber;

        return NoContent();
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var client = _store.Clients.FirstOrDefault(c => c.Id == id);
        if (client == null)
        {
            return NotFound(new { message = $"Клиент с Id = {id} не найден" });
        }

        _store.Clients.Remove(client);
        return NoContent();
    }
}
