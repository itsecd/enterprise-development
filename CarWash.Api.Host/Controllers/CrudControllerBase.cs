using CarWash.Application.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Create(TCreateUpdateDto newDto)
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller} с параметром {@dto}",
            nameof(Create), GetType().Name, newDto);

        try
        {
            var result = await appService.Create(newDto);

            logger.LogInformation(
                "Метод {method} контроллера {controller} успешно выполнен",
                nameof(Create), GetType().Name);

            return CreatedAtAction(nameof(Get), new { id = result.Id }, result);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(
                ex,
                "Не удалось создать запись: {Message}",
                ex.Message);

            return NotFound(ex.Message);
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
    public async Task<ActionResult<TDto>> Edit(TKey id, TCreateUpdateDto newDto)
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller} с параметрами {key},{@dto}",
            nameof(Edit), GetType().Name, id, newDto);

        try
        {
            var result = await appService.Update(newDto, id);

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

            return NotFound($"Запись с идентификатором {id} не найдена"); ;
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
    [ProducesResponseType(409)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(TKey id)
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller} с параметром {id}",
            nameof(Delete), GetType().Name, id);

        try
        {
            var isDeleted = await appService.Delete(id);

            if (!isDeleted)
                return NotFound($"Запись с идентификатором {id} не найдена");

            logger.LogInformation(
                "Метод {method} контроллера {controller} успешно выполнен",
                nameof(Delete), GetType().Name);

            return Ok();
        }
        catch (SqlException ex) when (ex.Number == 547)
        {
            logger.LogWarning(
                ex,
                "Невозможно удалить запись с идентификатором {id}: она используется другими записями",
                id);

            return Conflict(
                $"Невозможно удалить запись с идентификатором {id}, поскольку на неё ссылаются другие записи");
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
    public async Task<ActionResult<IList<TDto>>> GetAll()
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller}",
            nameof(GetAll), GetType().Name);

        try
        {
            var result = await appService.GetAll();

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
    public async Task<ActionResult<TDto>> Get(TKey id)
    {
        logger.LogInformation(
            "Вызван метод {method} контроллера {controller} с параметром {id}",
            nameof(Get), GetType().Name, id);

        try
        {
            var result = await appService.Get(id);

            if (result is null)
                return NotFound($"Запись с идентификатором {id} не найдена");

            logger.LogInformation(
                "Метод {method} контроллера {controller} успешно выполнен",
                nameof(Get), GetType().Name);

            return Ok(result);
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
}