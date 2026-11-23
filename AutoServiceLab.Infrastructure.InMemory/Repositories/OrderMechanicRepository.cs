using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class OrderMechanicRepository : IOrderMechanicRepository
{
    private readonly List<OrderMechanic> _orderMechanics = new(AutoServiceData.OrderMechanics);

    /// <inheritdoc />
    public OrderMechanic? Get(int id) => _orderMechanics.FirstOrDefault(o => o.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<OrderMechanic> GetAll() => _orderMechanics;

    /// <inheritdoc />
    public void Add(OrderMechanic entity)
    {
        entity.Id = _orderMechanics.Count == 0 ? 1 : _orderMechanics.Max(o => o.Id) + 1;
        _orderMechanics.Add(entity);
    }

    /// <inheritdoc />
    public void Update(OrderMechanic entity)
    {
        var index = _orderMechanics.FindIndex(o => o.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        _orderMechanics[index] = entity;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var item = Get(id);
        return item != null && _orderMechanics.Remove(item);
    }
}
