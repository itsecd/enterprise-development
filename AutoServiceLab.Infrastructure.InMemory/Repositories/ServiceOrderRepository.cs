using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class ServiceOrderRepository : IServiceOrderRepository
{
    /// <inheritdoc />
    public ServiceOrder? Get(int id) => AutoServiceData.ServiceOrders.FirstOrDefault(o => o.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<ServiceOrder> GetAll() => AutoServiceData.ServiceOrders;

    /// <inheritdoc />
    public void Add(ServiceOrder entity)
    {
        entity.Id = AutoServiceData.ServiceOrders.Count == 0 ? 1 : AutoServiceData.ServiceOrders.Max(o => o.Id) + 1;
        AutoServiceData.ServiceOrders.Add(entity);
    }

    /// <inheritdoc />
    public void Update(ServiceOrder entity)
    {
        var index = AutoServiceData.ServiceOrders.FindIndex(o => o.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        AutoServiceData.ServiceOrders[index] = entity;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var o = Get(id);
        return o != null && AutoServiceData.ServiceOrders.Remove(o);
    }
}
