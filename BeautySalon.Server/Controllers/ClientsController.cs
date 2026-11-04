using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using BeautySalon.Contracts.Clients;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Server.Controllers;

/// <summary>
/// CRUD-операции над клиентами салона.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientRepository clientRepository) : ControllerBase
{
    /// <summary>
    /// Возвращает всех клиентов.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClientResponse>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ClientResponse>> GetAll()
    {
        return Ok(clientRepository.GetAll().Select(MapToResponse));
    }

    /// <summary>
    /// Возвращает клиента по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ClientResponse> GetById(int id)
    {
        var client = clientRepository.GetById(id);
        return client is null ? NotFound() : Ok(MapToResponse(client));
    }

    /// <summary>
    /// Создает нового клиента.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ClientResponse> Create([FromBody] ClientRequest request)
    {
        // Маппинг из Request DTO в доменную сущность
        var client = new Client
        {
            FullName = request.FullName,
            Gender = request.Gender,
            BirthDate = request.BirthDate,
            Phone = request.Phone
        };

        clientRepository.Add(client);
        
        return CreatedAtAction(nameof(GetById), new { id = client.Id }, MapToResponse(client));
    }

    /// <summary>
    /// Обновляет данные клиента по идентификатору.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] ClientRequest request)
    {
        var client = clientRepository.GetById(id);
        if (client is null) return NotFound();

        client.FullName = request.FullName;
        client.Gender = request.Gender;
        client.BirthDate = request.BirthDate;
        client.Phone = request.Phone;

        clientRepository.Update(client);
        return NoContent();
    }

    /// <summary>
    /// Удаляет клиента по идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        var client = clientRepository.GetById(id);
        if (client is not null)
        {
            clientRepository.Delete(client);
        }

        return NoContent();
    }

    // Метод для маппинга доменной сущности в DTO ответа
    private static ClientResponse MapToResponse(Client client) => new()
    {
        Id = client.Id,
        FullName = client.FullName,
        Gender = client.Gender,
        BirthDate = client.BirthDate,
        Phone = client.Phone
    };
}