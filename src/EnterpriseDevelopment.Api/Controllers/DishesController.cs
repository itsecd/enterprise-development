using EnterpriseDevelopment.Application.Interfaces;
using EnterpriseDevelopment.Contracts.DTOs.Dishes;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseDevelopment.Api.Controllers;

/// <summary>
/// Контроллер для управления блюдами
/// </summary>
[ApiController]
[Route("dishes")]
public class DishesController(
    IDishService service,
    ILogger<DishesController> logger) : ControllerBase
{
    /// <summary>
    /// Получает все блюда
    /// </summary>
    /// <returns>Список блюд</returns>
    [HttpGet]
    public ActionResult<IEnumerable<DishDto>> GetAll()
    {
        logger.LogInformation("Запрошен список блюд");

        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получает блюдо по идентификатору
    /// </summary>
    /// <returns>Данные блюда</returns>
    [HttpGet("{id:int}")]
    public ActionResult<DishDto> GetById(int id)
    {
        logger.LogInformation("Запрошено блюдо с ID {DishId}", id);

        var dish = service.GetById(id);

        if (dish is null)
        {
            logger.LogWarning("Блюдо с ID {DishId} не найдено", id);
            return NotFound();
        }

        return Ok(dish);
    }

    /// <summary>
    /// Создаёт блюдо
    /// </summary>
    /// <returns>Данные созданного блюда</returns>
    [HttpPost]
    public ActionResult<DishDto> Create(CreateDishDto dto)
    {
        var dish = service.Create(dto);

        logger.LogInformation("Создано блюдо с ID {DishId}", dish.Id);

        return CreatedAtAction(
            nameof(GetById),
            new { id = dish.Id },
            dish);
    }

    /// <summary>
    /// Обновляет блюдо
    /// </summary>
    /// <returns>Результат обновления</returns>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UpdateDishDto dto)
    {
        if (!service.Update(id, dto))
        {
            logger.LogWarning("Блюдо с ID {DishId} не найдено", id);
            return NotFound();
        }

        logger.LogInformation("Обновлено блюдо с ID {DishId}", id);

        return NoContent();
    }

    /// <summary>
    /// Удаляет блюдо
    /// </summary>
    /// <returns>Результат удаления</returns>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        service.Delete(id);

        logger.LogInformation(
            "Обработано удаление блюда с ID {DishId}", id);

        return NoContent();
    }
}
