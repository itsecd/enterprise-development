using FitnessGym.Contracts.Dtos;
using FitnessGym.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.Api.Controllers;

/// <summary>
/// Работает с записями клиентов на занятия
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TrainingSessionsController(ITrainingSessionService service, ILogger<TrainingSessionsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает все занятия
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TrainingSessionDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<TrainingSessionDto>>> GetAll()
    {
        try
        {
            var sessions = await service.GetAllAsync();
            return Ok(sessions);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(GetAll));
        }
    }

    /// <summary>
    /// Возвращает занятие по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор занятия</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TrainingSessionDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TrainingSessionDto>> GetById(int id)
    {
        try
        {
            var session = await service.GetByIdAsync(id);
            if (session is null)
            {
                logger.LogWarning("Занятие {SessionId} не найдено", id);
                return NotFound();
            }
            return Ok(session);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(GetById));
        }
    }

    /// <summary>
    /// Создаёт новую запись на занятие
    /// </summary>
    /// <param name="request">Данные для создания</param>
    [HttpPost]
    [ProducesResponseType(typeof(TrainingSessionDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TrainingSessionDto>> Create([FromBody] TrainingSessionEditDto request)
    {
        try
        {
            var session = await service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = session.Id }, session);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(Create));
        }
    }

    /// <summary>
    /// Удаляет запись о занятии
    /// </summary>
    /// <param name="id">Идентификатор занятия</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted = await service.DeleteAsync(id);
            if (!deleted)
            {
                logger.LogWarning("Занятие {SessionId} для удаления не найдено", id);
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(Delete));
        }
    }

    private ObjectResult ServerError(Exception exception, string operation)
    {
        logger.LogError(exception, "Ошибка при выполнении операции {Operation}", operation);
        return Problem(
            statusCode: StatusCodes.Status500InternalServerError,
            title: "Не удалось обработать запрос.");
    }
}