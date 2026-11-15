using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AutoServiceLab.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий связей заказов и работ (EF Core)
/// </summary>
public class OrderWorkEfCoreRepository(AutoServiceDbContext context) : IOrderWorkRepository
{
    /// <inheritdoc />
    public OrderWork? Get(int id) => context.OrderWorks
        .Include(ow => ow.WorkType)
        .FirstOrDefault(ow => ow.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<OrderWork> GetAll() => context.OrderWorks
        .Include(ow => ow.WorkType)
        .ToList();

    /// <inheritdoc />
    public void Add(OrderWork entity)
    {
        context.OrderWorks.Add(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public void Update(OrderWork entity)
    {
        context.OrderWorks.Update(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var entity = Get(id);
        if (entity == null) return false;

        context.OrderWorks.Remove(entity);
        context.SaveChanges();
        return true;
    }
}
