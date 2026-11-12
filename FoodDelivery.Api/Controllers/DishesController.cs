using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Application.Services;
using FoodDelivery.Contracts.Dishes;

namespace FoodDelivery.Api.Controllers;

/// <summary>
/// Контроллер для управления блюдами в меню ресторанов.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class DishesController(IDishService dishService, ILogger<DishesController> logger) : ControllerBase
{
    /// <summary>
    /// Получить список всех блюд.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<DishDto>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос списка всех блюд");
            return Ok(dishService.GetAll());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка блюд");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Получить блюдо по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<DishDto> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос блюда с Id = {Id}", id);
            var dish = dishService.GetById(id);
            if (dish is null)
            {
                logger.LogWarning("Блюдо с Id = {Id} не найдено", id);
                return NotFound(new { Message = $"Блюдо с Id = {id} не найдено." });
            }
            return Ok(dish);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении блюда с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Добавить новое блюдо.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<DishDto> Create([FromBody] DishCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Создание нового блюда: {Name} для ресторана {RestaurantId}", dto.Name, dto.RestaurantId);
            var created = dishService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании блюда");
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Обновить информацию о блюде.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(int id, [FromBody] DishCreateUpdateDto dto)
    {
        try
        {
            logger.LogInformation("Обновление блюда с Id = {Id}", id);
            if (!dishService.Update(id, dto))
            {
                logger.LogWarning("Блюдо с Id = {Id} не найдено для обновления", id);
                return NotFound(new { Message = $"Блюдо с Id = {id} не найдено." });
            }
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении блюда с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Удалить блюдо по идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            logger.LogInformation("Удаление блюда с Id = {Id}", id);
            dishService.Delete(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении блюда с Id = {Id}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Внутренняя ошибка сервера." });
        }
    }
}