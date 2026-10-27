using Hotel.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Host.Controllers;

/// <summary>
/// Базовый контроллер для CRUD операций над сущностями
/// </summary>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TInputDto, TKey>(ICrudService<TDto, TInputDto, TKey> appService, ILogger logger) : ControllerBase
    where TDto : class, IHasId<TKey>
    where TInputDto : class
    where TKey : struct
{
    /// <summary>
    /// Создать новую запись
    /// </summary>
    [EndpointSummary("Создать новую запись")]
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Create([FromBody] TInputDto dto)
    {
        logger.LogInformation("Вызван метод {Method} контроллера {Controller}. Данные: {@Dto}", nameof(Create), GetType().Name, dto);

        try
        {
            var result = appService.Create(dto);

            logger.LogInformation("Метод {Method} контроллера {Controller} успешно выполнен", nameof(Create), GetType().Name);

            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "При создании записи переданы некорректные данные");

            return BadRequest(new { message = exception.Message });
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Во время выполнения метода {Method} контроллера {Controller} произошла ошибка", nameof(Create), GetType().Name);

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Произошла внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Обновить существующую запись
    /// </summary>
    [EndpointSummary("Обновить существующую запись")]
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Update(TKey id, [FromBody] TInputDto dto)
    {
        logger.LogInformation("Метод {Method} контроллера {Controller} вызван с идентификатором {Id} и данными {@Dto}", nameof(Update), GetType().Name, id, dto);

        try
        {
            var result = appService.Update(id, dto);

            if (result is null)
            {
                logger.LogWarning("Запись с идентификатором {Id} не найдена для обновления", id);

                return NotFound(new
                {
                    message = $"Запись с идентификатором {id} не найдена"
                });
            }

            logger.LogInformation("Метод {Method} контроллера {Controller} успешно выполнен", nameof(Update), GetType().Name);

            return Ok(result);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "При обновлении записи с идентификатором {Id} переданы некорректные данные", id);

            return BadRequest(new { message = exception.Message });
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Во время выполнения метода {Method} контроллера {Controller} произошла ошибка.", nameof(Update), GetType().Name);

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Произошла внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Удалить запись по идентификатору
    /// </summary>
    [EndpointSummary("Удалить запись по идентификатору")]
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult Delete(TKey id)
    {
        logger.LogInformation("Метод {Method} контроллера {Controller} вызван с идентификатором {Id}", nameof(Delete), GetType().Name, id);

        try
        {
            var result = appService.Delete(id);

            if (!result)
            {
                logger.LogWarning("Запись с идентификатором {Id} не найдена для удаления", id);

                return NotFound(new
                {
                    message = $"Запись с идентификатором {id} не найдена"
                });
            }

            logger.LogInformation("Метод {Method} контроллера {Controller} успешно выполнен", nameof(Delete), GetType().Name);

            return Ok();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Во время выполнения метода {Method} контроллера {Controller} произошла ошибка", nameof(Delete), GetType().Name);

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Произошла внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Получить список всех записей
    /// </summary>
    [EndpointSummary("Получить список всех записей")]
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<IReadOnlyList<TDto>> GetAll()
    {
        logger.LogInformation("Метод {Method} контроллера {Controller} вызван", nameof(GetAll), GetType().Name);

        try
        {
            var result = appService.GetAll();

            logger.LogInformation("Метод {Method} контроллера {Controller} успешно выполнен", nameof(GetAll), GetType().Name);

            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Во время выполнения метода {Method} контроллера {Controller} произошла ошибка", nameof(GetAll), GetType().Name);

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Произошла внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Получить запись по идентификатору
    /// </summary>
    [EndpointSummary("Получить запись по идентификатору")]
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> GetById(TKey id)
    {
        logger.LogInformation("Метод {Method} контроллера {Controller} вызван с идентификатором {Id}", nameof(GetById), GetType().Name, id);

        try
        {
            var result = appService.GetById(id);

            if (result is null)
            {
                logger.LogWarning("Запись с идентификатором {Id} не найдена", id);

                return NotFound(new
                {
                    message = $"Запись с идентификатором {id} не найдена"
                });
            }

            logger.LogInformation("Метод {Method} контроллера {Controller} успешно выполнен", nameof(GetById), GetType().Name);

            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Во время выполнения метода {Method} контроллера {Controller} произошла ошибка", nameof(GetById), GetType().Name);

            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Произошла внутренняя ошибка сервера." });
        }
    }
}