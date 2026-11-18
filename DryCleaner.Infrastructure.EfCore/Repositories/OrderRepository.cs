using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Repositories;
using DryCleaner.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EfCore.Repositories;

/// <summary>
/// Реализация репозитория заказов с использованием EfCore
/// </summary>
public class OrderRepository(DryCleanerDbContext context) : IRepository<Order>
{
    /// <inheritdoc />
    public async Task<IList<Order>> GetAllAsync()
    {
        return await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Item)
                .ThenInclude(i => i.Category)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Order?> GetByIdAsync(int id)
    {
        return await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Item)
                .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    /// <inheritdoc />
    public async Task<Order> AddAsync(Order entity)
    {
        context.Orders.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<Order?> UpdateAsync(Order entity)
    {
        var existing = await context.Orders.FindAsync(entity.Id);
        if (existing == null) return null;

        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Orders.FindAsync(id);
        if (entity == null) return false;

        context.Orders.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}