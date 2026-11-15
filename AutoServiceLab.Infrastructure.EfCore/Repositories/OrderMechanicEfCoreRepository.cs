using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoServiceLab.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий связей заказов и механиков (EF Core)
/// </summary>
public class OrderMechanicEfCoreRepository(AutoServiceDbContext context) : IOrderMechanicRepository
{
    /// <inheritdoc />
    public OrderMechanic? Get(int id) => context.OrderMechanics
        .Include(om => om.Mechanic)
        .FirstOrDefault(om => om.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<OrderMechanic> GetAll() => context.OrderMechanics
        .Include(om => om.Mechanic)
        .ToList();

    /// <inheritdoc />
    public void Add(OrderMechanic entity)
    {
        context.OrderMechanics.Add(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public void Update(OrderMechanic entity)
    {
        context.OrderMechanics.Update(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var entity = Get(id);
        if (entity == null) return false;

        context.OrderMechanics.Remove(entity);
        context.SaveChanges();
        return true;
    }
}