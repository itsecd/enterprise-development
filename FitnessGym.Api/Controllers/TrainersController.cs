using FitnessGym.Contracts.Dtos;
using FitnessGym.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.Api.Controllers;

/// <summary>
/// Работает с тренерами фитнес-клуба
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TrainersController(ITrainerService service, ILogger<TrainersController> logger) : ControllerBase
{
    /// <summary>Возвращает всех тренеров</summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TrainerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetAll()
    {
        try
        {
            var trainers = await service.GetAllAsync();
            return Ok(trainers);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(GetAll));
        }
    }

    /// <summary>
    /// Возвращает тренера по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор тренера</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TrainerDto>> GetById(int id)
    {
        try
        {
            var trainer = await service.GetByIdAsync(id);
            if (trainer is null)
            {
                logger.LogWarning("Тренер {TrainerId} не найден", id);
                return NotFound();
            }
            return Ok(trainer);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(GetById));
        }
    }

    /// <summary>
    /// Создаёт нового тренера
    /// </summary>
    /// <param name="request">Данные для создания</param>
    [HttpPost]
    [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TrainerDto>> Create([FromBody] TrainerEditDto request)
    {
        try
        {
            var trainer = await service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = trainer.Id }, trainer);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(Create));
        }
    }

    /// <summary>
    /// Обновляет данные тренера
    /// </summary>
    /// <param name="id">Идентификатор тренера</param>
    /// <param name="request">Новые данные</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] TrainerEditDto request)
    {
        try
        {
            var updated = await service.UpdateAsync(id, request);
            if (!updated)
            {
                logger.LogWarning("Тренер {TrainerId} для обновления не найден", id);
                return NotFound();
            }
            return NoContent();
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(Update));
        }
    }

    /// <summary>
    /// Удаляет тренера
    /// </summary>
    /// <param name="id">Идентификатор тренера</param>
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
                logger.LogWarning("Тренер {TrainerId} для удаления не найден", id);
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