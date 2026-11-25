using CarWash.Application.Exceptions;
using CarWash.Application.Mapping;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using CarWash.Domain.Repositories;

namespace CarWash.Application.Services;

/// <summary>
/// Операции приложения с автомобилями
/// </summary>
public class CarService(ICarRepository cars, ICustomerRepository customers, IOrderRepository orders) : ICarService
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<CarResponse>> GetAllAsync(CancellationToken cancellationToken = default) => (await cars.GetAllAsync(cancellationToken)).Select(car => car.ToResponse()).ToList();

    /// <inheritdoc />
    public async Task<CarResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        (await cars.GetByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException("Автомобиль не найден.")).ToResponse();

    /// <inheritdoc />
    public async Task<CarResponse> CreateAsync(CarRequest request, CancellationToken cancellationToken = default)
    {
        var customer = await customers.GetByIdAsync(request.CustomerId, cancellationToken)
            ?? throw new EntityNotFoundException("Владелец автомобиля не найден.");
        var car = request.ToDomain(customer);
        await cars.AddAsync(car, cancellationToken);
        return car.ToResponse();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(int id, CarRequest request, CancellationToken cancellationToken = default)
    {
        _ = await GetByIdAsync(id, cancellationToken);
        var customer = await customers.GetByIdAsync(request.CustomerId, cancellationToken)
            ?? throw new EntityNotFoundException("Владелец автомобиля не найден.");
        var car = request.ToDomain(customer);
        car.Id = id;
        await cars.UpdateAsync(car, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        _ = await GetByIdAsync(id, cancellationToken);
        if (await orders.AnyAsync(order => order.CarId == id, cancellationToken))
            throw new EntityConflictException("Автомобиль используется в заказах.");
        await cars.DeleteAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<CustomerResponse> GetCustomerAsync(int id, CancellationToken cancellationToken = default) =>
        (await cars.GetCustomerAsync(id, cancellationToken) ?? throw new EntityNotFoundException("Автомобиль не найден.")).ToResponse();

    /// <inheritdoc />
    public async Task<IReadOnlyList<OrderResponse>> GetOrdersAsync(int id, CancellationToken cancellationToken = default)
    {
        _ = await GetByIdAsync(id, cancellationToken);
        return (await orders.GetByCarIdAsync(id, cancellationToken)).Select(order => order.ToResponse()).ToList();
    }
}
