using Microsoft.AspNetCore.Mvc;
using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;

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
    [ProducesResponseType(typeof(IReadOnlyList<SpecializationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение всех специализаций");
            var specs = await service.GetAllAsync();
            return Ok(specs);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении всех специализаций");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Получить специализацию по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор специализации</param>
    /// <returns>Данные специализации</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос специализации с Id {Id}", id);
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении специализации с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Создать новую специализацию
    /// </summary>
    /// <param name="dto">Данные для создания специализации</param>
    /// <returns>Созданная специализация</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] SpecializationCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Создание новой специализации {Name}", dto.Name);
            var created = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Ошибка валидации при создании специализации");
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректный запрос"
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при создании специализации");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
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
        try
        {
            logger.LogInformation("Обновление специализации с Id {Id}", id);
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
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Ошибка валидации при обновлении специализации с Id {Id}", id);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректный запрос"
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при обновлении специализации с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Удалить специализацию
    /// </summary>
    /// <param name="id">Идентификатор специализации</param>
    /// <returns>Пустой ответ при успешном удалении</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            logger.LogInformation("Удаление специализации с Id {Id}", id);
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при удалении специализации с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }
}