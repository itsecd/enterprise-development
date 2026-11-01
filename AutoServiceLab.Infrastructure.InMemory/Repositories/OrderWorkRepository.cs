using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class OrderWorkRepository : IOrderWorkRepository
{
    /// <inheritdoc />
    public OrderWork? Get(int id) => AutoServiceData.OrderWorks.FirstOrDefault(o => o.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<OrderWork> GetAll() => AutoServiceData.OrderWorks;

    /// <inheritdoc />
    public void Add(OrderWork entity)
    {
        entity.Id = AutoServiceData.OrderWorks.Count == 0 ? 1 : AutoServiceData.OrderWorks.Max(o => o.Id) + 1;
        AutoServiceData.OrderWorks.Add(entity);
    }

    /// <inheritdoc />
    public void Update(OrderWork entity)
    {
        var index = AutoServiceData.OrderWorks.FindIndex(o => o.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        AutoServiceData.OrderWorks[index] = entity;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var item = Get(id);
        return item != null && AutoServiceData.OrderWorks.Remove(item);
    }
}
