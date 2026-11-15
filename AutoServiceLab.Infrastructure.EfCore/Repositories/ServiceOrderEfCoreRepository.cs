using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoServiceLab.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий заказов (EF Core)
/// </summary>
public class ServiceOrderEfCoreRepository(AutoServiceDbContext context) : IServiceOrderRepository
{
    /// <inheritdoc />
    public ServiceOrder? Get(int id) => context.ServiceOrders
        .Include(o => o.Client)
        .Include(o => o.Car)
        .FirstOrDefault(o => o.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<ServiceOrder> GetAll() => context.ServiceOrders
        .Include(o => o.Client)
        .Include(o => o.Car)
        .ToList();

    /// <inheritdoc />
    public void Add(ServiceOrder entity)
    {
        context.ServiceOrders.Add(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public void Update(ServiceOrder entity)
    {
        context.ServiceOrders.Update(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var order = Get(id);
        if (order == null) return false;

        context.ServiceOrders.Remove(order);
        context.SaveChanges();
        return true;
    }
}
