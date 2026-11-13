using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.EfCore.Repositories;

/// <summary>
/// Реализует репозиторий категорий с использованием efcore
/// </summary>
public class CategoryRepository(
    FoodDeliveryDbContext dbContext)
    : ICategoryRepository
{
    /// <inheritdoc />
    public async Task<IList<Category>> GetAllAsync()
    {
        return await dbContext.Categories
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Category?> GetByIdAsync(int id)
    {
        return await dbContext.Categories
            .FirstOrDefaultAsync(category => category.Id == id);
    }

    /// <inheritdoc />
    public async Task<Category> AddAsync(Category entity)
    {
        await dbContext.Categories.AddAsync(entity);
        await dbContext.SaveChangesAsync();

        return entity;
    }

    /// <inheritdoc />
    public async Task<Category?> UpdateAsync(Category entity)
    {
        var existingCategory = await dbContext.Categories
            .FirstOrDefaultAsync(category => category.Id == entity.Id);

        if (existingCategory is null)
        {
            return null;
        }

        existingCategory.Name = entity.Name;

        await dbContext.SaveChangesAsync();

        return existingCategory;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var category = await dbContext.Categories
            .FirstOrDefaultAsync(category => category.Id == id);

        if (category is null)
        {
            return false;
        }

        dbContext.Categories.Remove(category);

        await dbContext.SaveChangesAsync();

        return true;
    }
}