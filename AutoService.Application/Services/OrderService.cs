using AutoService.Contracts.Dtos;
using AutoService.Contracts.Interfaces;
using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;

namespace AutoService.Application.Services;

/// <summary>
/// Сервис для управления заказами.
/// </summary>
public class OrderService(
    IOrderRepository repository,
    IWorkTypeRepository workRepository,
    ICarRepository carRepository,
    IClientRepository clientRepository,
    IMechanicRepository mechanicRepository) : IOrderService
{
    /// <summary>
    /// Получить список всех заказов.
    /// </summary>
    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        var orders = await repository.GetAllAsync();
        return orders.Select(MapToDto);
    }

    /// <summary>
    /// Получить заказ по идентификатору.
    /// </summary>
    public async Task<OrderDto?> GetByIdAsync(Guid id)
    {
        var o = await repository.GetByIdAsync(id);
        return o == null ? null : MapToDto(o);
    }

    /// <summary>
    /// Добавить новый заказ.
    /// </summary>
    public async Task<OrderDto> CreateAsync(OrderDto orderDto)
    {
        var car = await carRepository.GetByIdAsync(orderDto.CarId)
            ?? throw new Exception($"Автомобиль с идентификатором {orderDto.CarId} не найден");
        var client = await clientRepository.GetByIdAsync(orderDto.ClientId)
            ?? throw new Exception($"Клиент с идентификатором {orderDto.ClientId} не найден");
        var mechanic = await mechanicRepository.GetByIdAsync(orderDto.MechanicId)
            ?? throw new Exception($"Механик с идентификатором {orderDto.MechanicId} не найден");

        var works = new List<WorkType>();
        foreach (var workId in orderDto.WorkIds)
        {
            var work = await workRepository.GetByIdAsync(workId);
            if (work != null) works.Add(work);
        }

        var order = new Order
        {
            Id = Guid.NewGuid(),
            CarId = car.Id,
            Car = car,
            ClientId = client.Id,
            Client = client,
            MechanicId = mechanic.Id,
            Mechanic = mechanic,
            Works = works,
            AcceptedAt = orderDto.AcceptedAt,
            IssuedAt = orderDto.IssuedAt
        };

        await repository.AddAsync(order);
        orderDto.Id = order.Id;
        return orderDto;
    }

    /// <summary>
    /// Обновить данные заказа.
    /// </summary>
    public async Task UpdateAsync(Guid id, OrderUpdateDto orderDto)
    {
        var order = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Заказ с id {id} не найден");

        var car = await carRepository.GetByIdAsync(orderDto.CarId)
            ?? throw new Exception($"Автомобиль с идентификатором {orderDto.CarId} не найден");
        var client = await clientRepository.GetByIdAsync(orderDto.ClientId)
            ?? throw new Exception($"Клиент с идентификатором {orderDto.ClientId} не найден");
        var mechanic = await mechanicRepository.GetByIdAsync(orderDto.MechanicId)
            ?? throw new Exception($"Механик с идентификатором {orderDto.MechanicId} не найден");

        var works = new List<WorkType>();
        foreach (var workId in orderDto.WorkIds)
        {
            var work = await workRepository.GetByIdAsync(workId);
            if (work != null) works.Add(work);
        }

        order.CarId = car.Id;
        order.Car = car;
        order.ClientId = client.Id;
        order.Client = client;
        order.MechanicId = mechanic.Id;
        order.Mechanic = mechanic;
        order.Works = works;
        order.AcceptedAt = orderDto.AcceptedAt;
        order.IssuedAt = orderDto.IssuedAt;

        await repository.UpdateAsync(order);
    }

    /// <summary>
    /// Удалить заказ.
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var order = await repository.GetByIdAsync(id);
        if (order == null) return;
        await repository.DeleteAsync(order);
    }

    /// <summary>
    /// Вспомогательный метод для маппинга доменной модели в DTO.
    /// </summary>
    private static OrderDto MapToDto(Order o)
    {
        return new OrderDto
        {
            Id = o.Id,
            CarId = o.CarId,
            ClientId = o.ClientId,
            MechanicId = o.MechanicId,
            WorkIds = o.Works.Select(w => w.Id).ToList(),
            AcceptedAt = o.AcceptedAt,
            IssuedAt = o.IssuedAt
        };
    }
}