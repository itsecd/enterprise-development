using EnterpriseDevelopment.Application.Interfaces;
using EnterpriseDevelopment.Contracts.DTOs.Orders;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseDevelopment.Api.Controllers;

/// <summary>
/// Контроллер для управления заказами
/// </summary>
[ApiController]
[Route("orders")]
public class OrdersController(
    IOrderService service,
    ILogger<OrdersController> logger) : ControllerBase
{
    /// <summary>
    /// Получает все заказы
    /// </summary>
    /// <returns>Список заказов</returns>
    [HttpGet]
    public ActionResult<IEnumerable<OrderDto>> GetAll()
    {
        logger.LogInformation("Запрошен список заказов");

        return Ok(service.GetAll());
    }

    /// <summary>
    /// Получает заказ по идентификатору
    /// </summary>
    /// <returns>Данные заказа</returns>
    [HttpGet("{id:int}")]
    public ActionResult<OrderDto> GetById(int id)
    {
        logger.LogInformation("Запрошен заказ с ID {OrderId}", id);

        var order = service.GetById(id);

        if (order is null)
        {
            logger.LogWarning("Заказ с ID {OrderId} не найден", id);
            return NotFound();
        }

        return Ok(order);
    }

    /// <summary>
    /// Создаёт заказ
    /// </summary>
    /// <returns>Данные созданного заказа</returns>
    [HttpPost]
    public ActionResult<OrderDto> Create(CreateOrderDto dto)
    {
        var order = service.Create(dto);

        logger.LogInformation("Создан заказ с ID {OrderId}", order.Id);

        return CreatedAtAction(
            nameof(GetById),
            new { id = order.Id },
            order);
    }

    /// <summary>
    /// Обновляет заказ
    /// </summary>
    /// <returns>Результат обновления</returns>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, UpdateOrderDto dto)
    {
        if (!service.Update(id, dto))
        {
            logger.LogWarning("Заказ с ID {OrderId} не найден", id);
            return NotFound();
        }

        logger.LogInformation("Обновлён заказ с ID {OrderId}", id);

        return NoContent();
    }

    /// <summary>
    /// Удаляет заказ
    /// </summary>
    /// <returns>Результат удаления</returns>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        service.Delete(id);

        logger.LogInformation(
            "Обработано удаление заказа с ID {OrderId}", id);

        return NoContent();
    }
}
