using Microsoft.AspNetCore.Mvc;
using DryCleaning.Api.Services;
using DryCleaning.Domain.Models;

namespace DryCleaning.Api.Controllers;


/// <summary>
/// Контроллер изделий
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly InMemStore _store;

    public ItemsController(InMemStore store)
    {
        _store = store;
    }

    /// <summary>
    /// Получить все изделия
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<Item>> GetAll()
    {
        return Ok(_store.Items);
    }

    /// <summary>
    /// Получить изделие по Id
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<Item> GetById(int id)
    {
        var item = _store.Items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            return NotFound(new { message = $"Изделие с Id = {id} не найдено" });
        }
        return Ok(item);
    }

    /// <summary>
    /// Создать новое изделие
    /// </summary>
    [HttpPost]
    public ActionResult<Item> Create([FromBody] Item newItem)
    {
        var category = _store.Categories.FirstOrDefault(c => c.Id == newItem.CategoryId);
        if (category == null)
        {
            return BadRequest(new { message = $"Категория с Id = {newItem.CategoryId} не существует" });
        }

        var nextId = _store.Items.Any() ? _store.Items.Max(i => i.Id) + 1 : 1;
        newItem.Id = nextId;
        newItem.Category = category;

        _store.Items.Add(newItem);

        return CreatedAtAction(nameof(GetById), new { id = newItem.Id }, newItem);
    }

    /// <summary>
    /// Обновить изделие
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] Item updatedItem)
    {
        var item = _store.Items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            return NotFound(new { message = $"Изделие с Id = {id} не найдено" });
        }

        var category = _store.Categories.FirstOrDefault(c => c.Id == updatedItem.CategoryId);
        if (category == null)
        {
            return BadRequest(new { message = $"Категория с Id = {updatedItem.CategoryId} не существует" });
        }

        item.Name = updatedItem.Name;
        item.Material = updatedItem.Material;
        item.CategoryId = updatedItem.CategoryId;
        item.Category = category;

        return NoContent();
    }

    /// <summary>
    /// Удалить изделие
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var item = _store.Items.FirstOrDefault(i => i.Id == id);
        if (item == null)
        {
            return NotFound(new { message = $"Изделие с Id = {id} не найдено" });
        }

        _store.Items.Remove(item);
        return NoContent();
    }
}
