using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Repositories;
using DryCleaner.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EfCore.Repositories;

/// <summary>
/// Реализация репозитория изделий с использованием EfCore
/// </summary>
public class ItemRepository(DryCleanerDbContext context) : IRepository<Item>
{
    /// <inheritdoc />
    public async Task<IList<Item>> GetAllAsync()
    {
        return await context.Items.Include(i => i.Category).AsNoTracking().ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Item?> GetByIdAsync(int id)
    {
        return await context.Items.Include(i => i.Category).FirstOrDefaultAsync(i => i.Id == id);
    }

    /// <inheritdoc />
    public async Task<Item> AddAsync(Item entity)
    {
        context.Items.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<Item?> UpdateAsync(Item entity)
    {
        var existing = await context.Items
            .Include(i => i.Category)
            .FirstOrDefaultAsync(i => i.Id == entity.Id);

        if (existing == null) return null;

        existing.Name = entity.Name;
        existing.CategoryId = entity.CategoryId;
        existing.Material = entity.Material;

        if (entity.Category != null)
        {
            existing.Category = entity.Category;
        }

        await context.SaveChangesAsync();
        return existing;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.Items.FindAsync(id);
        if (entity == null) return false;

        try
        {
            context.Items.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("Невозможно удалить предмет, так как он используется в заказе");
        }
    }
}