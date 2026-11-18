using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// CRUD-операции над занятиями (записями клиентов на занятия)
/// </summary>
/// <param name="service">Сервис занятий</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/lessons")]
public class LessonsController(ILessonService service, ILogger<LessonsController> logger) : ControllerBase
{
    /// <summary>
    /// Записывает клиента на занятие
    /// </summary>
    /// <param name="editDto">Данные занятия</param>
    /// <returns>Созданное занятие</returns>
    [HttpPost]
    [ProducesResponseType(typeof(LessonDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<LessonDto>> Create(LessonEditDto editDto)
    {
        var created = await service.Create(editDto);
        logger.LogInformation("Создано занятие с id={Id}", created.Id);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Возвращает список всех занятий
    /// </summary>
    /// <returns>Коллекция занятий</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<LessonDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<LessonDto>>> GetAll() =>
        Ok(await service.ReadAll());

    /// <summary>
    /// Возвращает занятие по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор занятия</param>
    /// <returns>Занятие</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LessonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<LessonDto>> GetById(int id) =>
        Ok(await service.Read(id));

    /// <summary>
    /// Обновляет данные занятия
    /// </summary>
    /// <param name="id">Идентификатор занятия</param>
    /// <param name="editDto">Новые данные занятия</param>
    /// <returns>Обновлённое занятие</returns>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(LessonDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<LessonDto>> Update(int id, LessonEditDto editDto)
    {
        var updated = await service.Update(id, editDto);
        logger.LogInformation("Обновлено занятие с id={Id}", updated.Id);
        return Ok(updated);
    }

    /// <summary>
    /// Удаляет занятие
    /// </summary>
    /// <param name="id">Идентификатор занятия</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        await service.Delete(id);
        logger.LogInformation("Удалено занятие с id={Id}", id);
        return NoContent();
    }
}