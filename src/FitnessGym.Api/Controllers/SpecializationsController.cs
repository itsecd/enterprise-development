using FitnessGym.Contracts.Dto;
using FitnessGym.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessGym.Api.Controllers;

/// <summary>
/// CRUD-операции и запросы для сущностей Specialization.
/// </summary>
/// <param name="service">Сервис специализаций.</param>
[ApiController]
[Route("api/specializations")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public class SpecializationsController(ISpecializationService service) : ControllerBase
{
    /// <summary>
    /// Возвращает все записи.
    /// </summary>
    [HttpGet]
    [ProducesResponseType<IReadOnlyList<SpecializationDto>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<SpecializationDto>>> GetSpecializations() =>
        Ok(await service.GetSpecializationsAsync());

    /// <summary>
    /// Возвращает запись по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType<SpecializationDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SpecializationDto>> GetSpecialization(int id)
    {
        var specialization = await service.GetSpecializationAsync(id);
        return specialization is null ? NotFound() : Ok(specialization);
    }

    /// <summary>
    /// Создаёт запись; идентификатор присваивается сервером.
    /// </summary>
    [HttpPost]
    [ProducesResponseType<SpecializationDto>(StatusCodes.Status201Created)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<SpecializationDto>> AddSpecialization(UpsertSpecializationDto specialization)
    {
        var created = await service.AddSpecializationAsync(specialization);
        return CreatedAtAction(nameof(GetSpecialization), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновляет запись и возвращает обновлённые данные.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType<SpecializationDto>(StatusCodes.Status200OK)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SpecializationDto>> UpdateSpecialization(int id, UpsertSpecializationDto specialization)
    {
        var updated = await service.UpdateSpecializationAsync(id, specialization);
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
    public async Task<ActionResult> DeleteSpecialization(int id) =>
        await service.DeleteSpecializationAsync(id) ? NoContent() : NotFound();
}
