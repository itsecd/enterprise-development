using Microsoft.AspNetCore.Mvc;
using DryCleaning.Infrastructure.Repositories;
using DryCleaning.Domain.Models;
using DryCleaning.Api.Dtos;

namespace DryCleaning.Api.Controllers;


/// <summary>
/// Контроллер заказов
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrdersController(
    IOrderRepository repository,
    IClientRepository clientRepository,
    IItemRepository itemRepository,
    ILogger<OrdersController> logger) : ControllerBase
{
    /// <summary>
    /// Получить все заказы
    /// </summary>
    [HttpGet]
    public ActionResult<IEnumerable<OrderDto>> GetAll()
    {
        logger.LogInformation("Запрос всего списка объектов(заказов)");
        var orders = repository.GetAll();
        var result = orders.Select(o => new OrderDto(
            o.Id,
            o.ClientId,
            o.Client.FullName,
            o.ItemId,
            o.Item.Name,
            o.AcceptDate,
            o.ExeDays,
            o.Status
        ));
        return Ok(result);
    }

    /// <summary>
    /// Получить заказ по Id
    /// </summary>
    [HttpGet("{id:int}")]
    public ActionResult<OrderDto> GetById(int id)
    {
        logger.LogInformation("Запрос конкретного объекта(заказа) из доступных");
        var order = repository.GetById(id);
        if (order == null)
        {
            logger.LogWarning("Заказ с Id = {id} отсутствует", id);
            return NotFound(new { message = $"Заказ с Id = {id} не найден" });
        }

        var result = new OrderDto(
            order.Id, order.ClientId, order.Client.FullName,
            order.ItemId, order.Item.Name,
            order.AcceptDate, order.ExeDays, order.Status
        );
        return Ok(result);
    }

    /// <summary>
    /// Создать новый заказ
    /// </summary>
    [HttpPost]
    public ActionResult<OrderDto> Create([FromBody] CreateOrderDto dto)
    {
        logger.LogInformation("Создание нового объекта(заказа)");
        var client = clientRepository.GetById(dto.ClientId);
        if (client == null)
        {
            logger.LogWarning("Клиент с Id = {id} не найден", dto.ClientId);
            return BadRequest(new { message = $"Клиент с Id = {dto.ClientId} не найден" });
        }

        var item = itemRepository.GetById(dto.ItemId);
        if (item == null)
        {
            logger.LogWarning("Изделие с Id = {id} не найдено", dto.ItemId);
            return BadRequest(new { message = $"Изделие с Id = {dto.ItemId} не найдено" });
        }

        var order = new Order
        {
            ClientId = dto.ClientId,
            Client = client,
            ItemId = dto.ItemId,
            Item = item,
            AcceptDate = dto.AcceptDate == default ? DateTime.Today : dto.AcceptDate,
            ExeDays = dto.ExeDays,
            Status = dto.Status
        };

        repository.Add(order);

        var result = new OrderDto(
            order.Id, order.ClientId, order.Client.FullName,
            order.ItemId, order.Item.Name,
            order.AcceptDate, order.ExeDays, order.Status
        );
        return CreatedAtAction(nameof(GetById), new { id = order.Id }, result);
    }

    /// <summary>
    /// Обновить параметры заказа
    /// </summary>
    [HttpPut("{id:int}")]
    public IActionResult Update(int id, [FromBody] UpdateOrderDto dto)
    {
        logger.LogInformation("Обновление объекта(заказа)");
        var order = repository.GetById(id);
        if (order == null)
        {
            logger.LogWarning("Заказ с Id = {id} не найден", id);
            return NotFound(new { message = $"Заказ с Id = {id} не найден" });
        }

        var client = clientRepository.GetById(dto.ClientId);
        if (client == null)
        {
            logger.LogWarning("Клиент с Id = {id} не найден", dto.ClientId);
            return BadRequest(new { message = $"Клиент с Id = {dto.ClientId} не найден" });
        }

        var item = itemRepository.GetById(dto.ItemId);
        if (item == null)
        {
            logger.LogWarning("Изделие с Id = {id} не найдено", dto.ItemId);
            return BadRequest(new { message = $"Изделие с Id = {dto.ItemId} не найдено" });
        }

        order.ClientId = dto.ClientId;
        order.Client = client;
        order.ItemId = dto.ItemId;
        order.Item = item;
        order.AcceptDate = dto.AcceptDate;
        order.ExeDays = dto.ExeDays;
        order.Status = dto.Status;

        repository.Update(order);
        return NoContent();
    }

    /// <summary>
    /// Удалить заказ
    /// </summary>
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        logger.LogInformation("Удаление объекта(изделия)");
        var order = repository.GetById(id);
        if (order == null)
        {
            logger.LogWarning("Заказ с Id = {id} не найден", id);
            return NotFound(new { message = $"Заказ с Id = {id} не найден" });
        }

        repository.Delete(id);
        return NoContent();
    }
}
