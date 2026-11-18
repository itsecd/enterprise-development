using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// CRUD-операции над тренерами
/// </summary>
/// <param name="service">Сервис тренеров</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/trainers")]
public class TrainersController(ITrainerService service, ILogger<TrainersController> logger) : ControllerBase
{
    /// <summary>
    /// Создаёт нового тренера
    /// </summary>
    /// <param name="editDto">Данные тренера</param>
    /// <returns>Созданный тренер</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TrainerDto>> Create(TrainerEditDto editDto)
    {
        var created = await service.Create(editDto);
        logger.LogInformation("Создан тренер с id={Id}", created.Id);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Возвращает список всех тренеров
    /// </summary>
    /// <returns>Коллекция тренеров</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TrainerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetAll() =>
        Ok(await service.ReadAll());

    /// <summary>
    /// Возвращает тренера по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор тренера</param>
    /// <returns>Тренер</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TrainerDto>> GetById(int id) =>
        Ok(await service.Read(id));

    /// <summary>
    /// Обновляет данные тренера
    /// </summary>
    /// <param name="id">Идентификатор тренера</param>
    /// <param name="editDto">Новые данные тренера</param>
    /// <returns>Обновлённый тренер</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TrainerDto>> Update(int id, TrainerEditDto editDto)
    {
        var updated = await service.Update(id, editDto);
        logger.LogInformation("Обновлён тренер с id={Id}", updated.Id);
        return Ok(updated);
    }

    /// <summary>
    /// Удаляет тренера
    /// </summary>
    /// <param name="id">Идентификатор тренера</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.Delete(id);
        logger.LogInformation("Удалён тренер с id={Id}", id);
        return NoContent();
    }
}