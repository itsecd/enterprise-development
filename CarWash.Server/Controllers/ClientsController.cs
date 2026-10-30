using CarWash.Domain.Entities;
using CarWash.Server.Data;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientsController(InMemoryDataStore dataStore) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Client>> GetAll() => Ok(dataStore.Clients);

    [HttpGet("{id}")]
    public ActionResult<Client> GetById(int id)
    {
        var client = dataStore.Clients.FirstOrDefault(c => c.Id == id);
        return client is null ? NotFound() : Ok(client);
    }

    [HttpPost]
    public ActionResult<Client> Create([FromBody] Client client)
    {
        client.Id = dataStore.Clients.Any() ? dataStore.Clients.Max(c => c.Id) + 1 : 1;
        dataStore.Clients.Add(client);
        return CreatedAtAction(nameof(GetById), new { id = client.Id }, client);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Client updatedClient)
    {
        var client = dataStore.Clients.FirstOrDefault(c => c.Id == id);
        if (client is null) return NotFound();

        client.FullName = updatedClient.FullName;
        client.Phone = updatedClient.Phone;
        // Список машин пока не обновляем принудительно, чтобы не ломать связи заказов.

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var client = dataStore.Clients.FirstOrDefault(c => c.Id == id);
        if (client is null) return NotFound();

        dataStore.Clients.Remove(client);
        return NoContent();
    }
}