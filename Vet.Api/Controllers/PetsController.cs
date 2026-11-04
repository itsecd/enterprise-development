using Microsoft.AspNetCore.Mvc;
using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;

namespace Vet.Api.Controllers;

/// <summary>
/// Контроллер управления питомцами
/// </summary>
/// <param name="service">Сервис питомцев</param>
/// <param name="logger">Логгер контроллера</param>
[ApiController]
[Route("api/[controller]")]
public sealed class PetsController(
    IApplicationService<PetDto, PetCreateUpdateDto, int> service,
    ILogger<PetsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех питомцев
    /// </summary>
    /// <returns>Список всех питомцев</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение всех питомцев");
            var pets = await service.GetAllAsync();
            return Ok(pets);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении всех питомцев");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Получить питомца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор питомца</param>
    /// <returns>Данные питомца</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос питомца с Id {Id}", id);
            var pet = await service.GetByIdAsync(id);
            if (pet is null)
            {
                logger.LogWarning("Питомец с Id {Id} не найден", id);
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Ресурс не найден",
                    Detail = $"Питомец с идентификатором {id} не найден"
                });
            }

            return Ok(pet);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении питомца с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Создать нового питомца
    /// </summary>
    /// <param name="dto">Данные для создания питомца</param>
    /// <returns>Созданный питомец</returns>
    [HttpPost]
    [ProducesResponseType(typeof(PetDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] PetCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Создание нового питомца {Name}", dto.Name);
            var created = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Связанная сущность не найдена при создании питомца");
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден"
            });
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Ошибка валидации при создании питомца");
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректный запрос"
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при создании питомца");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Обновить существующего питомца
    /// </summary>
    /// <param name="id">Идентификатор питомца</param>
    /// <param name="dto">Обновленные данные питомца</param>
    /// <returns>Пустой ответ при успешном обновлении</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] PetCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Обновление питомца с Id {Id}", id);
            var updated = await service.UpdateAsync(id, dto);
            if (!updated)
            {
                logger.LogWarning("Питомец с Id {Id} не найден для обновления", id);
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Ресурс не найден",
                    Detail = $"Питомец с идентификатором {id} не найден"
                });
            }

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Связанная сущность не найдена при обновлении питомца с Id {Id}", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден"
            });
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Ошибка валидации при обновлении питомца с Id {Id}", id);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректный запрос"
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при обновлении питомца с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Удалить питомца
    /// </summary>
    /// <param name="id">Идентификатор питомца</param>
    /// <returns>Пустой ответ при успешном удалении</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            logger.LogInformation("Удаление питомца с Id {Id}", id);
            var deleted = await service.DeleteAsync(id);
            if (!deleted)
            {
                logger.LogWarning("Питомец с Id {Id} не найден для удаления", id);
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Ресурс не найден",
                    Detail = $"Питомец с идентификатором {id} не найден"
                });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при удалении питомца с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Получить все приемы конкретного питомца
    /// </summary>
    /// <param name="id">Идентификатор питомца</param>
    /// <param name="appointmentService">Сервис записей на прием</param>
    /// <returns>История приемов питомца</returns>
    [HttpGet("{id:int}/appointments")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAppointmentsByPet(
        int id,
        [FromServices] IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int> appointmentService)
    {
        var pet = await service.GetByIdAsync(id);
        if (pet is null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Питомец с идентификатором {id} не найден"
            });
        }

        var allAppointments = await appointmentService.GetAllAsync();
        var petAppointments = allAppointments.Where(a => a.Pet.Id == id).ToList();
        return Ok(petAppointments);
    }
}