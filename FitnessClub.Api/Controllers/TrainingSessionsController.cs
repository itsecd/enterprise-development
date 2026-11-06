
using FitnessClub.Api.Dtos.TrainingSessions;
using FitnessClub.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для работы с записями на занятия.
/// </summary>
[ApiController]
[Route("api/training-sessions")]
public class TrainingSessionsController(TrainingSessionService service) : ControllerBase
{
    /// <summary>
    /// Получить все записи на занятия.
    /// </summary>
    [HttpGet]
    public ActionResult<IReadOnlyList<TrainingSessionDto>> GetAll()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получить занятия за текущий месяц в выбранном зале.
    /// </summary>
    [HttpGet("current-month")]
    public ActionResult<IReadOnlyList<TrainingSessionDto>> GetCurrentMonth([FromQuery] int gymId)
    {
        if (gymId <= 0)
        {
            return BadRequest("Некорректный ID зала.");
        }

        var sessions = service.GetCurrentMonthByGym(gymId);

        if (sessions is null)
        {
            return NotFound("Зал с указанным ID не найден.");
        }

        return Ok(sessions);
    }

    /// <summary>
    /// Получить запись по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<TrainingSessionDto> GetById(int id)
    {
        var session = service.GetById(id);

        if (session is null)
        {
            return NotFound();
        }

        return Ok(session);
    }

    /// <summary>
    /// Создать запись на занятие.
    /// </summary>
    [HttpPost]
    public ActionResult<TrainingSessionDto> Create(
        CreateTrainingSessionDto dto)
    {
        try
        {
            var session = service.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = session.Id },
                session);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Изменить запись на занятие.
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, CreateTrainingSessionDto dto)
    {
        try
        {
            if (!service.Update(id, dto))
            {
                return NotFound();
            }

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Удалить запись на занятие.
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        if (!service.Delete(id))
        {
            return NotFound();
        }

        return NoContent();
    }
}
