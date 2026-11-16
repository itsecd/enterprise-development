using Microsoft.AspNetCore.Mvc;
using DryCleaning.Infrastructure.Repositories;
using DryCleaning.Domain.Models;
using DryCleaning.Api.Dtos;

namespace DryCleaning.Api.Controllers;


/// <summary>
/// Контроллер изделий
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ItemsController(
    IItemRepository repository, 
    ICategoryRepository categoryRepository,
    ILogger<ItemsController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все изделия
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<ItemDto>> GetAll()
    {
        logger.LogInformation("Запрос всего списка объектов(изделий)");
        var items = repository.GetAll();
        var result = items.Select(i => new ItemDto(i.Id, i.Name, i.Material, i.CategoryId, i.Category.Name));
        return Ok(result);
    }

    /// <summary>
    /// Получить изделие по Id
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<ItemDto> GetById(int id)
    {
        logger.LogInformation("Запрос конкретного объекта(изделия) из доступных");
        var item = repository.GetById(id);
        if (item == null)
        {
            logger.LogWarning("Изделие с Id = {id} отсутствует", id);
            return NotFound(new { message = $"Изделие с Id = {id} не найдено" });
        }

        var result = new ItemDto(item.Id, item.Name, item.Material, item.CategoryId, item.Category.Name);
        return Ok(result);
    }

    /// <summary>
    /// Создать новое изделие
    /// </summary>
    [HttpPost]
    public ActionResult<ItemDto> Create([FromBody] CreateItemDto dto)
    {
        logger.LogInformation("Создание нового объекта(изделия)");
        var category = categoryRepository.GetById(dto.CategoryId);
        if (category == null)
        {
            logger.LogWarning("Категория с Id = {id} не существует", dto.CategoryId);
            return BadRequest(new { message = $"Категория с Id = {dto.CategoryId} не существует" });
        }

        var item = new Item
        {
            Name = dto.Name,
            Material = dto.Material,
            CategoryId = dto.CategoryId,
            Category = category
        };

        repository.Add(item);

        var result = new ItemDto(item.Id, item.Name, item.Material, item.CategoryId, item.Category.Name);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, result);
    }

    /// <summary>
    /// Обновить изделие
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] UpdateItemDto dto)
    {
        logger.LogInformation("Обновление объекта(изделия)");
        var item = repository.GetById(id);
        if (item == null)
        {
            logger.LogWarning("Изделие с Id = {id} отсутствует", id);
            return NotFound(new { message = $"Изделие с Id = {id} не найдено" });
        }

        var category = categoryRepository.GetById(dto.CategoryId);
        if (category == null)
        {
            logger.LogWarning("Категория с Id = {id} не существует", dto.CategoryId);
            return BadRequest(new { message = $"Категория с Id = {dto.CategoryId} не существует" });
        }

        item.Name = dto.Name;
        item.Material = dto.Material;
        item.CategoryId = dto.CategoryId;
        item.Category = category;

        repository.Update(item);
        return NoContent();
    }

    /// <summary>
    /// Удалить изделие
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        logger.LogInformation("Удаление объекта(изделия)");
        var item = repository.GetById(id);
        if (item == null)
        {
            logger.LogWarning("Изделие с Id = {id} отсутствует", id);
            return NotFound(new { message = $"Изделие с Id = {id} не найдено" });
        }

        repository.Delete(id);
        return NoContent();
    }
}
