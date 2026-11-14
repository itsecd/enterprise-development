using CarWash.Application.Exceptions;
using CarWash.Application.Mapping;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using CarWash.Domain.Models;
using CarWash.Domain.Repositories;

namespace CarWash.Application.Services;

/// <summary>
/// Операции приложения с заказами
/// </summary>
public class OrderService(
    IOrderRepository orders,
    ICustomerRepository customers,
    ICarRepository cars,
    IWashServiceRepository services) : IOrderService
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<OrderResponse>> GetAllAsync(CancellationToken cancellationToken = default) => (await orders.GetAllAsync(cancellationToken)).Select(order => order.ToResponse()).ToList();

    /// <inheritdoc />
    public async Task<OrderResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        (await orders.GetByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException("Заказ не найден.")).ToResponse();

    /// <inheritdoc />
    public async Task<OrderResponse> CreateAsync(OrderRequest request, CancellationToken cancellationToken = default)
    {
        var order = await CreateOrderAsync(request, cancellationToken);
        await orders.AddAsync(order, cancellationToken);
        return order.ToResponse();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(int id, OrderRequest request, CancellationToken cancellationToken = default)
    {
        _ = await GetByIdAsync(id, cancellationToken);
        var order = await CreateOrderAsync(request, cancellationToken);
        order.Id = id;
        await orders.UpdateAsync(order, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        _ = await GetByIdAsync(id, cancellationToken);
        await orders.DeleteAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<CustomerResponse> GetCustomerAsync(int id, CancellationToken cancellationToken = default) =>
        (await orders.GetCustomerAsync(id, cancellationToken) ?? throw new EntityNotFoundException("Заказ не найден.")).ToResponse();

    /// <inheritdoc />
    public async Task<CarResponse> GetCarAsync(int id, CancellationToken cancellationToken = default) =>
        (await orders.GetCarAsync(id, cancellationToken) ?? throw new EntityNotFoundException("Заказ не найден.")).ToResponse();

    /// <inheritdoc />
    public async Task<WashServiceResponse> GetWashServiceAsync(int id, CancellationToken cancellationToken = default) =>
        (await orders.GetWashServiceAsync(id, cancellationToken) ?? throw new EntityNotFoundException("Заказ не найден.")).ToResponse();

    private async Task<Order> CreateOrderAsync(OrderRequest request, CancellationToken cancellationToken = default)
    {
        var customer = await customers.GetByIdAsync(request.CustomerId, cancellationToken)
            ?? throw new EntityNotFoundException("Клиент не найден.");
        var car = await cars.GetByIdAsync(request.CarId, cancellationToken)
            ?? throw new EntityNotFoundException("Автомобиль не найден.");
        var service = await services.GetByIdAsync(request.WashServiceId, cancellationToken)
            ?? throw new EntityNotFoundException("Услуга не найдена.");
        var order = request.ToDomain(customer, car, service);
        try
        {
            _ = order.EndTime;
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new RequestValidationException(nameof(request.StartTime),
                "Время окончания заказа выходит за допустимые границы даты.");
        }
        return order;
    }
}
