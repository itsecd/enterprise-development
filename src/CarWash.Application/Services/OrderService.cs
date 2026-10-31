using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;

namespace CarWash.Application.Services;

public class OrderService(
    IOrderRepository orderRepository,
    IClientRepository clientRepository,
    ICarRepository carRepository,
    IServiceRepository serviceRepository) : IOrderService
{
    public async Task<IEnumerable<OrderDto>> GetAllAsync()
    {
        var orders = await orderRepository.GetAllAsync();
        return orders.Select(MapToDto);
    }

    public async Task<OrderDto?> GetByIdAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);
        return order == null ? null : MapToDto(order);
    }

    public async Task<OrderDto> CreateAsync(OrderEditDto editDto)
    {
        var client = await clientRepository.GetByIdAsync(editDto.ClientId)
            ?? throw new KeyNotFoundException("Клиент не найден");
        var car = await carRepository.GetByIdAsync(editDto.CarId)
            ?? throw new KeyNotFoundException("Автомобиль не найден");
        var service = await serviceRepository.GetByIdAsync(editDto.ServiceId)
            ?? throw new KeyNotFoundException("Услуга не найдена");

        var order = new Order
        {
            Client = client,
            Car = car,
            Service = service,
            StartTime = editDto.StartTime,
            BoxNumber = editDto.BoxNumber
        };

        var created = await orderRepository.AddAsync(order);
        return MapToDto(created);
    }

    public async Task UpdateAsync(int id, OrderEditDto editDto)
    {
        var order = await orderRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Заказ не найден");

        var client = await clientRepository.GetByIdAsync(editDto.ClientId)
            ?? throw new KeyNotFoundException("Клиент не найден");
        var car = await carRepository.GetByIdAsync(editDto.CarId)
            ?? throw new KeyNotFoundException("Автомобиль не найден");
        var service = await serviceRepository.GetByIdAsync(editDto.ServiceId)
            ?? throw new KeyNotFoundException("Услуга не найдена");

        order.Client = client;
        order.Car = car;
        order.Service = service;
        order.StartTime = editDto.StartTime;
        order.BoxNumber = editDto.BoxNumber;

        await orderRepository.UpdateAsync(order);
    }

    public async Task DeleteAsync(int id)
    {
        await orderRepository.DeleteAsync(id);
    }

    private static OrderDto MapToDto(Order order)
    {
        return new OrderDto
        {
            Id = order.Id,
            ClientId = order.Client.Id,
            ClientFullName = order.Client.FullName,
            CarId = order.Car.Id,
            CarLicensePlate = order.Car.LicensePlate,
            ServiceId = order.Service.Id,
            ServiceName = order.Service.Name,
            StartTime = order.StartTime,
            BoxNumber = order.BoxNumber
        };
    }
}