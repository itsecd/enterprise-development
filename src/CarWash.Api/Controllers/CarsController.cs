using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Controllers;

/// <summary>
/// Контроллер для работы с автомобилями
/// </summary>
/// <param name="carService">Сервис для работы с автомобилями</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/[controller]")]
[ProducesResponseType<ProblemDetails>(StatusCodes.Status500InternalServerError)]
public class CarsController(ICarService carService, ILogger<CarsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех автомобилей
    /// </summary>
    /// <returns>Список автомобилей</returns>
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CarDto>))]
    public async Task<ActionResult<IEnumerable<CarDto>>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос списка автомобилей");
            return Ok(await carService.GetAllAsync());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка автомобилей");
            return Problem(detail: "Не удалось получить список автомобилей", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить автомобиль по ID
    /// </summary>
    /// <param name="id">ID автомобиля</param>
    /// <returns>Данные автомобиля</returns>
    [HttpGet("{id:int}")]
    [ProducesResponseType(200, Type = typeof(CarDto))]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CarDto>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос автомобиля с ID {Id}", id);
            var item = await carService.GetByIdAsync(id);
            if (item == null)
            {
                logger.LogWarning("Автомобиль с ID {Id} не найден", id);
                return Problem(detail: $"Автомобиль с ID {id} не найден", statusCode: StatusCodes.Status404NotFound);
            }

            return Ok(item);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении автомобиля с ID {Id}", id);
            return Problem(detail: "Не удалось получить автомобиль", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Создать автомобиль
    /// </summary>
    /// <param name="dto">Данные автомобиля</param>
    /// <returns>Созданный объект</returns>
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(CarDto))]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<CarDto>> Create([FromBody] CarEditDto dto)
    {
        try
        {
            var created = await carService.CreateAsync(dto);
            logger.LogInformation("Создан автомобиль с ID {Id}", created.Id);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании автомобиля");
            return Problem(detail: "Не удалось создать автомобиль", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Изменить автомобиль
    /// </summary>
    /// <param name="id">ID автомобиля</param>
    /// <param name="dto">Новые данные автомобиля</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType<ValidationProblemDetails>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] CarEditDto dto)
    {
        try
        {
            await carService.UpdateAsync(id, dto);
            logger.LogInformation("Обновлён автомобиль с ID {Id}", id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не удалось изменить автомобиль с ID {Id}: {Message}", id, ex.Message);
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при изменении автомобиля с ID {Id}", id);
            return Problem(detail: "Не удалось изменить автомобиль", statusCode: StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Удалить автомобиль
    /// </summary>
    /// <param name="id">ID автомобиля</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType<ProblemDetails>(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await carService.DeleteAsync(id);
            logger.LogInformation("Удалён автомобиль с ID {Id}", id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Не удалось удалить автомобиль с ID {Id}: {Message}", id, ex.Message);
            return Problem(detail: ex.Message, statusCode: StatusCodes.Status404NotFound);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении автомобиля с ID {Id}", id);
            return Problem(detail: "Не удалось удалить автомобиль", statusCode: StatusCodes.Status500InternalServerError);
        }
    }
}
