using FitnessGym.Contracts.Dtos;
using FitnessGym.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace FitnessGym.Api.Controllers;

/// <summary>
/// Работает со справочником специализаций
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SpecializationsController(ISpecializationService service, ILogger<SpecializationsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает все специализации
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<SpecializationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<SpecializationDto>>> GetAll()
    {
        try
        {
            var specs = await service.GetAllAsync();
            return Ok(specs);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(GetAll));
        }
    }

    /// <summary>
    /// Возвращает специализацию по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор специализации</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> GetById(int id)
    {
        try
        {
            var spec = await service.GetByIdAsync(id);
            if (spec is null)
            {
                logger.LogWarning("Специализация {SpecializationId} не найдена", id);
                return NotFound();
            }
            return Ok(spec);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(GetById));
        }
    }

    /// <summary>
    /// Создаёт новую специализацию
    /// </summary>
    /// <param name="request">Название специализации</param>
    [HttpPost]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> Create([FromBody] SpecializationEditDto request)
    {
        try
        {
            var spec = await service.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = spec.Id }, spec);
        }
        catch (Exception exception)
        {
            return ServerError(exception, nameof(Create));
        }
    }

    /// <summary>
    /// Удаляет специализацию
    /// </summary>
    /// <param name="id">Идентификатор специализации</param>
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
                logger.LogWarning("Специализация {SpecializationId} для удаления не найдена", id);
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