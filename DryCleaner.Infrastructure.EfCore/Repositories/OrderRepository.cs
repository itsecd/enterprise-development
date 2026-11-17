using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Repositories;
using DryCleaner.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EfCore.Repositories;

public class OrderRepository(DryCleanerDbContext context) : IRepository<Order>
{
    public async Task<IList<Order>> GetAllAsync()
    {
        return await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Item)
                .ThenInclude(i => i.Category)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await context.Orders
            .Include(o => o.Customer)
            .Include(o => o.Item)
                .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order> AddAsync(Order entity)
    {
        context.Orders.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<Order?> UpdateAsync(Order entity)
    {
        var existing = await context.Orders.FindAsync(entity.Id);
        if (existing == null) return null;

        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Orders.FindAsync(id);
        if (entity == null) return false;

        context.Orders.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}