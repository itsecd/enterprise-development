using Hotel.Application.Contracts.Client;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebAPI.Controllers;

/// <summary>
/// Контроллер для CRUD операций над клиентами
/// </summary>
/// <param name="clientService">Интерфейс службы</param>
[ApiController]
[Route("api/[controller]")]
public class ClientsController(IClientService clientService) : ControllerBase
{
    /// <summary>
    /// Получение всех клиентов в таблице
    /// </summary>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Коллекция клиентов</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    public async Task<IActionResult> GetAll(CancellationToken ct)
        => Ok(await clientService.GetAllAsync(ct));
    
    /// <summary>
    /// Получение информации о клиенте
    /// </summary>
    /// <param name="id">Id клиента</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Клиент</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetById(int id, CancellationToken ct)
    {
        var result = await clientService.GetByIdAsync(id, ct);
        return result is null ? NotFound() : Ok(result);
    }
    
    /// <summary>
    /// Создание клиента
    /// </summary>
    /// <param name="dto">DTO для создания клиента</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Созданный клиент</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<IActionResult> Create(ClientCreateUpdateDto dto, CancellationToken ct)
    {
        var created = await clientService.CreateAsync(dto, ct);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }
    
    /// <summary>
    /// Обновление информации о клиенте
    /// </summary>
    /// <param name="id">Id клиента</param>
    /// <param name="dto">DTO для обновления клиента</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Обновлённый клиент</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Update(int id, ClientCreateUpdateDto dto, CancellationToken ct)
    {
        var updated = await clientService.UpdateAsync(id, dto, ct);
        return updated is null ? NotFound() : Ok(updated);
    }
    
    /// <summary>
    /// Удаление клиента
    /// </summary>
    /// <param name="id">Id клиента</param>
    /// <param name="ct">Токен для отмены операции</param>
    /// <returns>Стутс-код</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(int id, CancellationToken ct)
    {
        var deleted = await clientService.DeleteAsync(id, ct);
        return deleted ? NoContent() : NotFound();
    }
}