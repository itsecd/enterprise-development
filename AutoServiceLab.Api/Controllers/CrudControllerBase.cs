using AutoServiceLab.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutoServiceLab.Api.Controllers;

/// <summary>
/// Базовый контроллер для CRUD-операций
/// </summary>
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
    /// Получить все элементы
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<IList<TDto>>> GetAll()
    {
        try
        {
            logger.LogInformation("Получение всех элементов типа {DtoType}", typeof(TDto).Name);
            var result = await service.GetAllAsync();
            return Ok(result);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при получении всех элементов типа {DtoType}", typeof(TDto).Name);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить элемент по id
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> Get(TKey id)
    {
        try
        {
            logger.LogInformation("Получение элемента типа {DtoType} с Id {Id}", typeof(TDto).Name, id);
            var result = await service.GetAsync(id);
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
    /// Создать элемент
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> Post(TCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Создание элемента типа {DtoType}", typeof(TDto).Name);
            var result = await service.CreateAsync(dto);
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
    /// Обновить элемент по id
    /// </summary>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<ActionResult<TDto>> Update(TCreateUpdateDto dto, TKey id)
    {
        try
        {
            logger.LogInformation("Обновление элемента типа {DtoType} с Id {Id}", typeof(TDto).Name, id);
            var result = await service.UpdateAsync(dto, id);
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
    /// Удалить элемент по id
    /// </summary>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public virtual async Task<IActionResult> Delete(TKey id)
    {
        try
        {
            logger.LogInformation("Удаление элемента типа {DtoType} с Id {Id}", typeof(TDto).Name, id);

            await service.DeleteAsync(id);

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
