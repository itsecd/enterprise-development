using Microsoft.AspNetCore.Mvc;
using Vet.Contracts.Dtos;
using Vet.Contracts.Interfaces;

namespace Vet.Api.Controllers;

/// <summary>
/// Контроллер управления специализациями
/// </summary>
/// <param name="service">Сервис специализаций</param>
/// <param name="logger">Логгер контроллера</param>
[ApiController]
[Route("api/[controller]")]
public sealed class SpecializationsController(
    IApplicationService<SpecializationDto, SpecializationCreateUpdateDto, int> service,
    ILogger<SpecializationsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех специализаций
    /// </summary>
    /// <returns>Список всех специализаций</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<SpecializationDto>>> GetAll()
    {
        var specs = await service.GetAllAsync();
        return Ok(specs);
    }

    /// <summary>
    /// Получить специализацию по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор специализации</param>
    /// <returns>Данные специализации</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> GetById(int id)
    {
        var spec = await service.GetByIdAsync(id);
        if (spec is null)
        {
            logger.LogWarning("Специализация с Id {Id} не найдена", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Специализация с идентификатором {id} не найдена"
            });
        }

        return Ok(spec);
    }

    /// <summary>
    /// Создать новую специализацию
    /// </summary>
    /// <param name="dto">Данные для создания специализации</param>
    /// <returns>Созданная специализация</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> Create([FromBody] SpecializationCreateUpdateDto dto)
    {
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновить существующую специализацию
    /// </summary>
    /// <param name="id">Идентификатор специализации</param>
    /// <param name="dto">Обновленные данные специализации</param>
    /// <returns>Пустой ответ при успешном обновлении</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] SpecializationCreateUpdateDto dto)
    {
        var updated = await service.UpdateAsync(id, dto);
        if (!updated)
        {
            logger.LogWarning("Специализация с Id {Id} не найдена для обновления", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Специализация с идентификатором {id} не найдена"
            });
        }

        return NoContent();
    }

    /// <summary>
    /// Удалить специализацию
    /// </summary>
    /// <param name="id">Идентификатор специализации</param>
    /// <returns>Пустой ответ при успешном удалении</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await service.DeleteAsync(id);
        if (!deleted)
        {
            logger.LogWarning("Специализация с Id {Id} не найдена для удаления", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Специализация с идентификатором {id} не найдена"
            });
        }

        return NoContent();
    }
}