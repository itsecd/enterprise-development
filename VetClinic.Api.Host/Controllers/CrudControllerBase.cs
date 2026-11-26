using Microsoft.AspNetCore.Mvc;
using VetClinic.Application.Contracts;

namespace VetClinic.Api.Host.Controllers;

/// <summary>
/// Базовый Crud контроллер
/// </summary>
/// <typeparam name="TDto">Тип исходящего DTO для возврата клиенту</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип входящего DTO для создания и обновления сущности</typeparam>
/// <typeparam name="TKey">Тип уникального идентификатора сущности</typeparam>
/// <param name="service">Прикладной сервис для работы с сущностью</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/[controller]")]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IApplicationService<TDto, TCreateUpdateDto, TKey> service,
    ILogger logger)
    : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Возвращает полный список всех доступных элементов
    /// </summary>
    /// <returns>Список DTO с HTTP-статусом 200 OK</returns>
    [EndpointSummary("Возвращает полный список всех доступных элементов")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
    {
        try
        {
            logger.LogInformation("Получение всех элементов типа {DtoType}", typeof(TDto).Name);
            var result = await service.GetAll();
            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при получении всех элементов типа {DtoType}", typeof(TDto).Name);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Возвращает конкретный элемент по его уникальному идентификатору
    /// </summary>
    /// <param name="id">Id запрашиваемого элемента</param>
    /// <returns>Найденный элемент (200 OK) или статус 404 Not Found</returns>
    [EndpointSummary("Возвращает конкретный элемент по его уникальному идентификатору")]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Get(TKey id)
    {
        try
        {
            logger.LogInformation("Получение элемента типа {DtoType} с Id {Id}", typeof(TDto).Name, id);
            var result = await service.Get(id);
            if (result is null)
            {
                logger.LogWarning("Элемент типа {DtoType} с Id {Id} не найден", typeof(TDto).Name, id);
                return NotFound();
            }
            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при получении элемента типа {DtoType} с Id {Id}", typeof(TDto).Name, id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Создает новый элемент на основе переданных в теле запроса данных
    /// </summary>
    /// <param name="dto">Модель данных для создания сущности</param>
    /// <returns>Созданный элемент со статусом 201 Created или ошибку валидации (400/404)</returns>
    [EndpointSummary("Создает новый элемент на основе переданных в теле запроса данных")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> Post(TCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Создание элемента типа {DtoType}", typeof(TDto).Name);
            var result = await service.Create(dto);
            logger.LogInformation("Элемент типа {DtoType} успешно создан", typeof(TDto).Name);
            return StatusCode(StatusCodes.Status201Created, result);
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(exception, "Не найдена связанная сущность при создании элемента типа {DtoType}", typeof(TDto).Name);
            return NotFound();
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Некорректные входные данные при создании элемента типа {Type}", typeof(TDto).Name);
            return BadRequest(exception.Message);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при создании элемента типа {DtoType}", typeof(TDto).Name);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Полностью обновляет данные существующего элемента по его идентификатору
    /// </summary>
    /// <param name="dto">Модель с обновленными полями сущности</param>
    /// <param name="id">Уникальный идентификатор обновляемого элемента</param>
    /// <returns>Обновленный элемент со статусом 200 OK или статусы (400/404)</returns>
    [EndpointSummary("Полностью обновляет данные существующего элемента по его идентификатору")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<TDto>> UpdateAsync(TCreateUpdateDto dto, TKey id)
    {
        try
        {
            logger.LogInformation("Обновление элемента типа {DtoType} с Id {Id}", typeof(TDto).Name, id);
            var result = await service.Update(dto, id);
            logger.LogInformation("Элемент типа {DtoType} с Id {Id} успешно обновлён", typeof(TDto).Name, id);
            return Ok(result);
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(exception, "Элемент типа {DtoType} с Id {Id} или связанная сущность не найдены", typeof(TDto).Name, id);
            return NotFound();
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Некорректные входные данные при обновлении элемента типа {Type}", typeof(TDto).Name);
            return BadRequest(exception.Message);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при обновлении элемента типа {DtoType} с Id {Id}", typeof(TDto).Name, id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Идемпотентно удаляет элемент по его уникальному идентификатору
    /// </summary>
    /// <param name="id">Уникальный идентификатор удаляемого элемента</param>
    /// <returns>Статус 204 No Content при успешном завершении или 409 Conflict при нарушении</returns>
    [EndpointSummary("Идемпотентно удаляет элемент по его уникальному идентификатору")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]

    public async Task<IActionResult> DeleteAsync(TKey id)
    {
        try
        {
            logger.LogInformation("Удаление элемента типа {DtoType} с Id {Id}", typeof(TDto).Name, id);
            var existing = await service.Get(id);
            if (existing is null)
            {
                logger.LogWarning("Элемент типа {DtoType} с Id {Id} не найден", typeof(TDto).Name, id);
                return NoContent();
            }
            var result = await service.Delete(id);
            if (!result)
            {
                logger.LogWarning("Элемент типа {DtoType} с Id {Id} не может быть удалён", typeof(TDto).Name, id);
                return Conflict();
            }
            logger.LogInformation("Элемент типа {DtoType} с Id {Id} успешно удалён", typeof(TDto).Name, id);
            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при удалении элемента типа {DtoType} с Id {Id}", typeof(TDto).Name, id);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}