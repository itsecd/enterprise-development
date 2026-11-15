using Microsoft.AspNetCore.Mvc;
using FitnesClub.Api.Contracts.Members;
using FitnesClub.Domain.Entities;
using FitnesClub.Domain.Interfaces;

namespace FitnesClub.Api.Controllers;

/// <summary>
/// Контроллер для управления учетными записями клиентов фитнес-клуба.
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class MembersController(
    IRepository<Member, Guid> repo,
    ILogger<MembersController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех клиентов клуба.
    /// </summary>
    /// <returns>Коллекция DTO клиентов.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<MemberDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<MemberDto>> GetAll()
    {
        logger.LogInformation("Запрос на получение списка всех клиентов");

        try
        {
            var members = repo.ReadAll();
            return Ok(members.Select(m => m.ToDto()).ToList());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка всех клиентов");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при получении списка клиентов." });
        }
    }

    /// <summary>
    /// Возвращает данные клиента по его уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор клиента.</param>
    /// <returns>DTO найденного клиента или статус 404, если клиент не найден.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(MemberDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<MemberDto> GetById(Guid id)
    {
        logger.LogInformation("Запрос на получение данных клиента с ID: {MemberId}", id);

        try
        {
            var member = repo.Read(id);
            if (member is null)
            {
                logger.LogWarning("Клиент с ID {MemberId} не найден", id);
                return NotFound(new { message = $"Клиент с ID {id} не найден." });
            }

            return Ok(member.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении данных клиента с ID: {MemberId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Создает новую запись клиента клуба.
    /// </summary>
    /// <param name="dto">Данные для регистрации клиента и параметров его абонемента.</param>
    /// <returns>Созданный клиент с присвоенным идентификатором.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(MemberDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<MemberDto> Create([FromBody] MemberCreateUpdateDto dto)
    {
        logger.LogInformation("Создание нового клиента: {LastName} {FirstName}", dto.LastName, dto.FirstName);

        try
        {
            var newMember = dto.ToEntity();
            repo.Create(newMember);

            logger.LogInformation("Клиент успешно создан с ID: {MemberId}", newMember.Id);
            return CreatedAtAction(nameof(GetById), new { id = newMember.Id }, newMember.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании нового клиента");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при создании клиента." });
        }
    }

    /// <summary>
    /// Обновляет персональные данные или срок абонемента существующего клиента.
    /// </summary>
    /// <param name="id">Уникальный идентификатор обновляемого клиента.</param>
    /// <param name="dto">Новые данные клиента.</param>
    /// <returns>Статус 204 при успешном обновлении или 404, если клиент не найден.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(Guid id, [FromBody] MemberCreateUpdateDto dto)
    {
        logger.LogInformation("Попытка обновления данных клиента с ID: {MemberId}", id);

        try
        {
            var existingMember = repo.Read(id);
            if (existingMember is null)
            {
                logger.LogWarning("Клиент с ID {MemberId} не найден для обновления", id);
                return NotFound(new { message = $"Клиент с ID {id} не найден." });
            }

            dto.UpdateEntity(existingMember);
            repo.Update(existingMember);

            logger.LogInformation("Данные клиента с ID {MemberId} успешно обновлены", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении данных клиента с ID: {MemberId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при обновлении клиента." });
        }
    }

    /// <summary>
    /// Удаляет запись клиента из системы по его идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор удаляемого клиента.</param>
    /// <returns>Статус 204 при успешном удалении (в том числе повторном).</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(Guid id)
    {
        logger.LogInformation("Запрос на удаление клиента с ID: {MemberId}", id);

        try
        {
            var existingMember = repo.Read(id);
            if (existingMember is not null)
            {
                repo.Delete(id);
                logger.LogInformation("Клиент с ID {MemberId} успешно удален", id);
            }
            else
            {
                logger.LogInformation("Клиент с ID {MemberId} уже отсутствует в хранилище", id);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении клиента с ID: {MemberId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при удалении клиента." });
        }
    }
}