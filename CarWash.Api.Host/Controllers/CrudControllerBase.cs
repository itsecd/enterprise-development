using CarWash.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Host.Controllers;

/// <summary>
/// Базовый контроллер для CRUD-операций над сущностями
/// </summary>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationService<TDto, TCreateUpdateDto, TKey> appService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger)
    : ControllerBase
    where TDto : class, IHasId<TKey>
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Добавление новой записи
    /// </summary>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Create(TCreateUpdateDto newDto)
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller} с параметром {@dto}",
            nameof(Create), GetType().Name, newDto);

        try
        {
            var result = appService.Create(newDto);

            logger.LogInformation(
                "Метод {method} контроллера {controller} успешно выполнен",
                nameof(Create), GetType().Name);

            return CreatedAtAction(nameof(Get), new { id = GetId(result) }, result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при выполнении метода {method} контроллера {controller}",
                nameof(Create), GetType().Name);

            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Изменение имеющихся данных
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Edit(TKey id, TCreateUpdateDto newDto)
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller} с параметрами {key},{@dto}",
            nameof(Edit), GetType().Name, id, newDto);

        try
        {
            var result = appService.Update(newDto, id);

            logger.LogInformation(
                "Метод {method} контроллера {controller} успешно выполнен",
                nameof(Edit), GetType().Name);

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(
                ex,
                "Запись с идентификатором {id} не найдена при выполнении метода {method} контроллера {controller}",
                id, nameof(Edit), GetType().Name);

            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при выполнении метода {method} контроллера {controller}",
                nameof(Edit), GetType().Name);

            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Удаление данных
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public IActionResult Delete(TKey id)
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller} с параметром {id}",
            nameof(Delete), GetType().Name, id);

        try
        {
            appService.Delete(id);

            logger.LogInformation(
                "Метод {method} контроллера {controller} успешно выполнен",
                nameof(Delete), GetType().Name);

            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(
                ex,
                "Запись с идентификатором {id} не найдена при выполнении метода {method} контроллера {controller}",
                id, nameof(Delete), GetType().Name);

            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при выполнении метода {method} контроллера {controller}",
                nameof(Delete), GetType().Name);

            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получение списка всех данных
    /// </summary>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<IList<TDto>> GetAll()
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller}",
            nameof(GetAll), GetType().Name);

        try
        {
            var result = appService.GetAll();

            logger.LogInformation(
                "Метод {method} контроллера {controller} успешно выполнен",
                nameof(GetAll), GetType().Name);

            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при выполнении метода {method} контроллера {controller}",
                nameof(GetAll), GetType().Name);

            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Получение данных по идентификатору
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Get(TKey id)
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller} с параметром {id}",
            nameof(Get), GetType().Name, id);

        try
        {
            var result = appService.Get(id);

            logger.LogInformation(
                "Метод {method} контроллера {controller} успешно выполнен",
                nameof(Get), GetType().Name);

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(
                ex,
                "Запись с идентификатором {id} не найдена при выполнении метода {method} контроллера {controller}",
                id, nameof(Delete), GetType().Name);

            return NotFound();
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Ошибка при выполнении метода {method} контроллера {controller}",
                nameof(Get), GetType().Name);

            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    private static object? GetId(TDto dto)
    {
        return dto.Id;
    }
}
