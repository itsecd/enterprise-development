using Microsoft.AspNetCore.Mvc;
using DryCleaning.Infrastructure.Repositories;
using DryCleaning.Domain.Models;
using DryCleaning.Api.Dtos;

namespace DryCleaning.Api.Controllers;


/// <summary>
/// Контроллер категорий
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CategoriesController(ICategoryRepository repository, ILogger<CategoriesController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все категории
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<CategoryDto>> GetAll()
    {
        logger.LogInformation("Запрос всего списка объектов(категорий)");
        var categories = repository.GetAll();

        var result = categories.Select(c => new CategoryDto(c.Id, c.Name, c.CleaningType, c.Price));

        return Ok(result);
    }

    /// <summary>
    /// Получить категорию по Id
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<CategoryDto> GetById(int id)
    {
        logger.LogInformation("Запрос конкретного объекта(категории) из доступных");
        var category = repository.GetById(id);
        if (category == null)
        {
            logger.LogWarning("Категория с Id = {id} отсутствует", id);
            return NotFound(new { message = $"Категория с Id = {id} не найдена" });
        }

        var result = new CategoryDto(category.Id, category.Name, category.CleaningType, category.Price);
        return Ok(result);
    }

    /// <summary>
    /// Создать новую категорию
    /// </summary>
    [HttpPost]
    public ActionResult<CategoryDto> Create([FromBody] CreateCategoryDto dto)
    {
        logger.LogInformation("Создание нового объекта(категории)");
        var category = new Category
        {
            Name = dto.Name,
            CleaningType = dto.CleaningType,
            Price = dto.Price
        };

        repository.Add(category);

        var result = new CategoryDto(category.Id, category.Name, category.CleaningType, category.Price);
        return CreatedAtAction(nameof(GetById), new { id = category.Id }, result);
    }

    /// <summary>
    /// Обновить категорию
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] UpdateCategoryDto dto)
    {
        logger.LogInformation("Обновление объекта(категории)");
        var category = repository.GetById(id);
        if (category == null)
        {
            logger.LogWarning("Категория с Id = {id} отсутствует", id);
            return NotFound(new { message = $"Категория с Id = {id} не найдена" });
        }

        category.Name = dto.Name;
        category.CleaningType = dto.CleaningType;
        category.Price = dto.Price;

        repository.Update(category);
        return NoContent();
    }

    /// <summary>
    /// Удалить категорию
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        logger.LogInformation("Удаление объекта(категории)");
        var category = repository.GetById(id);
        if (category == null)
        {
            logger.LogWarning("Категория с Id = {id} отсутствует", id);
            return NotFound(new { message = $"Категория с Id = {id} не найдена" });
        }

        repository.Delete(id);
        return NoContent();
    }
}
