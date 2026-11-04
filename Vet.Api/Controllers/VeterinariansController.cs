using Microsoft.AspNetCore.Mvc;
using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;

namespace Vet.Api.Controllers;

/// <summary>
/// Контроллер управления ветеринарами
/// </summary>
/// <param name="service">Сервис ветеринаров</param>
/// <param name="logger">Логгер контроллера</param>
[ApiController]
[Route("api/[controller]")]
public sealed class VeterinariansController(
    IApplicationService<VeterinarianDto, VeterinarianCreateUpdateDto, int> service,
    ILogger<VeterinariansController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех ветеринаров
    /// </summary>
    /// <returns>Список всех ветеринаров</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение всех ветеринаров");
            var vets = await service.GetAllAsync();
            return Ok(vets);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении всех ветеринаров");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Получить ветеринара по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор ветеринара</param>
    /// <returns>Данные ветеринара</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос ветеринара с Id {Id}", id);
            var vet = await service.GetByIdAsync(id);
            if (vet is null)
            {
                logger.LogWarning("Ветеринар с Id {Id} не найден", id);
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Ресурс не найден",
                    Detail = $"Ветеринар с идентификатором {id} не найден"
                });
            }

            return Ok(vet);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении ветеринара с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Создать нового ветеринара
    /// </summary>
    /// <param name="dto">Данные для создания ветеринара</param>
    /// <returns>Созданный ветеринар</returns>
    [HttpPost]
    [ProducesResponseType(typeof(VeterinarianDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Create([FromBody] VeterinarianCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Создание нового ветеринара {FullName}", dto.FullName);
            var created = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Специализация не найдена при создании ветеринара");
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден"
            });
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Ошибка валидации при создании ветеринара");
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректный запрос"
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при создании ветеринара");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Обновить существующего ветеринара
    /// </summary>
    /// <param name="id">Идентификатор ветеринара</param>
    /// <param name="dto">Обновленные данные ветеринара</param>
    /// <returns>Пустой ответ при успешном обновлении</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] VeterinarianCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Обновление ветеринара с Id {Id}", id);
            var updated = await service.UpdateAsync(id, dto);
            if (!updated)
            {
                logger.LogWarning("Ветеринар с Id {Id} не найден для обновления", id);
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Ресурс не найден",
                    Detail = $"Ветеринар с идентификатором {id} не найден"
                });
            }

            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Специализация не найдена при обновлении ветеринара с Id {Id}", id);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = ex.Message
            });
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Ошибка валидации при обновлении ветеринара с Id {Id}", id);
            return BadRequest(new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректный запрос",
                Detail = ex.Message
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при обновлении ветеринара с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера",
                Detail = ex.Message
            });
        }
    }

    /// <summary>
    /// Удалить ветеринара
    /// </summary>
    /// <param name="id">Идентификатор ветеринара</param>
    /// <returns>Пустой ответ при успешном удалении</returns>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            logger.LogInformation("Удаление ветеринара с Id {Id}", id);
            var deleted = await service.DeleteAsync(id);
            if (!deleted)
            {
                logger.LogWarning("Ветеринар с Id {Id} не найден для удаления", id);
                return NotFound(new ProblemDetails
                {
                    Status = StatusCodes.Status404NotFound,
                    Title = "Ресурс не найден",
                    Detail = $"Ветеринар с идентификатором {id} не найден"
                });
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка сервера при удалении ветеринара с Id {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера",
                Detail = ex.Message
            });
        }
    }

    /// <summary>
    /// Получить все приемы конкретного ветеринара
    /// </summary>
    /// <param name="id">Идентификатор ветеринара</param>
    /// <param name="appointmentService">Сервис записей на прием</param>
    /// <returns>Список записей врача</returns>
    [HttpGet("{id:int}/appointments")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAppointmentsByVet(
        int id,
        [FromServices] IApplicationService<AppointmentDto, AppointmentCreateUpdateDto, int> appointmentService)
    {
        var vet = await service.GetByIdAsync(id);
        if (vet is null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден",
                Detail = $"Ветеринар с идентификатором {id} не найден"
            });
        }

        var allAppointments = await appointmentService.GetAllAsync();
        var vetAppointments = allAppointments.Where(a => a.Veterinarian.Id == id).ToList();
        return Ok(vetAppointments);
    }
}