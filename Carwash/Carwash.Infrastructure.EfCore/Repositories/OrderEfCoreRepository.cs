using Microsoft.EntityFrameworkCore;
using Carwash.Domain;
using Carwash.Domain.Entities;

namespace Carwash.Infrastructure.EfCore.Repositories;

/// <summary>
/// Имплементация репозитория для работы с заказами с использованием Entity Framework Core
/// </summary>
/// <param name="context">Контекст БД</param>
public class OrderEfCoreRepository(CarwashDbContext context) : IRepository<Order, int>
{
    /// <inheritdoc/>
    public async Task<Order> Create(Order entity)
    {
        var result = await context.Orders.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int entityId)
    {
        var result = await context.Orders.FirstOrDefaultAsync(o => o.Id == entityId);
        if (result == null)
            return false;

        context.Orders.Remove(result);
        await context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public async Task<Order?> Read(int entityId) => await context.Orders.FirstOrDefaultAsync(o => o.Id == entityId);

    /// <inheritdoc/>
    public async Task<List<Order>> ReadAll() => await context.Orders.ToListAsync();

    /// <inheritdoc/>
    public async Task<Order> Update(Order entity)
    {
        context.Orders.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
