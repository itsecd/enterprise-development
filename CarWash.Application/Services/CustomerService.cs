using CarWash.Application.Exceptions;
using CarWash.Application.Mapping;
using CarWash.Contracts.Requests;
using CarWash.Contracts.Responses;
using CarWash.Contracts.Services;
using CarWash.Domain.Repositories;

namespace CarWash.Application.Services;

/// <summary>
/// Операции приложения с клиентами
/// </summary>
public class CustomerService(ICustomerRepository customers, IOrderRepository orders, ICarRepository cars) : ICustomerService
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<CustomerResponse>> GetAllAsync(CancellationToken cancellationToken = default) =>
        (await customers.GetAllAsync(cancellationToken)).Select(customer => customer.ToResponse()).ToList();

    /// <inheritdoc />
    public async Task<CustomerResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default) =>
        (await customers.GetByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException("Клиент не найден.")).ToResponse();

    /// <inheritdoc />
    public async Task<CustomerResponse> CreateAsync(CustomerRequest request, CancellationToken cancellationToken = default)
    {
        var customer = request.ToDomain();
        await customers.AddAsync(customer, cancellationToken);
        return customer.ToResponse();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(int id, CustomerRequest request, CancellationToken cancellationToken = default)
    {
        var customer = await customers.GetByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException("Клиент не найден.");
        request.ApplyTo(customer);
        await customers.UpdateAsync(customer, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        _ = await GetByIdAsync(id, cancellationToken);
        if (await orders.AnyAsync(order => order.CustomerId == id, cancellationToken))
            throw new EntityConflictException("Клиент используется в заказах.");
        if (await cars.AnyAsync(car => car.CustomerId == id, cancellationToken))
            throw new EntityConflictException("У клиента есть автомобили. Сначала смените их владельца или удалите автомобили.");
        await customers.DeleteAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<CarResponse>> GetCarsAsync(int id, CancellationToken cancellationToken = default)
    {
        _ = await GetByIdAsync(id, cancellationToken);
        return (await customers.GetCarsAsync(id, cancellationToken)).Select(car => car.ToResponse()).ToList();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<OrderResponse>> GetOrdersAsync(int id, CancellationToken cancellationToken = default)
    {
        _ = await GetByIdAsync(id, cancellationToken);
        return (await orders.GetByCustomerIdAsync(id, cancellationToken)).Select(order => order.ToResponse()).ToList();
    }
}
