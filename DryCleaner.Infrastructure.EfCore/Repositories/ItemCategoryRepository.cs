using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Repositories;
using DryCleaner.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EfCore.Repositories;

/// <summary>
/// Реализация репозитория категорий изделия с использованием EfCore
/// </summary>
public class ItemCategoryRepository(DryCleanerDbContext context) : IRepository<ItemCategory>
{
    /// <inheritdoc />
    public async Task<IList<ItemCategory>> GetAllAsync()
    {
        return await context.ItemCategories.AsNoTracking().ToListAsync();
    }

    /// <inheritdoc />
    public async Task<ItemCategory?> GetByIdAsync(int id)
    {
        return await context.ItemCategories.FindAsync(id);
    }

    /// <inheritdoc />
    public async Task<ItemCategory> AddAsync(ItemCategory entity)
    {
        context.ItemCategories.Add(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<ItemCategory?> UpdateAsync(ItemCategory entity)
    {
        var existing = await context.ItemCategories.FindAsync(entity.Id);
        if (existing == null) return null;

        context.Entry(existing).CurrentValues.SetValues(entity);
        await context.SaveChangesAsync();
        return existing;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await context.ItemCategories.FindAsync(id);
        if (entity == null) return false;

        try
        {
            context.ItemCategories.Remove(entity);
            await context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("Невозможно удалить категорию изделия, так как используется предмет с данной категорией");
        }
    }
}