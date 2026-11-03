using Microsoft.AspNetCore.Mvc;
using FitnesClub.Api.Contracts.Specializations;
using FitnesClub.Domain.Entities;
using FitnesClub.Domain.Interfaces;

namespace FitnesClub.Api.Controllers;

/// <summary>
/// Контроллер для управления справочником специализаций тренеров.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class SpecializationsController(
    IRepository<Specialization, Guid> repo,
    ILogger<SpecializationsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает полный список всех специализаций тренеров.
    /// </summary>
    /// <returns>Коллекция DTO специализаций.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<SpecializationDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<SpecializationDto>> GetAll()
    {
        logger.LogInformation("Запрос на получение списка всех специализаций");

        try
        {
            var specializations = repo.ReadAll();
            return Ok(specializations.Select(s => s.ToDto()).ToList());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка всех специализаций");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при получении списка специализаций." });
        }
    }

    /// <summary>
    /// Возвращает специализацию по её уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор специализации.</param>
    /// <returns>DTO найденной специализации или статус 404, если специализация не найдена.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<SpecializationDto> GetById(Guid id)
    {
        logger.LogInformation("Запрос на получение специализации с ID: {SpecializationId}", id);

        try
        {
            var specialization = repo.Read(id);
            if (specialization is null)
            {
                logger.LogWarning("Специализация с ID {SpecializationId} не найдена в хранилище", id);
                return NotFound(new { message = $"Специализация с ID {id} не найдена." });
            }

            return Ok(specialization.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении специализации с ID: {SpecializationId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Создает новую запись специализации в справочнике.
    /// </summary>
    /// <param name="dto">Данные для создания новой специализации.</param>
    /// <returns>Созданная специализация с присвоенным идентификатором.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(SpecializationDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<SpecializationDto> Create([FromBody] SpecializationCreateUpdateDto dto)
    {
        logger.LogInformation("Создание новой специализации: {SpecializationName}", dto.Name);

        try
        {
            var newSpecialization = dto.ToEntity();
            repo.Create(newSpecialization);

            logger.LogInformation("Специализация успешно создана с ID: {SpecializationId}", newSpecialization.Id);
            return CreatedAtAction(nameof(GetById), new { id = newSpecialization.Id }, newSpecialization.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании специализации: {SpecializationName}", dto.Name);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при создании специализации." });
        }
    }

    /// <summary>
    /// Обновляет наименование существующей специализации.
    /// </summary>
    /// <param name="id">Уникальный идентификатор обновляемой специализации.</param>
    /// <param name="dto">Новые данные специализации.</param>
    /// <returns>Статус 204 при успешном обновлении или 404, если специализация не найдена.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(Guid id, [FromBody] SpecializationCreateUpdateDto dto)
    {
        logger.LogInformation("Попытка обновления специализации с ID: {SpecializationId}", id);

        try
        {
            var existing = repo.Read(id);
            if (existing is null)
            {
                logger.LogWarning("Специализация с ID {SpecializationId} не найдена для обновления", id);
                return NotFound(new { message = $"Специализация с ID {id} не найдена." });
            }

            dto.UpdateEntity(existing);
            repo.Update(existing);

            logger.LogInformation("Специализация с ID {SpecializationId} успешно обновлена", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении специализации с ID: {SpecializationId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при обновлении специализации." });
        }
    }

    /// <summary>
    /// Удаляет специализацию из справочника по её идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор удаляемой специализации.</param>
    /// <returns>Статус 204 при успешном удалении (в том числе повторном).</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(Guid id)
    {
        logger.LogInformation("Запрос на удаление специализации с ID: {SpecializationId}", id);

        try
        {
            var existing = repo.Read(id);
            if (existing is not null)
            {
                repo.Delete(id);
                logger.LogInformation("Специализация с ID {SpecializationId} успешно удалена", id);
            }
            else
            {
                logger.LogInformation("Специализация с ID {SpecializationId} уже отсутствует в хранилище", id);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении специализации с ID: {SpecializationId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при удалении специализации." });
        }
    }
}