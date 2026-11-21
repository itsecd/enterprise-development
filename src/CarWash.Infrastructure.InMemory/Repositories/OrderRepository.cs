using CarWash.Domain.Data;
using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;

namespace CarWash.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий, хранящий заказы в памяти
/// </summary>
public class OrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = [.. DataSeed.Orders];

    /// <inheritdoc />
    public Task<IEnumerable<Order>> GetAllAsync()
        => Task.FromResult<IEnumerable<Order>>([.. _orders]);

    /// <inheritdoc />
    public Task<Order?> GetByIdAsync(int id)
        => Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));

    /// <inheritdoc />
    public Task<Order> AddAsync(Order entity)
    {
        entity.Id = _orders.Count != 0 ? _orders.Max(o => o.Id) + 1 : 1;
        _orders.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc />
    public Task UpdateAsync(Order entity)
    {
        var index = _orders.FindIndex(o => o.Id == entity.Id);
        if (index != -1)
        {
            _orders[index] = entity;
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task DeleteAsync(int id)
    {
        var item = _orders.FirstOrDefault(o => o.Id == id);
        if (item != null)
        {
            _orders.Remove(item);
        }

        return Task.CompletedTask;
    }
}
