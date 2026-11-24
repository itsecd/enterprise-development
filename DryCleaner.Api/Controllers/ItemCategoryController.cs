using DryCleaner.Application.Contracts.DTOs.Item;
using DryCleaner.Application.Contracts.DTOs.ItemCategory;
using DryCleaner.Application.Contracts.DTOs.Customer;
using DryCleaner.Application.Contracts.DTOs.Order;
using DryCleaner.Application.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaner.Api.Controllers;

/// <summary>
/// HTTP-операции для работы с категориями изделий
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ItemCategoryController(IItemCategoryService service, IItemService itemService, ILogger<ItemCategoryController> logger) : ControllerBase
{
    /// <summary>
    /// Получение всех категорий изделий
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IList<ItemCategoryDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<ItemCategoryDto>>> GetAll()
    {
        try 
        {
            logger.LogInformation("Запрос: Вывод всех категорий изделий");
            return Ok(await service.GetAllAsync()); 
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении категорий");
            return StatusCode(500, "Ошибка сервера" );
        }
    }

    /// <summary>
    /// Получение категории изделия по идентификатору
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ItemCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ItemCategoryDto>> Get(int id)
    {
        try
        {
            logger.LogInformation("Запрос: Получение категории по идентификатору");

            var result = await service.GetAsync(id);
            if (result is null) return NotFound();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении категории по идентификатору");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Получение списка изделий в конкретной категории
    /// </summary>
    [HttpGet("{id:int}/items")]
    [ProducesResponseType(typeof(IList<ItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IList<ItemDto>>> GetCategoryItems(int id)
    {
        try
        {
            logger.LogInformation("Запрос списка изделий для категории с указанным идентификатором");

            var categoryExists = await service.GetAsync(id) != null;
            if (!categoryExists)
            {
                logger.LogWarning("Категория с указанным идентификатором не найдена");
                return NotFound("Категория с указанным идентификатором не найдена");
            }

            var items = await itemService.GetByCategoryIdAsync(id);
            return Ok(items);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении изделий категории с указанным идентификатором");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Создание категории изделия
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(ItemCategoryDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ItemCategoryDto>> Create([FromBody] ItemCategoryCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Запрос: Создание категории");

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании категории");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Обновление категории изделия
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(typeof(ItemCategoryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ItemCategoryDto>> Update(int id, [FromBody] ItemCategoryCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Запрос: Обновление категории");

            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await service.UpdateAsync(dto, id));
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { Message = "Категория не найдена" });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении категории");
            return StatusCode(500, "Ошибка сервера");
        }
    }

    /// <summary>
    /// Удаление категории изделия
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            logger.LogInformation("Запрос: Удаление категории");

            if (!(await service.DeleteAsync(id)))
            {
                logger.LogWarning("Категория изделия не существует или уже удалена");
            }
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(ex, "Категория изделия не может быть удалена из-за связанных сущностей");
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении категории");
            return StatusCode(500, "Ошибка сервера");
        }
    }
}