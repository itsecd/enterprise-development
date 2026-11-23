using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class OrderMechanicRepository : IOrderMechanicRepository
{
    private readonly List<OrderMechanic> _orderMechanics = new(AutoServiceData.OrderMechanics);

    /// <inheritdoc />
    public Task<OrderMechanic?> GetAsync(int id) => Task.FromResult(_orderMechanics.FirstOrDefault(o => o.Id == id));

    /// <inheritdoc />
    public Task<IReadOnlyList<OrderMechanic>> GetAllAsync() => Task.FromResult<IReadOnlyList<OrderMechanic>>(_orderMechanics);

    /// <inheritdoc />
    public Task AddAsync(OrderMechanic entity)
    {
        entity.Id = _orderMechanics.Count == 0 ? 1 : _orderMechanics.Max(o => o.Id) + 1;
        _orderMechanics.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task UpdateAsync(OrderMechanic entity)
    {
        var index = _orderMechanics.FindIndex(o => o.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        _orderMechanics[index] = entity;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var item = await GetAsync(id);
        return item != null && _orderMechanics.Remove(item);
    }
}
