using Microsoft.AspNetCore.Mvc;
using FitnesClub.Api.Contracts.Specializations;
using FitnesClub.Api.Storage;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Controllers;

/// <summary>
/// Контроллер для управления справочником специализаций тренеров.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SpecializationsController : ControllerBase
{
    private readonly IRepository<Specialization, Guid> _repo;

    /// <summary>
    /// Инициализирует новый экземпляр контроллера специализаций.
    /// </summary>
    /// <param name="repo">Репозиторий для работы со специализациями.</param>
    public SpecializationsController(IRepository<Specialization, Guid> repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Возвращает полный список всех специализаций тренеров.
    /// </summary>
    /// <returns>Коллекция DTO специализаций.</returns>
    [HttpGet]
    public ActionResult<IReadOnlyList<SpecializationDto>> GetAll()
    {
        var specializations = _repo.ReadAll();
        return Ok(specializations.Select(s => s.ToDto()).ToList());
    }

    /// <summary>
    /// Возвращает специализацию по её уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор специализации.</param>
    /// <returns>DTO найденной специализации или статус 404, если специализация не найдена.</returns>
    [HttpGet("{id:guid}")]
    public ActionResult<SpecializationDto> GetById(Guid id)
    {
        var specialization = _repo.Read(id);
        if (specialization is null)
            return NotFound($"Специализация с ID {id} не найдена.");

        return Ok(specialization.ToDto());
    }

    /// <summary>
    /// Создает новую запись специализации в справочнике.
    /// </summary>
    /// <param name="dto">Данные для создания новой специализации.</param>
    /// <returns>Созданная специализация с присвоенным идентификатором.</returns>
    [HttpPost]
    public ActionResult<SpecializationDto> Create([FromBody] SpecializationCreateUpdateDto dto)
    {
        var newSpecialization = dto.ToEntity();
        _repo.Create(newSpecialization);

        return CreatedAtAction(nameof(GetById), new { id = newSpecialization.Id }, newSpecialization.ToDto());
    }

    /// <summary>
    /// Обновляет наименование существующей специализации.
    /// </summary>
    /// <param name="id">Уникальный идентификатор обновляемой специализации.</param>
    /// <param name="dto">Новые данные специализации.</param>
    /// <returns>Статус 204 при успешном обновлении или 404, если специализация не найдена.</returns>
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] SpecializationCreateUpdateDto dto)
    {
        var existing = _repo.Read(id);
        if (existing is null)
            return NotFound($"Специализация с ID {id} не найдена.");

        dto.UpdateEntity(existing);
        _repo.Update(existing);

        return NoContent();
    }

    /// <summary>
    /// Удаляет специализацию из справочника по её идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор удаляемой специализации.</param>
    /// <returns>Статус 204 при успешном удалении или 404, если запись не найдена.</returns>
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var existing = _repo.Read(id);
        if (existing is null)
            return NotFound($"Специализация с ID {id} не найдена.");

        _repo.Delete(id);
        return NoContent();
    }
}