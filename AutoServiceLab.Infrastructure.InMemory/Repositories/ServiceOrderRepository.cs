using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class ServiceOrderRepository : IServiceOrderRepository
{
    private readonly List<ServiceOrder> _orders = new(AutoServiceData.ServiceOrders);

    /// <inheritdoc />
    public ServiceOrder? Get(int id) => _orders.FirstOrDefault(o => o.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<ServiceOrder> GetAll() => _orders;

    /// <inheritdoc />
    public void Add(ServiceOrder entity)
    {
        entity.Id = _orders.Count == 0 ? 1 : _orders.Max(o => o.Id) + 1;
        _orders.Add(entity);
    }

    /// <inheritdoc />
    public void Update(ServiceOrder entity)
    {
        var index = _orders.FindIndex(o => o.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        _orders[index] = entity;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var o = Get(id);
        return o != null && _orders.Remove(o);
    }
}
