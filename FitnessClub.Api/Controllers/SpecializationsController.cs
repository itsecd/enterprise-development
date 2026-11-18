using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// CRUD-операции над справочником специализаций
/// </summary>
/// <param name="service">Сервис специализаций</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/specializations")]
public class SpecializationsController(ISpecializationService service, ILogger<SpecializationsController> logger) : ControllerBase
{
    /// <summary>
    /// Создаёт новую специализацию
    /// </summary>
    /// <param name="editDto">Данные специализации</param>
    /// <returns>Созданная специализация</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> Create(SpecializationEditDto editDto)
    {
        var created = await service.Create(editDto);
        logger.LogInformation("Создана специализация с id={Id}", created.Id);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Возвращает список всех специализаций
    /// </summary>
    /// <returns>Коллекция специализаций</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<SpecializationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<SpecializationDto>>> GetAll() =>
        Ok(await service.ReadAll());

    /// <summary>
    /// Возвращает специализацию по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор специализации</param>
    /// <returns>Специализация</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> GetById(int id) =>
        Ok(await service.Read(id));

    /// <summary>
    /// Обновляет специализацию
    /// </summary>
    /// <param name="id">Идентификатор специализации</param>
    /// <param name="editDto">Новые данные специализации</param>
    /// <returns>Обновлённая специализация</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<SpecializationDto>> Update(int id, SpecializationEditDto editDto)
    {
        var updated = await service.Update(id, editDto);
        logger.LogInformation("Обновлена специализация с id={Id}", updated.Id);
        return Ok(updated);
    }

    /// <summary>
    /// Удаляет специализацию
    /// </summary>
    /// <param name="id">Идентификатор специализации</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.Delete(id);
        logger.LogInformation("Удалена специализация с id={Id}", id);
        return NoContent();
    }
}