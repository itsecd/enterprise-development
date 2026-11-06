
using FitnessClub.Api.Dtos.Trainers;
using FitnessClub.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Контроллер для управления тренерами фитнес-клуба.
/// </summary>
[ApiController]
[Route("api/trainers")]
public class TrainersController(TrainerService service, ILogger<TrainersController> logger) : ControllerBase
{
    /// <summary>
    /// Получить всех тренеров.
    /// </summary>
    [HttpGet]
    public ActionResult<IReadOnlyList<TrainerDto>> GetAll()
    {
        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получить тренеров со стажем не менее 5 лет.
    /// </summary>
    [HttpGet("experienced")]
    public ActionResult<IReadOnlyList<TrainerDto>> GetExperienced()
    {
        return Ok(service.GetExperienced());
    }

    /// <summary>
    /// Получить пять самых популярных тренеров.
    /// </summary>
    [HttpGet("top-5")]
    public ActionResult<IReadOnlyList<TrainerDto>> GetTopFive()
    {
        return Ok(service.GetTopFive());
    }

    /// <summary>
    /// Получить тренера по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<TrainerDto> GetById(int id)
    {
        var trainer = service.GetById(id);

        if (trainer is null)
        {
            return NotFound();
        }

        return Ok(trainer);
    }

    /// <summary>
    /// Создать нового тренера.
    /// </summary>
    [HttpPost]
    public ActionResult<TrainerDto> Create(CreateTrainerDto dto)
    {
        try
        {
            var trainer = service.Create(dto);

            logger.LogInformation(
                "Создан тренер с ID {Id}", trainer.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = trainer.Id },
                trainer);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    /// <summary>
    /// Обновить информацию о тренере.
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, CreateTrainerDto dto)
    {
        try
        {
            if (!service.Update(id, dto))
            {
                return NotFound();
            }

            logger.LogInformation(
                "Обновлён тренер с ID {Id}", id);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    /// <summary>
    /// Удалить тренера.
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        try
        {
            if (!service.Delete(id))
            {
                return NotFound();
            }

            logger.LogInformation(
                "Удалён тренер с ID {Id}", id);

            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }
}
