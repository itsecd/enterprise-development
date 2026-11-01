using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class OrderMechanicRepository : IOrderMechanicRepository
{
    /// <inheritdoc />
    public OrderMechanic? Get(int id) => AutoServiceData.OrderMechanics.FirstOrDefault(o => o.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<OrderMechanic> GetAll() => AutoServiceData.OrderMechanics;

    /// <inheritdoc />
    public void Add(OrderMechanic entity)
    {
        entity.Id = AutoServiceData.OrderMechanics.Count == 0 ? 1 : AutoServiceData.OrderMechanics.Max(o => o.Id) + 1;
        AutoServiceData.OrderMechanics.Add(entity);
    }

    /// <inheritdoc />
    public void Update(OrderMechanic entity)
    {
        var index = AutoServiceData.OrderMechanics.FindIndex(o => o.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        AutoServiceData.OrderMechanics[index] = entity;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var item = Get(id);
        return item != null && AutoServiceData.OrderMechanics.Remove(item);
    }
}
