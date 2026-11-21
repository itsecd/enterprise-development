using FoodDelivery.Application.Contracts.Common;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет основные HTTP-операции с ресурсом
/// </summary>
/// <typeparam name="TDto">Тип данных ответа</typeparam>
/// <typeparam name="TRequest">Тип данных запроса</typeparam>
/// <param name="service">Прикладная служба ресурса</param>
/// <param name="logger">Средство журналирования</param>
[ApiController]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError, "application/problem+json")]
public abstract class CrudControllerBase<TDto, TRequest>(
    ICrudService<TDto, TRequest> service,
    ILogger logger) : ControllerBase
    where TDto : class
    where TRequest : class
{
    /// <summary>Прикладная служба ресурса</summary>
    protected ICrudService<TDto, TRequest> Service { get; } = service;

    /// <summary>Средство журналирования</summary>
    protected ILogger Logger { get; } = logger;

    /// <summary>
    /// Возвращает все ресурсы
    /// </summary>
    /// <returns>Коллекция ресурсов</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<TDto>>> GetAll(CancellationToken cancellationToken = default)
    {
        var resources = await Service.GetAllAsync(cancellationToken);

        Logger.LogInformation(
            "Получена коллекция {ResourceType}, количество: {Count}",
            typeof(TDto).Name,
            resources.Count);

        return Ok(resources);
    }

    /// <summary>
    /// Возвращает ресурс по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <returns>Найденный ресурс или сообщение об отсутствии</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public async Task<ActionResult<TDto>> GetById(int id, CancellationToken cancellationToken = default)
    {
        var resource = await Service.GetByIdAsync(id, cancellationToken);

        Logger.LogInformation(
            "Получен ресурс {ResourceType} с идентификатором {Id}",
            typeof(TDto).Name,
            id);

        return Ok(resource);
    }

    /// <summary>
    /// Создаёт ресурс
    /// </summary>
    /// <param name="request">Данные нового ресурса</param>
    /// <returns>Созданный ресурс</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [EndpointSummary("Создаёт ресурс")]
    [HttpPost]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    public virtual async Task<ActionResult<TDto>> Create([FromBody] TRequest request, CancellationToken cancellationToken = default)
    {
        var resource = await Service.CreateAsync(request, cancellationToken);
        var id = GetResourceId(resource);

        Logger.LogInformation(
            "Создан ресурс {ResourceType} с идентификатором {Id}",
            typeof(TDto).Name,
            id);

        return CreatedAtAction(nameof(GetById), new { id }, resource);
    }

    /// <summary>
    /// Изменяет ресурс
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <param name="request">Новые данные ресурса</param>
    /// <returns>Изменённый ресурс или сообщение об отсутствии</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [EndpointSummary("Изменяет ресурс")]
    [HttpPut("{id:int}")]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public virtual async Task<ActionResult<TDto>> Update(int id, [FromBody] TRequest request, CancellationToken cancellationToken = default)
    {
        var resource = await Service.UpdateAsync(id, request, cancellationToken);

        Logger.LogInformation(
            "Изменён ресурс {ResourceType} с идентификатором {Id}",
            typeof(TDto).Name,
            id);

        return Ok(resource);
    }

    /// <summary>
    /// Удаляет ресурс, если он существует
    /// </summary>
    /// <param name="id">Идентификатор ресурса</param>
    /// <returns>Ответ без содержимого</returns>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    [HttpDelete("{id:int}")]
    [ProducesDefaultResponseType(typeof(ProblemDetails))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict, "application/problem+json")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken = default)
    {
        await Service.DeleteAsync(id, cancellationToken);

        Logger.LogInformation(
            "Выполнено удаление ресурса {ResourceType} с идентификатором {Id}",
            typeof(TDto).Name,
            id);

        return NoContent();
    }

    /// <summary>
    /// Возвращает идентификатор ресурса
    /// </summary>
    /// <param name="resource">Данные ресурса</param>
    /// <returns>Идентификатор ресурса</returns>
    protected abstract int GetResourceId(TDto resource);
}
