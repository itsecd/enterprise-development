using EnterpriseDevelopment.Application.Interfaces;
using EnterpriseDevelopment.Contracts.DTOs.Categories;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseDevelopment.Api.Controllers;

/// <summary>
/// Контроллер для управления категориями блюд
/// </summary>
[ApiController]
[Route("categories")]
public class CategoriesController(
    ICategoryService service,
    ILogger<CategoriesController> logger) : ControllerBase
{
    /// <summary>
    /// Получает все категории блюд
    /// </summary>
    /// <returns>Список категорий</returns>
    [HttpGet]
    public ActionResult<IEnumerable<CategoryDto>> GetAll()
    {
        logger.LogInformation("Запрошен список категорий блюд");

        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получает категорию по идентификатору
    /// </summary>
    /// <returns>Данные категории</returns>
    [HttpGet("{id:int}")]
    public ActionResult<CategoryDto> GetById(int id)
    {
        logger.LogInformation("Запрошена категория с ID {CategoryId}", id);

        var category = service.GetById(id);

        if (category is null)
        {
            logger.LogWarning("Категория с ID {CategoryId} не найдена", id);
            return NotFound();
        }

        return Ok(category);
    }

    /// <summary>
    /// Создаёт категорию блюд
    /// </summary>
    /// <returns>Данные созданной категории</returns>
    [HttpPost]
    public ActionResult<CategoryDto> Create(CreateCategoryDto dto)
    {
        var category = service.Create(dto);

        logger.LogInformation(
            "Создана категория с ID {CategoryId}", category.Id);

        return CreatedAtAction(
            nameof(GetById),
            new { id = category.Id },
            category);
    }

    /// <summary>
    /// Обновляет категорию блюд
    /// </summary>
    /// <returns>Результат обновления</returns>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UpdateCategoryDto dto)
    {
        if (!service.Update(id, dto))
        {
            logger.LogWarning(
                "Категория с ID {CategoryId} не найдена", id);

            return NotFound();
        }

        logger.LogInformation(
            "Обновлена категория с ID {CategoryId}", id);

        return NoContent();
    }

    /// <summary>
    /// Удаляет категорию блюд
    /// </summary>
    /// <returns>Результат удаления</returns>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        service.Delete(id);

        logger.LogInformation(
            "Обработано удаление категории с ID {CategoryId}", id);

        return NoContent();
    }
}
