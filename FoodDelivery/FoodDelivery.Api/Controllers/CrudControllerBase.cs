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
[Route("api/[controller]")]
[ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError, "application/problem+json")]
public abstract class CrudControllerBase<TDto, TRequest>(
    ICrudService<TDto, TRequest> service,
    ILogger logger) : ControllerBase
    where TDto : class
    where TRequest : class
{
    /// <summary>
    /// Возвращает все ресурсы
    /// </summary>
    /// <returns>Коллекция ресурсов</returns>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IReadOnlyList<TDto>> GetAll()
    {
        var resources = service.GetAll();

        logger.LogInformation(
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
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public ActionResult<TDto> GetById(int id)
    {
        var resource = service.GetById(id);

        if (resource is null)
        {
            logger.LogInformation(
                "Ресурс {ResourceType} с идентификатором {Id} не найден",
                typeof(TDto).Name,
                id);

            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "Ресурс не найден");
        }

        logger.LogInformation(
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
    [EndpointSummary("Создаёт ресурс")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    public virtual ActionResult<TDto> Create([FromBody] TRequest request)
    {
        var resource = service.Create(request);
        var id = GetResourceId(resource);

        logger.LogInformation(
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
    [EndpointSummary("Изменяет ресурс")]
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, "application/problem+json")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound, "application/problem+json")]
    public virtual ActionResult<TDto> Update(int id, [FromBody] TRequest request)
    {
        var resource = service.Update(id, request);

        if (resource is null)
        {
            logger.LogInformation(
                "Не найден ресурс {ResourceType} для изменения, идентификатор: {Id}",
                typeof(TDto).Name,
                id);

            return Problem(
                statusCode: StatusCodes.Status404NotFound,
                title: "Ресурс не найден");
        }

        logger.LogInformation(
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
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public virtual IActionResult Delete(int id)
    {
        service.Delete(id);

        logger.LogInformation(
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
