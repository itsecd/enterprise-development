using Microsoft.AspNetCore.Mvc;
using FitnesClub.Api.Contracts.Trainers;
using FitnesClub.Domain.Entities;
using FitnesClub.Domain.Interfaces;

namespace FitnesClub.Api.Controllers;

/// <summary>
/// Контроллер для управления учетными записями и анкетами тренеров.
/// </summary>
/// <param name="repo">Репозиторий для работы с данными тренеров.</param>
/// <param name="logger">Сервис структурированного логирования событий контроллера.</param>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class TrainersController(
    IRepository<Trainer, Guid> repo,
    ILogger<TrainersController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех действующих тренеров клуба.
    /// </summary>
    /// <returns>Коллекция DTO тренеров с контактной информацией и специализацией.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<TrainerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<TrainerDto>> GetAll()
    {
        logger.LogInformation("Запрос на получение списка всех тренеров");

        try
        {
            var trainers = repo.ReadAll();
            return Ok(trainers.Select(t => t.ToDto()).ToList());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка всех тренеров");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при получении списка тренеров." });
        }
    }

    /// <summary>
    /// Возвращает данные тренера по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор тренера.</param>
    /// <returns>DTO найденного тренера или статус 404, если тренер не найден.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TrainerDto> GetById(Guid id)
    {
        logger.LogInformation("Запрос на получение тренера с ID: {TrainerId}", id);

        try
        {
            var trainer = repo.Read(id);
            if (trainer is null)
            {
                logger.LogWarning("Тренер с ID {TrainerId} не найден", id);
                return NotFound(new { message = $"Тренер с ID {id} не найден." });
            }

            return Ok(trainer.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении тренера с ID: {TrainerId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Создает новую запись тренера в системе.
    /// </summary>
    /// <param name="dto">Данные для регистрации тренера (персональные данные, специализация, стаж).</param>
    /// <returns>Созданный тренер с присвоенным идентификатором.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TrainerDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<TrainerDto> Create([FromBody] TrainersCreateUpdateDto dto)
    {
        logger.LogInformation("Создание нового тренера: {LastName} {FirstName}", dto.LastName, dto.FirstName);

        try
        {
            var newTrainer = dto.ToEntity();
            repo.Create(newTrainer);

            logger.LogInformation("Тренер успешно создан с ID: {TrainerId}", newTrainer.Id);
            return CreatedAtAction(nameof(GetById), new { id = newTrainer.Id }, newTrainer.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании тренера {LastName} {FirstName}", dto.LastName, dto.FirstName);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при создании тренера." });
        }
    }

    /// <summary>
    /// Обновляет анкетные данные, контакты или специализацию существующего тренера.
    /// </summary>
    /// <param name="id">Уникальный идентификатор обновляемого тренера.</param>
    /// <param name="dto">Новые данные тренера.</param>
    /// <returns>Статус 204 при успешном обновлении или 404, если тренер не найден.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(Guid id, [FromBody] TrainersCreateUpdateDto dto)
    {
        logger.LogInformation("Попытка обновления тренера с ID: {TrainerId}", id);

        try
        {
            var existingTrainer = repo.Read(id);
            if (existingTrainer is null)
            {
                logger.LogWarning("Тренер с ID {TrainerId} не найден для обновления", id);
                return NotFound(new { message = $"Тренер с ID {id} не найден." });
            }

            dto.UpdateEntity(existingTrainer);
            repo.Update(existingTrainer);

            logger.LogInformation("Данные тренера с ID {TrainerId} успешно обновлены", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении тренера с ID: {TrainerId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при обновлении тренера." });
        }
    }

    /// <summary>
    /// Удаляет запись тренера из системы по его идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор удаляемого тренера.</param>
    /// <returns>Статус 204 при успешном удалении (в том числе повторном).</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(Guid id)
    {
        logger.LogInformation("Запрос на удаление тренера с ID: {TrainerId}", id);

        try
        {
            var existingTrainer = repo.Read(id);
            if (existingTrainer is not null)
            {
                repo.Delete(id);
                logger.LogInformation("Тренер с ID {TrainerId} успешно удален", id);
            }
            else
            {
                logger.LogInformation("Тренер с ID {TrainerId} уже отсутствует в хранилище", id);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении тренера с ID: {TrainerId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при удалении тренера." });
        }
    }
}