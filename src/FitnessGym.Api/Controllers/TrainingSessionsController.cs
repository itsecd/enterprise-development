using System.ComponentModel.DataAnnotations;
using FitnessGym.Contracts.Dto;
using FitnessGym.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.Api.Controllers;

/// <summary>
/// CRUD-операции и запросы для сущностей TrainingSession.
/// </summary>
/// <param name="service">Сервис занятий.</param>
[ApiController]
[Route("api/sessions")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public class TrainingSessionsController(ITrainingSessionService service) : ControllerBase
{
    /// <summary>
    /// Возвращает все записи.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<TrainingSessionDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TrainingSessionDto>>> GetSessions() =>
        Ok(await service.GetSessionsAsync());

    /// <summary>
    /// Возвращает запись по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<TrainingSessionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TrainingSessionDto>> GetSession(int id)
    {
        var session = await service.GetSessionAsync(id);
        return session is null ? NotFound() : Ok(session);
    }

    /// <summary>
    /// Создаёт запись; идентификатор присваивается сервером.
    /// </summary>
    [HttpPost]
    [ProducesResponseType<TrainingSessionDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TrainingSessionDto>> AddSession(UpsertTrainingSessionDto session)
    {
        var created = await service.AddSessionAsync(session);
        return CreatedAtAction(nameof(GetSession), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновляет запись и возвращает обновлённые данные.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType<TrainingSessionDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TrainingSessionDto>> UpdateSession(int id, UpsertTrainingSessionDto session)
    {
        var updated = await service.UpdateSessionAsync(id, session);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// Удаляет запись по идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteSession(int id) =>
        await service.DeleteSessionAsync(id) ? NoContent() : NotFound();

    /// <summary>
    /// Возвращает занятия в выбранном зале за текущий месяц.
    /// </summary>
    [HttpGet("current-month")]
    [ProducesResponseType<IReadOnlyList<TrainingSessionDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<TrainingSessionDto>>> GetSessionsForCurrentMonth(
        [FromQuery, Required] string hallName) =>
        Ok(await service.GetSessionsForCurrentMonthAsync(hallName));
}
