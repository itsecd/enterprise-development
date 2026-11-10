using Microsoft.AspNetCore.Mvc;
using Vet.Contracts.Dtos;
using Vet.Contracts.Interfaces;

namespace Vet.Api.Controllers;

/// <summary>
/// Контроллер управления породами
/// </summary>
/// <param name="service">Сервис пород</param>
/// <param name="logger">Логгер контроллера</param>
[ApiController]
[Route("api/[controller]")]
public sealed class BreedsController(
    IApplicationService<BreedDto, BreedCreateUpdateDto, int> service,
    ILogger<BreedsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех пород
    /// </summary>
    /// <returns>Список всех пород</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<BreedDto>>> GetAll()
    {
        var breeds = await service.GetAllAsync();
        return Ok(breeds);
    }

    /// <summary>
    /// Получить породу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор породы</param>
    /// <returns>Данные породы</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BreedDto>> GetById(int id)
    {
        var breed = await service.GetByIdAsync(id);
        if (breed is null)
        {
            logger.LogWarning("Порода с Id {Id} не найдена", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Порода с идентификатором {id} не найдена"
            });
        }

        return Ok(breed);
    }

    /// <summary>
    /// Создать новую породу
    /// </summary>
    /// <param name="dto">Данные для создания породы</param>
    /// <returns>Созданная порода</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BreedDto>> Create([FromBody] BreedCreateUpdateDto dto)
    {
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновить существующую породу
    /// </summary>
    /// <param name="id">Идентификатор породы</param>
    /// <param name="dto">Обновленные данные породы</param>
    /// <returns>Пустой ответ при успешном обновлении</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] BreedCreateUpdateDto dto)
    {
        var updated = await service.UpdateAsync(id, dto);
        if (!updated)
        {
            logger.LogWarning("Порода с Id {Id} не найдена для обновления", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Порода с идентификатором {id} не найдена"
            });
        }

        return NoContent();
    }

    /// <summary>
    /// Удалить породу
    /// </summary>
    /// <param name="id">Идентификатор породы</param>
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
            logger.LogWarning("Порода с Id {Id} не найдена для удаления", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Порода с идентификатором {id} не найдена"
            });
        }

        return NoContent();
    }
}