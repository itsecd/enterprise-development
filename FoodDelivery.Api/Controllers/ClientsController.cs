using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Application.Services;
using FoodDelivery.Contracts.Clients;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Контроллер для управления клиентами службы доставки.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService clientService, ILogger<ClientsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех клиентов.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<ClientDto>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос списка всех клиентов");
            return Ok(clientService.GetAll());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка клиентов");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Получить клиента по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ClientDto> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос клиента с Id = {Id}", id);
            var client = clientService.GetById(id);
            if (client is null)
            {
                logger.LogWarning("Клиент с Id = {Id} не найден", id);
                return NotFound(new { Message = $"Клиент с Id = {id} не найден." });
            }
            return Ok(client);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении клиента с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Зарегистрировать нового клиента.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<ClientDto> Create([FromBody] ClientCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Создание нового клиента: {FullName}", dto.FullName);
            var created = clientService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании клиента");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Обновить данные клиента.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(int id, [FromBody] ClientCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Обновление клиента с Id = {Id}", id);
            if (!clientService.Update(id, dto))
            {
                logger.LogWarning("Клиент с Id = {Id} не найден для обновления", id);
                return NotFound(new { Message = $"Клиент с Id = {id} не найден." });
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении клиента с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Удалить клиента по идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            logger.LogInformation("Удаление клиента с Id = {Id}", id);
            clientService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении клиента с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }
}