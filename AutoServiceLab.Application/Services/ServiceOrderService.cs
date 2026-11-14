using AutoServiceLab.Application.Contracts.Dtos.Mechanics;
using AutoServiceLab.Application.Contracts.Dtos.ServiceOrders;
using AutoServiceLab.Application.Contracts.Dtos.WorkTypes;
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
    IOrderMechanicRepository orderMechanicRepo,
    ILogger<ServiceOrderService> logger) : IServiceOrderService
{
    /// <inheritdoc />
    public IList<ServiceOrderDto> GetAll()
    {
        logger.LogInformation("Получение списка всех заказов");
        return repository.GetAll().Select(MapToDto).ToList();
    }

    /// <inheritdoc />
    public ServiceOrderDto? Get(int id)
    {
        logger.LogInformation("Получение заказа с ID: {Id}", id);
        var o = repository.Get(id);
        return o == null ? null : MapToDto(o);
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
            Client = clientRepo.Get(dto.ClientId) ?? throw new KeyNotFoundException("Клиент не найден"),
            CarId = dto.CarId,
            Car = carRepo.Get(dto.CarId) ?? throw new KeyNotFoundException("Машина не найдена")
        };
        repository.Add(order);
        return MapToDto(order);
    }

    /// <inheritdoc />
    public ServiceOrderDto Update(ServiceOrderCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление заказа с ID: {Id}", id);
        var order = repository.Get(id) ?? throw new KeyNotFoundException();
        order.IssueDate = dto.IssueDate;
        order.ClientId = dto.ClientId;
        order.Client = clientRepo.Get(dto.ClientId) ?? throw new KeyNotFoundException("Клиент не найден");
        order.CarId = dto.CarId;
        order.Car = carRepo.Get(dto.CarId) ?? throw new KeyNotFoundException("Машина не найдена");
        repository.Update(order);
        return MapToDto(order);
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

    /// <summary>
    /// Вспомогательный метод для преобразования сущности в DTO
    /// </summary>
    private ServiceOrderDto MapToDto(ServiceOrder o)
    {
        var works = orderWorkRepo.GetAll()
            .Where(ow => ow.ServiceOrderId == o.Id)
            .Select(ow => new WorkTypeDto(ow.WorkType.Id, ow.WorkType.Name, ow.WorkType.Category, ow.WorkType.Duration, ow.WorkType.LaborCost))
            .ToList();

        var mechanics = orderMechanicRepo.GetAll()
            .Where(om => om.ServiceOrderId == o.Id)
            .Select(om => new MechanicDto(om.Mechanic.Id, om.Mechanic.FullName, om.Mechanic.Specialization, om.Mechanic.ExperienceYears))
            .ToList();

        return new ServiceOrderDto(
            o.Id,
            o.ReceptionDate,
            o.IssueDate,
            o.ClientId,
            o.Client?.FullName ?? "",
            o.CarId,
            o.Car?.Model ?? "",
            works,
            mechanics
        );
    }
}
