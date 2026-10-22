using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Предоставляет HTTP-операции для работы с категориями
/// </summary>
[ApiController]
[Route("api/categories")]
public class CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает все категории
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IList<CategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IList<CategoryDto>> GetAll()
    {
        try
        {
            logger.LogInformation("Получен запрос на получение всех категорий");
            return Ok(categoryService.GetAll());
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при получении списка категорий");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Возвращает категорию по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<CategoryDto> GetById(int id)
    {
        try
        {
            var category = categoryService.Get(id);

            if (category is null)
            {
                return NotFound(new { message = "Категория не найдена" });
            }

            return Ok(category);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при получении категории {CategoryId}", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Создаёт категорию
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<CategoryDto> Create(CategoryCreateUpdateDto dto)
    {
        try
        {
            var category = categoryService.Create(dto);
            logger.LogInformation("Создана категория {CategoryId}", category.Id);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, category);
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Некорректные данные при создании категории");
            return BadRequest(new { message = "Переданы некорректные данные" });
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при создании категории");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Обновляет категорию
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<CategoryDto> Update(int id, CategoryCreateUpdateDto dto)
    {
        try
        {
            if (categoryService.Get(id) is null)
            {
                return NotFound(new { message = "Категория не найдена" });
            }

            return Ok(categoryService.Update(dto, id));
        }
        catch (KeyNotFoundException exception)
        {
            logger.LogWarning(exception, "Не найдена связанная сущность при обновлении категории {CategoryId}", id);
            return BadRequest(new { message = "Невозможно обновить категорию из-за отсутствующей связанной сущности" });
        }
        catch (ArgumentException exception)
        {
            logger.LogWarning(exception, "Некорректные данные при обновлении категории {CategoryId}", id);
            return BadRequest(new { message = "Переданы некорректные данные" });
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при обновлении категории {CategoryId}", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }

    /// <summary>
    /// Удаляет категорию
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            var deleted = categoryService.Delete(id);

            if (deleted)
            {
                logger.LogInformation("Удалена категория {CategoryId}", id);
            }

            return NoContent();
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Ошибка при удалении категории {CategoryId}", id);
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }
}