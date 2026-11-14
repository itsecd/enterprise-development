using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class OrderWorkRepository : IOrderWorkRepository
{
    private readonly List<OrderWork> _orderWorks = AutoServiceData.OrderWorks;

    /// <inheritdoc />
    public OrderWork? Get(int id) => _orderWorks.FirstOrDefault(o => o.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<OrderWork> GetAll() => _orderWorks;

    /// <inheritdoc />
    public void Add(OrderWork entity)
    {
        entity.Id = _orderWorks.Count == 0 ? 1 : _orderWorks.Max(o => o.Id) + 1;
        _orderWorks.Add(entity);
    }

    /// <inheritdoc />
    public void Update(OrderWork entity)
    {
        var index = _orderWorks.FindIndex(o => o.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        _orderWorks[index] = entity;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var item = Get(id);
        return item != null && _orderWorks.Remove(item);
    }
}
