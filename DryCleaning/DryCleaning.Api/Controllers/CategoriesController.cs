using Microsoft.AspNetCore.Mvc;
using DryCleaning.Api.Services;
using DryCleaning.Domain.Models;

namespace DryCleaning.Api.Controllers;


/// <summary>
/// Контроллер категорий
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly InMemStore _store;

    public CategoriesController(InMemStore store)
    {
        _store = store;
    }

    /// <summary>
    /// Получить все категории
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetAll()
    {
        return Ok(_store.Categories);
    }

    /// <summary>
    /// Получить категорию по Id
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<Category> GetById(int id)
    {
        var category = _store.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound(new { message = $"Категория с Id = {id} не найдена" });
        }
        return Ok(category);
    }

    /// <summary>
    /// Создать новую категорию
    /// </summary>
    [HttpPost]
    public ActionResult<Category> Create([FromBody] Category newCategory)
    {
        var nextId = _store.Categories.Any() ? _store.Categories.Max(c => c.Id) + 1 : 1;
        newCategory.Id = nextId;

        _store.Categories.Add(newCategory);

        return CreatedAtAction(nameof(GetById), new { id = newCategory.Id }, newCategory);
    }

    /// <summary>
    /// Обновить категорию
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Category updatedCategory)
    {
        var category = _store.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound(new { message = $"Категория с Id = {id} не найдена" });
        }

        category.Name = updatedCategory.Name;
        category.CleaningType = updatedCategory.CleaningType;
        category.Price = updatedCategory.Price;

        return NoContent();
    }

    /// <summary>
    /// Удалить категорию
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var category = _store.Categories.FirstOrDefault(c => c.Id == id);
        if (category == null)
        {
            return NotFound(new { message = $"Категория с Id = {id} не найдена" });
        }

        _store.Categories.Remove(category);
        return NoContent();
    }
}
