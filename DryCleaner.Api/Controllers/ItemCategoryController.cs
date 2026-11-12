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
    public ActionResult<IList<ItemCategoryDto>> GetAll()
    {
        try 
        {
            logger.LogInformation("Запрос: Вывод всех категорий изделий");
            return Ok(service.GetAll()); 
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
    public ActionResult<ItemCategoryDto> Get(int id)
    {
        try
        {
            logger.LogInformation("Запрос: Получение категории по идентификатору");

            var result = service.Get(id);
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
    public ActionResult<IList<ItemDto>> GetCategoryItems(int id)
    {
        try
        {
            logger.LogInformation("Запрос списка изделий для категории с указанным идентификатором");

            var categoryExists = service.Get(id) != null;
            if (!categoryExists)
            {
                logger.LogWarning("Категория с указанным идентификатором не найдена");
                return NotFound("Категория с указанным идентификатором не найдена");
            }

            var items = itemService.GetByCategoryId(id);
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
    public ActionResult<ItemCategoryDto> Create([FromBody] ItemCategoryCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Запрос: Создание категории");

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var created = service.Create(dto);
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
    public ActionResult<ItemCategoryDto> Update(int id, [FromBody] ItemCategoryCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Запрос: Обновление категории");

            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(service.Update(dto, id));
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { Message = ex.Message });
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
    public IActionResult Delete(int id)
    {
        try
        {
            logger.LogInformation("Запрос: Удаление категории");

            if (!service.Delete(id))
            {
                logger.LogWarning("Категория изделия не существует или уже удалена");
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении категории");
            return StatusCode(500, "Ошибка сервера");
        }
    }
}