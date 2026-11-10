using Microsoft.AspNetCore.Mvc;
using Vet.Contracts.Dtos;
using Vet.Contracts.Interfaces;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> GetAll()
    {
        var pets = await service.GetAllAsync();
        return Ok(pets);
    }

    /// <summary>
    /// Получить питомца по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор питомца</param>
    /// <returns>Данные питомца</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PetDto>> GetById(int id)
    {
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

    /// <summary>
    /// Создать нового питомца
    /// </summary>
    /// <param name="dto">Данные для создания питомца</param>
    /// <returns>Созданный питомец</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<PetDto>> Create([FromBody] PetCreateUpdateDto dto)
    {
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
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

    /// <summary>
    /// Получить все приемы конкретного питомца
    /// </summary>
    /// <param name="id">Идентификатор питомца</param>
    /// <param name="appointmentService">Сервис записей на прием</param>
    /// <returns>История приемов питомца</returns>
    [HttpGet("{id:int}/appointments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAppointmentsByPet(
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