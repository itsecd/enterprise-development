using Microsoft.AspNetCore.Mvc;
using Vet.Contracts.Dtos;
using Vet.Contracts.Interfaces;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<VeterinarianDto>>> GetAll()
    {
        var vets = await service.GetAllAsync();
        return Ok(vets);
    }

    /// <summary>
    /// Получить ветеринара по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор ветеринара</param>
    /// <returns>Данные ветеринара</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VeterinarianDto>> GetById(int id)
    {
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

    /// <summary>
    /// Создать нового ветеринара
    /// </summary>
    /// <param name="dto">Данные для создания ветеринара</param>
    /// <returns>Созданный ветеринар</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<VeterinarianDto>> Create([FromBody] VeterinarianCreateUpdateDto dto)
    {
        var created = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
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

    /// <summary>
    /// Получить все приемы конкретного ветеринара
    /// </summary>
    /// <param name="id">Идентификатор ветеринара</param>
    /// <param name="appointmentService">Сервис записей на прием</param>
    /// <returns>Список записей врача</returns>
    [HttpGet("{id:int}/appointments")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAppointmentsByVet(
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