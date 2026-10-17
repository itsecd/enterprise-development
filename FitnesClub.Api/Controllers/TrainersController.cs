using Microsoft.AspNetCore.Mvc;
using FitnesClub.Api.Contracts.Trainers;
using FitnesClub.Api.Storage;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Controllers;

/// <summary>
/// Контроллер для управления учетными записями и анкетами тренеров.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class TrainersController : ControllerBase
{
    private readonly IRepository<Trainer, Guid> _repo;

    /// <summary>
    /// Инициализирует новый экземпляр контроллера тренеров.
    /// </summary>
    /// <param name="repo">Репозиторий для работы с данными тренеров.</param>
    public TrainersController(IRepository<Trainer, Guid> repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Возвращает список всех действующих тренеров клуба.
    /// </summary>
    /// <returns>Коллекция DTO тренеров с контактной информацией и специализацией.</returns>
    [HttpGet]
    public ActionResult<IReadOnlyList<TrainerDto>> GetAll()
    {
        var trainers = _repo.ReadAll();
        return Ok(trainers.Select(t => t.ToDto()).ToList());
    }

    /// <summary>
    /// Возвращает данные тренера по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор тренера.</param>
    /// <returns>DTO найденного тренера или статус 404, если тренер не найден.</returns>
    [HttpGet("{id:guid}")]
    public ActionResult<TrainerDto> GetById(Guid id)
    {
        var trainer = _repo.Read(id);
        if (trainer is null)
            return NotFound($"Тренер с ID {id} не найден.");
        return Ok(trainer.ToDto());
    }

    /// <summary>
    /// Создает новую запись тренера в системе.
    /// </summary>
    /// <param name="dto">Данные для регистрации тренера (персональные данные, специализация, стаж).</param>
    /// <returns>Созданный тренер с присвоенным идентификатором.</returns>
    [HttpPost]
    public ActionResult<TrainerDto> Create([FromBody] TrainersCreateUpdateDto dto)
    {
        var newTrainer = dto.ToEntity();
        _repo.Create(newTrainer);
        return CreatedAtAction(nameof(GetById), new { id = newTrainer.Id }, newTrainer.ToDto());
    }

    /// <summary>
    /// Обновляет анкетные данные, контакты или специализацию существующего тренера.
    /// </summary>
    /// <param name="id">Уникальный идентификатор обновляемого тренера.</param>
    /// <param name="dto">Новые данные тренера.</param>
    /// <returns>Статус 204 при успешном обновлении или 404, если тренер не найден.</returns>
    [HttpPut("{id:guid}")]
    public ActionResult Update(Guid id, [FromBody] TrainersCreateUpdateDto dto)
    {
        var existingTrainer = _repo.Read(id);
        if (existingTrainer is null)
            return NotFound($"Тренер с ID {id} не найден.");

        dto.UpdateEntity(existingTrainer);
        _repo.Update(existingTrainer);

        return NoContent();
    }

    /// <summary>
    /// Удаляет запись тренера из системы по его идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор удаляемого тренера.</param>
    /// <returns>Статус 204 при успешном удалении или 404, если тренер не найден.</returns>
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var existingTrainer = _repo.Read(id);
        if (existingTrainer is null)
            return NotFound($"Тренер с ID {id} не найден.");

        _repo.Delete(id);
        return NoContent();
    }
}