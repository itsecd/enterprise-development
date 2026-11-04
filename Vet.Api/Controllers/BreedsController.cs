using Microsoft.AspNetCore.Mvc;
using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;

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
    [ProducesResponseType(typeof(IReadOnlyList<BreedDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение всех пород");
            var breeds = await service.GetAllAsync();
            return Ok(breeds);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении всех пород");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Получить породу по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор породы</param>
    /// <returns>Данные породы</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос породы с Id {Id}", id);
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении породы с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Создать новую породу
    /// </summary>
    /// <param name="dto">Данные для создания породы</param>
    /// <returns>Созданная порода</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BreedDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] BreedCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Создание новой породы {Name}", dto.Name);
            var created = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Ошибка валидации при создании породы");
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректный запрос"
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при создании породы");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
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
        try
        {
            logger.LogInformation("Обновление породы с Id {Id}", id);
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
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Ошибка валидации при обновлении породы с Id {Id}", id);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректный запрос"
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при обновлении породы с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Удалить породу
    /// </summary>
    /// <param name="id">Идентификатор породы</param>
    /// <returns>Пустой ответ при успешном удалении</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            logger.LogInformation("Удаление породы с Id {Id}", id);
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при удалении породы с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }
}