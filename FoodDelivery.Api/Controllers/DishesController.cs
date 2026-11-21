using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции для работы с блюдами
/// </summary>
[ApiController]
[Route("api/dishes")]
public class DishesController(
    IDishService dishService,
    ILogger<DishesController> logger)
    : ControllerBase
{
    /// <summary>
    /// Возвращает все блюда
    /// </summary>
    [HttpGet]
    [ProducesResponseType(
        typeof(IList<DishDto>),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<DishDto>>> GetAll()
    {
        try
        {
            logger.LogInformation(
                "Получен запрос на получение всех блюд");

            var dishes = await dishService.GetAllAsync();

            return Ok(dishes);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при получении списка блюд");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Возвращает блюдо по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(
        typeof(DishDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DishDto>> GetById(int id)
    {
        try
        {
            var dish = await dishService.GetAsync(id);

            if (dish is null)
            {
                return NotFound(new
                {
                    message = "Блюдо не найдено"
                });
            }

            return Ok(dish);
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при получении блюда с идентификатором {DishId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Создаёт блюдо
    /// </summary>
    [HttpPost]
    [ProducesResponseType(
        typeof(DishDto),
        StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DishDto>> Create(
        DishCreateUpdateDto dto)
    {
        try
        {
            var dish = await dishService.CreateAsync(dto);

            logger.LogInformation(
                "Создано блюдо с идентификатором {DishId}",
                dish.Id);

            return CreatedAtAction(
                nameof(GetById),
                new { id = dish.Id },
                dish);
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(
                exception,
                "Не найдена связанная сущность при создании блюда");

            return BadRequest(new
            {
                message = "Указанная категория или ресторан не найдены"
            });
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Переданы некорректные данные при создании блюда");

            return BadRequest(new
            {
                message = "Переданы некорректные данные"
            });
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при создании блюда");

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Обновляет блюдо
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(
        typeof(DishDto),
        StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<DishDto>> Update(
        int id,
        DishCreateUpdateDto dto)
    {
        try
        {
            var existingDish = await dishService.GetAsync(id);

            if (existingDish is null)
            {
                return NotFound(new
                {
                    message = "Блюдо не найдено"
                });
            }

            var updatedDish =
                await dishService.UpdateAsync(dto, id);

            logger.LogInformation(
                "Обновлено блюдо с идентификатором {DishId}",
                id);

            return Ok(updatedDish);
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(
                exception,
                "Не найдена связанная сущность при обновлении блюда {DishId}",
                id);

            return BadRequest(new
            {
                message = "Указанная категория или ресторан не найдены"
            });
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(
                exception,
                "Переданы некорректные данные при обновлении блюда {DishId}",
                id);

            return BadRequest(new
            {
                message = "Переданы некорректные данные"
            });
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при обновлении блюда {DishId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Удаляет блюдо
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deleted =
                await dishService.DeleteAsync(id);

            if (deleted)
            {
                logger.LogInformation(
                    "Удалено блюдо с идентификатором {DishId}",
                    id);
            }

            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(
                exception,
                "Ошибка при удалении блюда {DishId}",
                id);

            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new { message = "Внутренняя ошибка сервера" });
        }
    }
}