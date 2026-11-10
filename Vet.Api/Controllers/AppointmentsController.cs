using Microsoft.AspNetCore.Mvc;
using Vet.Contracts.Dtos;
using Vet.Contracts.Interfaces;

namespace Vet.Api.Controllers;

/// <summary>
/// Контроллер управления записями на прием
/// </summary>
/// <param name="service">Сервис записей на прием</param>
/// <param name="logger">Логгер контроллера</param>
[ApiController]
[Route("api/[controller]")]
public sealed class AppointmentsController(
    IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int> service,
    ILogger<AppointmentsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех записей на прием
    /// </summary>
    /// <returns>Список всех записей</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAll()
    {
        var appointments = await service.GetAllAsync();
        return Ok(appointments);
    }

    /// <summary>
    /// Получить запись на прием по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <returns>Данные записи</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AppointmentDto>> GetById(int id)
    {
        var appointment = await service.GetByIdAsync(id);
        if (appointment is null)
        {
            logger.LogWarning("Запись с Id {Id} не найдена", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Запись на прием с идентификатором {id} не найдена"
            });
        }

        return Ok(appointment);
    }

    /// <summary>
    /// Создать новую запись на прием
    /// </summary>
    /// <param name="dto">Данные для создания записи</param>
    /// <returns>Созданная запись</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AppointmentDto>> Create([FromBody] AppointmentCreateUpdateDto dto)
    {
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновить существующую запись на прием
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="dto">Обновленные данные записи</param>
    /// <returns>Пустой ответ при успешном обновлении</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] AppointmentCreateUpdateDto dto)
    {
        var updated = await service.UpdateAsync(id, dto);
        if (!updated)
        {
            logger.LogWarning("Запись с Id {Id} не найдена для обновления", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Запись с идентификатором {id} не найдена"
            });
        }

        return NoContent();
    }

    /// <summary>
    /// Удалить запись на прием
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <returns>Пустой ответ при успешном удалении</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await service.DeleteAsync(id);
        if (!deleted)
        {
            logger.LogWarning("Запись с Id {Id} не найдена для удаления", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Запись с идентификатором {id} не найдена"
            });
        }

        return NoContent();
    }
}