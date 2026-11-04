using Microsoft.AspNetCore.Mvc;
using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;

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
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение всех записей на прием");
            var appointments = await service.GetAllAsync();
            return Ok(appointments);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении записей на прием");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера",
                Detail = ex.Message
            });
        }
    }

    /// <summary>
    /// Получить запись на прием по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <returns>Данные записи</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос записи с Id {Id}", id);
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении записи с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Создать новую запись на прием
    /// </summary>
    /// <param name="dto">Данные для создания записи</param>
    /// <returns>Созданная запись</returns>
    [HttpPost]
    [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] AppointmentCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Создание новой записи в кабинет {Room}", dto.RoomNumber);
            var created = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Питомец или ветеринар не найден при создании записи");
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден"
            });
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Ошибка валидации при создании записи на прием");
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректный запрос"
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при создании записи на прием");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
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
        try
        {
            logger.LogInformation("Обновление записи с Id {Id}", id);
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
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Связанная сущность не найдена при обновлении записи с Id {Id}", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден"
            });
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Ошибка валидации при обновлении записи с Id {Id}", id);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректный запрос"
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при обновлении записи с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
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
        try
        {
            logger.LogInformation("Удаление записи с Id {Id}", id);
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при удалении записи с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера",
                Detail = ex.Message
            });
        }
    }
}