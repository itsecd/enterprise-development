using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Services;

namespace VetClinic.Api.Controllers;

/// <summary>
/// Базовый контроллер с CRUD-операциями, вся логика находится в сервисе
/// </summary>
/// <typeparam name="TDto">DTO для чтения</typeparam>
/// <typeparam name="TEditDto">DTO для создания и изменения</typeparam>
/// <param name="service">CRUD-сервис сущности</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Produces("application/json")]
public abstract class CrudControllerBase<TDto, TEditDto>(
    ICrudService<TDto, TEditDto> service,
    ILogger logger) : ControllerBase
    where TDto : class
    where TEditDto : class
{
    /// <summary>
    /// Получить все записи
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Список записей</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TDto>>> GetAll(CancellationToken cancellationToken)
    {
        logger.LogInformation("{Controller}: запрос всех записей", GetType().Name);
        return Ok(await service.GetAllAsync(cancellationToken));
    }

    /// <summary>
    /// Получить запись по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Найденная запись</response>
    /// <response code="404">Запись не найдена</response>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<TDto>> GetById(int id, CancellationToken cancellationToken)
    {
        logger.LogInformation("{Controller}: запрос записи {Id}", GetType().Name, id);
        var dto = await service.GetByIdAsync(id, cancellationToken);
        if (dto is null)
        {
            logger.LogWarning("{Controller}: запись {Id} не найдена", GetType().Name, id);
            return NotFoundRu(id);
        }
        return Ok(dto);
    }

    /// <summary>
    /// Создать запись
    /// </summary>
    /// <param name="dto">Данные новой записи</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="201">Запись создана</response>
    /// <response code="400">Данные не прошли валидацию</response>
    /// <response code="401">Нужен токен доступа</response>
    /// <response code="404">Связанная сущность не найдена</response>
    /// <response code="409">Изменение нарушает ограничения базы данных</response>
    [Authorize]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<TDto>> Create([FromBody] TEditDto dto, CancellationToken cancellationToken)
    {
        var created = await service.CreateAsync(dto, cancellationToken);
        var id = GetId(created);
        logger.LogInformation("{Controller}: создана запись {Id}", GetType().Name, id);
        return CreatedAtAction(nameof(GetById), new { id }, created);
    }

    /// <summary>
    /// Изменить запись
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="dto">Новые данные</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="200">Запись изменена</response>
    /// <response code="400">Данные не прошли валидацию</response>
    /// <response code="401">Нужен токен доступа</response>
    /// <response code="404">Запись или связанная сущность не найдена</response>
    /// <response code="409">Изменение нарушает ограничения базы данных или противоречит связанным записям</response>
    [Authorize]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<TDto>> Update(int id, [FromBody] TEditDto dto, CancellationToken cancellationToken)
    {
        var updated = await service.UpdateAsync(id, dto, cancellationToken);
        if (updated is null)
        {
            logger.LogWarning("{Controller}: запись {Id} для изменения не найдена", GetType().Name, id);
            return NotFoundRu(id);
        }
        logger.LogInformation("{Controller}: изменена запись {Id}", GetType().Name, id);
        return Ok(updated);
    }

    /// <summary>
    /// Удалить запись
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <response code="204">Записи больше нет</response>
    /// <response code="401">Нужен токен доступа</response>
    [Authorize]
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await service.DeleteAsync(id, cancellationToken);
        logger.LogInformation("{Controller}: удалена запись {Id}", GetType().Name, id);
        return NoContent();
    }

    /// <summary>
    /// Получить идентификатор созданной записи для заголовка Location
    /// </summary>
    /// <param name="dto">Созданная запись</param>
    protected abstract int GetId(TDto dto);

    /// <summary>
    /// Ответ 404 с русским описанием в формате ProblemDetails
    /// </summary>
    /// <param name="id">Идентификатор, который не найден</param>
    protected ObjectResult NotFoundRu(int id) => new(new ProblemDetails
    {
        Status = StatusCodes.Status404NotFound,
        Title = "Не найдено",
        Detail = $"Запись с идентификатором {id} не найдена"
    })
    {
        StatusCode = StatusCodes.Status404NotFound
    };
}
