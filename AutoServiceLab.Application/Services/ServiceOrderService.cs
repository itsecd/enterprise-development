using AutoServiceLab.Application.Contracts.DTOs.ServiceOrders;
using AutoServiceLab.Application.Contracts.Interfaces;
using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace AutoServiceLab.Application.Services;

/// <summary>
/// Сервис для работы с заказами
/// </summary>
public class ServiceOrderService(
    IServiceOrderRepository repository,
    IClientRepository clientRepo,
    ICarRepository carRepo,
    IOrderWorkRepository orderWorkRepo,
    ILogger<ServiceOrderService> logger) : IServiceOrderService
{
    /// <inheritdoc />
    public IList<ServiceOrderDto> GetAll()
    {
        logger.LogInformation("Получение списка всех заказов");
        return repository.GetAll()
            .Select(o => new ServiceOrderDto(o.Id, o.ReceptionDate, o.IssueDate, o.ClientId, o.Client?.FullName ?? "", o.CarId, o.Car?.Model ?? ""))
            .ToList();
    }

    /// <inheritdoc />
    public ServiceOrderDto? Get(int id)
    {
        logger.LogInformation("Получение заказа с ID: {Id}", id);
        var o = repository.Get(id);
        return o == null ? null : new ServiceOrderDto(o.Id, o.ReceptionDate, o.IssueDate, o.ClientId, o.Client?.FullName ?? "", o.CarId, o.Car?.Model ?? "");
    }

    /// <inheritdoc />
    public ServiceOrderDto Create(ServiceOrderCreateUpdateDto dto)
    {
        logger.LogInformation("Создание заказа для клиента {ClientId}", dto.ClientId);
        var order = new ServiceOrder
        {
            ReceptionDate = DateTime.Now,
            IssueDate = dto.IssueDate,
            ClientId = dto.ClientId,
            Client = clientRepo.Get(dto.ClientId) ?? throw new Exception("Клиент не найден"),
            CarId = dto.CarId,
            Car = carRepo.Get(dto.CarId) ?? throw new Exception("Машина не найдена")
        };
        repository.Add(order);
        return new ServiceOrderDto(order.Id, order.ReceptionDate, order.IssueDate, order.ClientId, order.Client.FullName, order.CarId, order.Car.Model);
    }

    /// <inheritdoc />
    public ServiceOrderDto Update(ServiceOrderCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление заказа с ID: {Id}", id);
        var order = repository.Get(id) ?? throw new KeyNotFoundException();
        order.IssueDate = dto.IssueDate;
        order.ClientId = dto.ClientId;
        order.Client = clientRepo.Get(dto.ClientId) ?? throw new Exception("Клиент не найден");
        order.CarId = dto.CarId;
        order.Car = carRepo.Get(dto.CarId) ?? throw new Exception("Машина не найдена");
        repository.Update(order);
        return new ServiceOrderDto(order.Id, order.ReceptionDate, order.IssueDate, order.ClientId, order.Client.FullName, order.CarId, order.Car.Model);
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        logger.LogInformation("Удаление заказа с ID: {Id}", id);
        return repository.Delete(id);
    }

    /// <inheritdoc />
    public IList<string> GetWorksByOrder(int orderId)
    {
        logger.LogInformation("Получение работ для заказа с ID: {OrderId}", orderId);
        return orderWorkRepo.GetAll()
            .Where(ow => ow.ServiceOrderId == orderId)
            .Select(ow => ow.WorkType?.Name ?? "Неизвестно")
            .ToList();
    }
}
