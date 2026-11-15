using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers;

/// <summary>
/// Управление записями на приём
/// </summary>
[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class AppointmentsController(IAppointmentService service) : ControllerBase
{
    /// <summary>
    /// Получить список всех записей на приём
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAll(CancellationToken cancellationToken) =>
        Ok(await service.GetAllAsync(cancellationToken));

    /// <summary>
    /// Получить запись на приём по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <response code="200">Найдено</response>
    /// <response code="404">Не найдено</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppointmentDto>> GetById(int id, CancellationToken cancellationToken) =>
        Ok(await service.GetByIdAsync(id, cancellationToken));

    /// <summary>
    /// Создать запись на приём
    /// </summary>
    /// <param name="dto">Данные для создания</param>
    /// <response code="201">Создано</response>
    /// <response code="400">Некорректные данные</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AppointmentDto>> Create([FromBody] AppointmentEditDto dto, CancellationToken cancellationToken)
    {
        var created = await service.CreateAsync(dto, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Изменить запись на приём
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="dto">Новые данные</param>
    /// <response code="200">Изменено, возвращён обновлённый объект</response>
    /// <response code="400">Некорректные данные</response>
    /// <response code="404">Не найдено</response>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppointmentDto>> Update(
        int id, [FromBody] AppointmentEditDto dto, CancellationToken cancellationToken) =>
        Ok(await service.UpdateAsync(id, dto, cancellationToken));

    /// <summary>
    /// Удалить запись на приём
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <response code="204">Удалено</response>
    /// <response code="404">Не найдено</response>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id, cancellationToken);
        return NoContent();
    }
}