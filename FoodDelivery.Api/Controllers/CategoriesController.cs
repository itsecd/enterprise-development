using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Application.Services;
using FoodDelivery.Contracts.Categories;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Контроллер для управления категориями блюд.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryService categoryService, ILogger<CategoriesController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех категорий блюд.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<CategoryDto>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос списка всех категорий блюд");
            return Ok(categoryService.GetAll());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка категорий");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Получить категорию блюд по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<CategoryDto> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос категории с Id = {Id}", id);
            var category = categoryService.GetById(id);
            if (category is null)
            {
                logger.LogWarning("Категория с Id = {Id} не найдена", id);
                return NotFound(new { Message = $"Категория с Id = {id} не найдена." });
            }
            return Ok(category);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении категории с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Создать новую категорию блюд.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<CategoryDto> Create([FromBody] CategoryCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Создание новой категории: {Name}", dto.Name);
            var created = categoryService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании категории");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Обновить название категории блюд.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(int id, [FromBody] CategoryCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Обновление категории с Id = {Id}", id);
            if (!categoryService.Update(id, dto))
            {
                logger.LogWarning("Категория с Id = {Id} не найдена для обновления", id);
                return NotFound(new { Message = $"Категория с Id = {id} не найдена." });
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении категории с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Удалить категорию блюд по идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            logger.LogInformation("Удаление категории с Id = {Id}", id);
            categoryService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении категории с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }
}