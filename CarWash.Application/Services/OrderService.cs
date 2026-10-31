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
    public OrderDto Create(OrderCreateUpdateDto dto)
    {
        var car = carRepository.Read(dto.CarId);
        var procedure = procedureRepository.Read(dto.ProcedureId);

        var newId = orderRepository.ReadAll()
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

        orderRepository.Create(order);
        car.Orders.Add(order);
        procedure.Orders.Add(order);

        return MapToDto(order);
    }

    public OrderDto Get(int dtoId)
    {
        return MapToDto(orderRepository.Read(dtoId));
    }

    public List<OrderDto> GetAll()
    {
        return orderRepository.ReadAll()
            .Select(MapToDto)
            .ToList();
    }

    public OrderDto Update(OrderCreateUpdateDto dto, int dtoId)
    {
        var order = orderRepository.Read(dtoId);
        var car = carRepository.Read(dto.CarId);
        var procedure = procedureRepository.Read(dto.ProcedureId);

        if (order.CarId != car.Id)
        {
            order.Car.Orders.Remove(order);
            car.Orders.Add(order);
        }

        if (order.ProcedureId != procedure.Id)
        {
            order.Procedure.Orders.Remove(order);
            procedure.Orders.Add(order);
        }

        order.CarId = car.Id;
        order.Car = car;
        order.ProcedureId = procedure.Id;
        order.Procedure = procedure;
        order.StartTime = dto.StartTime;
        order.WashBoxNumber = dto.WashBoxNumber;

        orderRepository.Update(order);

        return MapToDto(order);
    }

    public void Delete(int dtoId)
    {
        var order = orderRepository.Read(dtoId);

        order.Car.Orders.Remove(order);
        order.Procedure.Orders.Remove(order);

        orderRepository.Delete(dtoId);
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
