using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Repositories;
using DryCleaner.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EfCore.Repositories;

public class ItemCategoryRepository(DryCleanerDbContext context) : IRepository<ItemCategory>
{
    public async Task<IList<ItemCategory>> GetAllAsync()
    {
        return await context.ItemCategories.AsNoTracking().ToListAsync();
    }

    public async Task<ItemCategory?> GetByIdAsync(int id)
    {
        return await context.ItemCategories.FindAsync(id);
    }

    public async Task<ItemCategory> AddAsync(ItemCategory entity)
    {
        context.ItemCategories.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<ItemCategory?> UpdateAsync(ItemCategory entity)
    {
        var existing = await context.ItemCategories.FindAsync(entity.Id);
        if (existing == null) return null;

        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.ItemCategories.FindAsync(id);
        if (entity == null) return false;

        context.ItemCategories.Remove(entity);
        await context.SaveChangesAsync();
        return true;
    }
}