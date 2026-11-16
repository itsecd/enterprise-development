using CarWash.Application.Contracts;
using CarWash.Application.Contracts.Orders;
using CarWash.Domain;
using CarWash.Domain.Entities;

namespace CarWash.Application.Services;

/// <summary>
/// Служба для CRUD-операций над заказами
/// </summary>
public class OrderService(
    IRepository<Order, int> orderRepository,
    IRepository<Car, int> carRepository,
    IRepository<CarWashProcedure, int> procedureRepository)
    : IApplicationService<OrderDto, OrderCreateUpdateDto, int>
{
    public async Task<OrderDto> Create(OrderCreateUpdateDto dto)
    {
        var car = await carRepository.Read(dto.CarId)
            ?? throw new KeyNotFoundException(
                $"Автомобиль с Id {dto.CarId} не найден");

        var procedure = await procedureRepository.Read(dto.ProcedureId)
            ?? throw new KeyNotFoundException(
                $"Процедура с Id {dto.ProcedureId} не найдена");

        var newId = (await orderRepository.ReadAll())
            .Select(order => order.Id)
            .DefaultIfEmpty(0)
            .Max() + 1;

        var order = new Order
        {
            Id = newId,
            CarId = car.Id,
            Car = car,
            ProcedureId = procedure.Id,
            Procedure = procedure,
            StartTime = dto.StartTime,
            WashBoxNumber = dto.WashBoxNumber
        };

        await orderRepository.Create(order);

        car.Orders.Add(order);
        procedure.Orders.Add(order);

        return MapToDto(order);
    }

    public async Task<OrderDto?> Get(int dtoId)
    {
        var order = await orderRepository.Read(dtoId);

        return order is null ? null : MapToDto(order);
    }

    public async Task<IList<OrderDto>> GetAll()
    {
        var orders = await orderRepository.ReadAll();

        return orders
            .Select(MapToDto)
            .ToList();
    }

    public async Task<OrderDto> Update(
        OrderCreateUpdateDto dto,
        int dtoId)
    {
        var order = await orderRepository.Read(dtoId)
            ?? throw new KeyNotFoundException(
                $"Заказ с Id {dtoId} не найден");

        var car = await carRepository.Read(dto.CarId)
            ?? throw new KeyNotFoundException(
                $"Автомобиль с Id {dto.CarId} не найден");

        var procedure = await procedureRepository.Read(dto.ProcedureId)
            ?? throw new KeyNotFoundException(
                $"Процедура с Id {dto.ProcedureId} не найдена");

        if (order.CarId != car.Id)
        {
            order.Car?.Orders.Remove(order);
            car.Orders.Add(order);
        }

        if (order.ProcedureId != procedure.Id)
        {
            order.Procedure?.Orders.Remove(order);
            procedure.Orders.Add(order);
        }

        order.CarId = car.Id;
        order.Car = car;
        order.ProcedureId = procedure.Id;
        order.Procedure = procedure;
        order.StartTime = dto.StartTime;
        order.WashBoxNumber = dto.WashBoxNumber;

        var updatedOrder = await orderRepository.Update(order);

        return MapToDto(updatedOrder);
    }

    public async Task<bool> Delete(int dtoId)
    {
        var order = await orderRepository.Read(dtoId);

        if (order is null)
            return false;

        order.Car?.Orders.Remove(order);
        order.Procedure?.Orders.Remove(order);

        return await orderRepository.Delete(dtoId);
    }

    private static OrderDto MapToDto(Order order)
    {
        return new OrderDto(
            order.Id,
            order.CarId,
            order.ProcedureId,
            order.StartTime,
            order.WashBoxNumber);
    }
}