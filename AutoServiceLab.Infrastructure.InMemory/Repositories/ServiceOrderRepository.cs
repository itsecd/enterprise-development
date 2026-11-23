using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class ServiceOrderRepository : IServiceOrderRepository
{
    private readonly List<ServiceOrder> _orders = new(AutoServiceData.ServiceOrders);

    /// <inheritdoc />
    public Task<ServiceOrder?> GetAsync(int id) => Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));

    /// <inheritdoc />
    public Task<IReadOnlyList<ServiceOrder>> GetAllAsync() => Task.FromResult<IReadOnlyList<ServiceOrder>>(_orders);

    /// <inheritdoc />
    public Task AddAsync(ServiceOrder entity)
    {
        entity.Id = _orders.Count == 0 ? 1 : _orders.Max(o => o.Id) + 1;
        _orders.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task UpdateAsync(ServiceOrder entity)
    {
        var index = _orders.FindIndex(o => o.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        _orders[index] = entity;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var o = await GetAsync(id);
        return o != null && _orders.Remove(o);
    }
}
