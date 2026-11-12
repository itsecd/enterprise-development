using Carwash.Domain;
using Carwash.Domain.Data;
using Carwash.Domain.Entities;

namespace Carwash.Infrastructure.InMemory;

/// <summary>
/// Имплементация репозитория для заказов
/// </summary>
public class OrderInMemoryRepository : IRepository<Order, int>
{
    private readonly List<Order> _orders = DataSeed.Orders;
    private int _lastId = DataSeed.Orders.Count + 1;

    /// <inheritdoc/>
    public Task<Order> Create(Order entity)
    {
        entity.Id = _lastId++;
        _orders.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    public Task<bool> Delete(int entityId)
    {
        var order = Read(entityId).Result;
        if (order == null)
            return Task.FromResult(false);

        var result = _orders.Remove(order);
        return Task.FromResult(result);
    }

    /// <inheritdoc/>
    public Task<List<Order>> ReadAll()
    {
        return Task.FromResult(_orders.ToList());
    }

    /// <inheritdoc/>
    public Task<Order?> Read(int entityId)
    {
        var order = _orders.FirstOrDefault(s => s.Id == entityId);
        return Task.FromResult(order);
    }

    /// <inheritdoc/>
    public Task<Order> Update(Order entity)
    {
        var order = Read(entity.Id).Result;

        if (order == null)
            return Create(entity);

        order.ClientId = entity.ClientId;
        order.Client = entity.Client;
        order.CarId = entity.CarId;
        order.Car = entity.Car;
        order.ServiceId = entity.ServiceId;
        order.Service = entity.Service;
        order.BoxNumber = entity.BoxNumber;
        order.StartTime = entity.StartTime;

        return Task.FromResult(order);
    }
}
