using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class OrderWorkRepository : IOrderWorkRepository
{
    private readonly List<OrderWork> _orderWorks = new(AutoServiceData.OrderWorks);

    /// <inheritdoc />
    public Task<OrderWork?> GetAsync(int id) => Task.FromResult(_orderWorks.FirstOrDefault(o => o.Id == id));

    /// <inheritdoc />
    public Task<IReadOnlyList<OrderWork>> GetAllAsync() => Task.FromResult<IReadOnlyList<OrderWork>>(_orderWorks);

    /// <inheritdoc />
    public Task AddAsync(OrderWork entity)
    {
        entity.Id = _orderWorks.Count == 0 ? 1 : _orderWorks.Max(o => o.Id) + 1;
        _orderWorks.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task UpdateAsync(OrderWork entity)
    {
        var index = _orderWorks.FindIndex(o => o.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        _orderWorks[index] = entity;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var item = await GetAsync(id);
        return item != null && _orderWorks.Remove(item);
    }
}
