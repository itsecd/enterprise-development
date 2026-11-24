using BeautySalon.Contracts.Clients;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Server.Controllers;

/// <summary>
/// CRUD-операции над клиентами салона.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientRepository clientRepository, ILogger<ClientsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает всех клиентов.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ClientResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<ClientResponse>>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение списка всех клиентов.");
            var clients = await clientRepository.GetAllAsync();
            return Ok(clients.Select(MapToResponse));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка клиентов.");
            return Problem("Внутренняя ошибка сервера при получении клиентов.");
        }
    }

    /// <summary>
    /// Возвращает клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientResponse>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос на получение клиента с ID: {ClientId}", id);
            var client = await clientRepository.GetByIdAsync(id);
            if (client is null)
            {
                logger.LogWarning("Клиент с ID {ClientId} не найден.", id);
                return NotFound();
            }
            return Ok(MapToResponse(client));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении клиента с ID: {ClientId}", id);
            return Problem("Внутренняя ошибка сервера при получении клиента.");
        }
    }

    /// <summary>
    /// Создает нового клиента.
    /// </summary>
    /// <param name="request">Данные нового клиента.</param>
    [HttpPost]
    [ProducesResponseType(typeof(ClientResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientResponse>> Create([FromBody] ClientRequest request)
    {
        try
        {
            logger.LogInformation("Запрос на создание нового клиента: {FullName}", request.FullName);
            
            var client = new Client
            {
                FullName = request.FullName,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                Phone = request.Phone
            };

            await clientRepository.AddAsync(client);
            
            logger.LogInformation("Клиент успешно создан с ID: {ClientId}", client.Id);
            return CreatedAtAction(nameof(GetById), new { id = client.Id }, MapToResponse(client));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании клиента: {FullName}", request.FullName);
            return Problem("Внутренняя ошибка сервера при создании клиента.");
        }
    }

    /// <summary>
    /// Обновляет данные клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <param name="request">Новые данные клиента.</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] ClientRequest request)
    {
        try
        {
            logger.LogInformation("Запрос на обновление клиента с ID: {ClientId}", id);
            
            var client = await clientRepository.GetByIdAsync(id);
            if (client is null)
            {
                logger.LogWarning("Клиент с ID {ClientId} не найден для обновления.", id);
                return NotFound();
            }

            client.FullName = request.FullName;
            client.Gender = request.Gender;
            client.BirthDate = request.BirthDate;
            client.Phone = request.Phone;

            await clientRepository.UpdateAsync(client);
            
            logger.LogInformation("Клиент с ID {ClientId} успешно обновлен.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении клиента с ID: {ClientId}", id);
            return Problem("Внутренняя ошибка сервера при обновлении клиента.");
        }
    }

    /// <summary>
    /// Удаляет клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            logger.LogInformation("Запрос на удаление клиента с ID: {ClientId}", id);
            
            var client = await clientRepository.GetByIdAsync(id);
            if (client is not null)
            {
                await clientRepository.DeleteAsync(client);
                logger.LogInformation("Клиент с ID {ClientId} успешно удален.", id);
            }
            else
            {
                logger.LogWarning("Клиент с ID {ClientId} не найден (удаление пропущено).", id);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении клиента с ID: {ClientId}", id);
            return Problem("Внутренняя ошибка сервера при удалении клиента.");
        }
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