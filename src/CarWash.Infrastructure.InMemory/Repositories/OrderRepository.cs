using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;
using CarWash.Infrastructure.InMemory.Data;

namespace CarWash.Infrastructure.InMemory.Repositories;

public class OrderRepository(InMemoryDataStore dataStore) : IOrderRepository
{
    public Task<IEnumerable<Order>> GetAllAsync()
        => Task.FromResult<IEnumerable<Order>>(dataStore.Orders);

    public Task<Order?> GetByIdAsync(int id)
        => Task.FromResult(dataStore.Orders.FirstOrDefault(o => o.Id == id));

    public Task<Order> AddAsync(Order entity)
    {
        entity.Id = dataStore.Orders.Any() ? dataStore.Orders.Max(o => o.Id) + 1 : 1;
        dataStore.Orders.Add(entity);
        return Task.FromResult(entity);
    }

    public Task UpdateAsync(Order entity)
    {
        var index = dataStore.Orders.FindIndex(o => o.Id == entity.Id);
        if (index != -1) dataStore.Orders[index] = entity;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var order = dataStore.Orders.FirstOrDefault(o => o.Id == id);
        if (order != null) dataStore.Orders.Remove(order);
        return Task.CompletedTask;
    }
}