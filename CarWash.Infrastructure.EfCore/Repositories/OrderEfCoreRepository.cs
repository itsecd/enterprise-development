using CarWash.Domain;
using CarWash.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core репозиторий для заказов
/// </summary>
public class OrderEfCoreRepository(CarWashDbContext context)
    : IRepository<Order, int>
{
    public async Task<Order> Create(Order entity)
    {
        entity.Id = 0;
        context.Orders.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> Delete(int entityId)
        => await context.Orders
            .Where(order => order.Id == entityId)
            .ExecuteDeleteAsync() > 0;

    public async Task<Order?> Read(int entityId)
        => await context.Orders
            .Include(order => order.Car)
            .Include(order => order.Procedure)
            .FirstOrDefaultAsync(order => order.Id == entityId);

    public async Task<IList<Order>> ReadAll()
        => await context.Orders
            .Include(order => order.Car)
            .Include(order => order.Procedure)
            .ToListAsync();

    public async Task<Order> Update(Order entity)
    {
        var existing = await context.Orders
            .FirstOrDefaultAsync(order => order.Id == entity.Id)
            ?? throw new KeyNotFoundException(
                $"Заказ с Id {entity.Id} не найден");

        context.Entry(existing).CurrentValues.SetValues(entity);

        await context.SaveChangesAsync();
        return existing;
    }
}