using HotelSystem.Application.Contracts;
using HotelSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.Controllers;

/// <summary>
/// Базовый контроллер с общей реализацией CRUD-операций
/// </summary>
public abstract class BaseCrudController<TRequest, TResponse>(
    ICrudService<TRequest, TResponse> service,
    ILogger logger) : ControllerBase
    where TResponse : IEntityResponse
{
    /// <summary>
    /// Возвращает список всех сущностей
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<List<TResponse>> GetAll()
    {
        var entities = service.GetAll();

        logger.LogInformation(
            "Получен список сущностей типа {EntityType}. Количество: {Count}",
            typeof(TResponse).Name,
            entities.Count);

        return Ok(entities);
    }

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public ActionResult<TResponse> GetById(int id)
    {
        var entity = service.GetById(id);

        if (entity is null)
        {
            logger.LogWarning(
                "Сущность типа {EntityType} с ID {Id} не найдена",
                typeof(TResponse).Name,
                id);

            return NotFound();
        }

        logger.LogInformation(
            "Получена сущность типа {EntityType} с ID {Id}",
            typeof(TResponse).Name,
            id);

        return Ok(entity);
    }

    /// <summary>
    /// Создаёт новую сущность
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    public ActionResult<TResponse> Create([FromBody] TRequest request)
    {
        var created = service.Add(request);

        logger.LogInformation(
            "Создана сущность типа {EntityType} с ID {Id}",
            typeof(TResponse).Name,
            created.Id);

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Обновляет существующую сущность
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public ActionResult Update(int id, [FromBody] TRequest request)
    {
        if (!service.Update(id, request))
        {
            logger.LogWarning(
                "Не удалось обновить сущность типа {EntityType}: ID {Id} не найден",
                typeof(TResponse).Name,
                id);

            return NotFound();
        }

        logger.LogInformation(
            "Обновлена сущность типа {EntityType} с ID {Id}",
            typeof(TResponse).Name,
            id);

        return NoContent();
    }

    /// <summary>
    /// Удаляет сущность по идентификатору
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public ActionResult Delete(int id)
    {
        if (service.Delete(id))
        {
            logger.LogInformation(
                "Удалена сущность типа {EntityType} с ID {Id}",
                typeof(TResponse).Name,
                id);
        }
        else
        {
            logger.LogWarning(
                "Не удалось удалить сущность типа {EntityType} с ID {Id}: сущность не найдена",
                typeof(TResponse).Name,
                id);
        }

        return NoContent();
    }
}