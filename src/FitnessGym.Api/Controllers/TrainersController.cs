using System.ComponentModel.DataAnnotations;
using FitnessGym.Contracts.Dto;
using FitnessGym.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.Api.Controllers;

/// <summary>
/// CRUD-операции и запросы для сущностей Trainer.
/// </summary>
/// <param name="service">Сервис тренеров.</param>
[ApiController]
[Route("api/trainers")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public class TrainersController(ITrainerService service) : ControllerBase
{
    /// <summary>
    /// Возвращает все записи.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<TrainerDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetTrainers() =>
        Ok(await service.GetTrainersAsync());

    /// <summary>
    /// Возвращает запись по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<TrainerDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TrainerDto>> GetTrainer(int id)
    {
        var trainer = await service.GetTrainerAsync(id);
        return trainer is null ? NotFound() : Ok(trainer);
    }

    /// <summary>
    /// Создаёт запись; идентификатор присваивается сервером.
    /// </summary>
    [HttpPost]
    [ProducesResponseType<TrainerDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TrainerDto>> AddTrainer(UpsertTrainerDto trainer)
    {
        var created = await service.AddTrainerAsync(trainer);
        return CreatedAtAction(nameof(GetTrainer), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновляет запись и возвращает обновлённые данные.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType<TrainerDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TrainerDto>> UpdateTrainer(int id, UpsertTrainerDto trainer)
    {
        var updated = await service.UpdateTrainerAsync(id, trainer);
        return updated is null ? NotFound() : Ok(updated);
    }

    /// <summary>
    /// Удаляет запись по идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status409Conflict)]
    public async Task<ActionResult> DeleteTrainer(int id) =>
        await service.DeleteTrainerAsync(id) ? NoContent() : NotFound();

    /// <summary>
    /// Возвращает тренеров с указанным минимальным стажем работы.
    /// </summary>
    [HttpGet("experienced")]
    [ProducesResponseType<IReadOnlyList<TrainerDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetExperiencedTrainers(
        [FromQuery, Range(0, int.MaxValue)] int minYears = 5) =>
        Ok(await service.GetExperiencedTrainersAsync(minYears));

    /// <summary>
    /// Возвращает самых популярных тренеров по числу записанных занятий.
    /// </summary>
    [HttpGet("popular")]
    [ProducesResponseType<IReadOnlyList<TrainerDto>>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<TrainerDto>>> GetPopularTrainers(
        [FromQuery, Range(1, int.MaxValue)] int count = 5) =>
        Ok(await service.GetPopularTrainersAsync(count));
}
