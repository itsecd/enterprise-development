using Microsoft.AspNetCore.Mvc;
using FitnesClub.Api.Contracts.Members;
using FitnesClub.Api.Storage;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Controllers;

/// <summary>
/// Контроллер для управления учетными записями клиентов фитнес-клуба.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly IRepository<Member, Guid> _repo;

    /// <summary>
    /// Инициализирует новый экземпляр контроллера клиентов.
    /// </summary>
    /// <param name="repo">Репозиторий для работы с данными клиентов.</param>
    public MembersController(IRepository<Member, Guid> repo)
    {
        _repo = repo;
    }

    /// <summary>
    /// Возвращает список всех клиентов клуба.
    /// </summary>
    /// <returns>Коллекция DTO клиентов.</returns>
    [HttpGet]
    public ActionResult<IReadOnlyList<MemberDto>> GetAll()
    {
        var members = _repo.ReadAll();
        return Ok(members.Select(m => m.ToDto()).ToList());
    }

    /// <summary>
    /// Возвращает данные клиента по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор клиента.</param>
    /// <returns>DTO найденного клиента или статус 404, если клиент не найден.</returns>
    [HttpGet("{id:guid}")]
    public ActionResult<MemberDto> GetById(Guid id)
    {
        var member = _repo.Read(id);
        if (member is null)
            return NotFound($"Клиент с ID {id} не найден.");

        return Ok(member.ToDto());
    }

    /// <summary>
    /// Создает новую запись клиента клуба.
    /// </summary>
    /// <param name="dto">Данные для регистрации клиента и параметров его абонемента.</param>
    /// <returns>Созданный клиент с присвоенным идентификатором.</returns>
    [HttpPost]
    public ActionResult<MemberDto> Create([FromBody] MemberCreateUpdateDto dto)
    {
        var newMember = dto.ToEntity();
        _repo.Create(newMember);

        return CreatedAtAction(nameof(GetById), new { id = newMember.Id }, newMember.ToDto());
    }

    /// <summary>
    /// Обновляет персональные данные или срок абонемента существующего клиента.
    /// </summary>
    /// <param name="id">Уникальный идентификатор обновляемого клиента.</param>
    /// <param name="dto">Новые данные клиента.</param>
    /// <returns>Статус 204 при успешном обновлении или 404, если клиент не найден.</returns>
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] MemberCreateUpdateDto dto)
    {
        var existingMember = _repo.Read(id);
        if (existingMember is null)
            return NotFound($"Клиент с ID {id} не найден.");

        dto.UpdateEntity(existingMember);
        _repo.Update(existingMember);

        return NoContent();
    }

    /// <summary>
    /// Удаляет запись клиента из системы по его идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор удаляемого клиента.</param>
    /// <returns>Статус 204 при успешном удалении или 404, если клиент не найден.</returns>
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var existingMember = _repo.Read(id);
        if (existingMember is null)
            return NotFound($"Клиент с ID {id} не найден.");

        _repo.Delete(id);
        return NoContent();
    }
}