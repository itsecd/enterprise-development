using DryCleaner.Application.Contracts.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// Базовый абстрактный контроллер для реализации CRUD-операций с использованием сервисного слоя и логирования.
/// </summary>
/// <typeparam name="TDto">Тип DTO, используемый для передачи данных.</typeparam>
/// <typeparam name="TCreateUpdateDto">Тип DTO, используемый для создания и обновления сущности.</typeparam>
/// <typeparam name="TKey">Тип ключа, идентифицирующего сущность.</typeparam>
/// <param name="aplicationService">Сервис приложения для выполнения CRUD-операций.</param>
/// <param name="logger">Экземпляр логгера для записи информации о выполнении операций.</param>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(
    IAplicationService<TDto, TCreateUpdateDto, TKey> aplicationService,
    ILogger<CrudControllerBase<TDto, TCreateUpdateDto, TKey>> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает DTO по заданному идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор сущности.</param>
    /// <returns>Результат действия с DTO, если найден; NotFound, если не найден; или код ошибки при исключении.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Get(TKey id)
    {
        logger.LogInformation("{method} method of {Pcontroller} is called with {id} parametr",
            nameof(Get), GetType().Name, id);

        try
        {
            var result = await aplicationService.Get(id);

            return result == null ? NotFound() : Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}");
        }
    }

    /// <summary>
    /// Возвращает список всех объектов DTO.
    /// </summary>
    /// <returns>Код состояния 200 и список объектов DTO при успешном выполнении; код состояния 500 в случае ошибки сервера.</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IList<TDto>>> GetAll()
    {
        logger.LogInformation("{method} method of {Pcontroller} is called.",
            nameof(GetAll), GetType().Name);

        try
        {
            var result = await aplicationService.GetAll();
            logger.LogInformation("{method} method of {Pcontroller} was called. Result: {result}",
                nameof(GetAll), GetType().Name, result);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogInformation("{method} method of {Pcontroller} was called. Ended with an error: {errorText}",
                nameof(GetAll), GetType().Name, ex.Message);
            return StatusCode(500, $"{ex.Message}");
        }
    }

    /// <summary>
    /// Создает новый ресурс на основе предоставленных данных.
    /// </summary>
    /// <param name="dto">Данные для создания нового ресурса.</param>
    /// <returns>Результат создания ресурса с кодом состояния HTTP 201 при успехе или соответствующим кодом ошибки.</returns>
    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Create(TCreateUpdateDto dto)
    {
        logger.LogInformation("{method} method of {Pcontroller} is called with parameter: {dto}.",
            nameof(Create), GetType().Name, dto);

        try
        {
            var result = await aplicationService.Create(dto);
            logger.LogInformation("{method} method of {Pcontroller} was called with parameter: {dto}. Result: {result}",
                nameof(Create), GetType().Name, dto, result);

            return CreatedAtAction(nameof(Create), result);
        }
        catch (KeyNotFoundException notFound)
        {
            logger.LogInformation("{method} method of {Pcontroller} was called. Ended with an error: {errorText}",
                nameof(Create), GetType().Name, notFound.Message);
            return NotFound(notFound.Message);
        }
        catch (Exception ex)
        {
            logger.LogInformation("{method} method of {Pcontroller} was called with parameter: {dto}. Ended with an error: {errorText}",
                nameof(Create), GetType().Name, dto, ex.Message);
            return StatusCode(500, $"{ex.Message}");
        }

    }

    /// <summary>
    /// Обновляет сущность по идентификатору и возвращает обновлённый объект.
    /// </summary>
    /// <param name="dto">Данные для обновления сущности.</param>
    /// <param name="id">Идентификатор обновляемой сущности.</param>
    /// <returns>Результат действия с обновлённым объектом или код ошибки.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(201)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<TDto>> Update(TCreateUpdateDto dto, TKey id)
    {
        logger.LogInformation("{method} method of {Pcontroller} is called with parameter: id={id}, {dto}.",
            nameof(Update), GetType().Name, id, dto);

        try
        {
            var result = await aplicationService.Update(dto, id);
            logger.LogInformation("{method} method of {Pcontroller} was called with parameter: id={id}, {dto}. Result: {result}",
                nameof(Update), GetType().Name, id, dto, result);

            return Ok(result);
        }
        catch (KeyNotFoundException notFound)
        {
            logger.LogInformation("{method} method of {Pcontroller} was called. Ended with an error: {errorText}",
                nameof(Update), GetType().Name, notFound.Message);
            return NotFound(notFound.Message);
        }
        catch (Exception ex)
        {
            logger.LogInformation("{method} method of {Pcontroller} was called with parameter: id={id}, {dto}. Ended with an error: {errorText}",
                nameof(Update), GetType().Name, id, dto, ex.Message);
            return StatusCode(500, $"{ex.Message}");
        }

    }

    /// <summary>
    /// Удаляет сущность по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор удаляемой сущности.</param>
    /// <returns>Код состояния 200 (Ok) при успешном удалении; 404, если сущность не найдена; 500 при ошибке сервера.</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(201)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(TKey id)
    {
        logger.LogInformation("{method} method of {Pcontroller} is called with parameter: id={id}.",
            nameof(Delete), GetType().Name, id);

        try
        {
            var result = await aplicationService.Delete(id);
            logger.LogInformation("{method} method of {Pcontroller} was called with parameter: id={id}. Result: {result}",
                nameof(Delete), GetType().Name, id, result);

            return Ok();
        }
        catch (Exception ex)
        {
            logger.LogInformation("{method} method of {Pcontroller} was called with parameter: id={id}. Ended with an error: {errorText}",
                nameof(Delete), GetType().Name, id, ex.Message);
            return StatusCode(500, $"{ex.Message}");
        }

    }
}
