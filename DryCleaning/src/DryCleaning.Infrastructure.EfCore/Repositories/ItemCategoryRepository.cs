using DryCleaning.Domain;
using DryCleaning.Domain.Interfaces;
using DryCleaning.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace DryCleaning.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий категорий изделий в базе данных
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class ItemCategoryRepository(DryCleaningDbContext context) : IItemCategoryRepository
{
    /// <summary>
    /// Загружает все категории изделий из базы
    /// </summary>
    /// <returns>Список категорий</returns>
    public async Task<IReadOnlyList<ItemCategory>> GetAllAsync()
    {
        return await context.ItemCategories.ToListAsync();
    }

    /// <summary>
    /// Ищет категорию по первичному ключу
    /// Если категория уже загружена в контекст, запрос к базе не выполняется
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <returns>Категория или null, если она не найдена</returns>
    public async Task<ItemCategory?> GetByIdAsync(int id)
    {
        return await context.ItemCategories.FindAsync(id);
    }

    /// <summary>
    /// Добавляет категорию в базу
    /// Идентификатор назначает база при сохранении
    /// </summary>
    /// <param name="category">Новая категория</param>
    /// <returns>Категория с назначенным идентификатором</returns>
    public async Task<ItemCategory> AddAsync(ItemCategory category)
    {
        context.ItemCategories.Add(category);
        await context.SaveChangesAsync();
        return category;
    }

    /// <summary>
    /// Загружает категорию из базы и сохраняет новые название, вид обработки и цену
    /// </summary>
    /// <param name="category">Категория с идентификатором и новыми данными</param>
    /// <returns>True — категория обновлена, False — категория не найдена</returns>
    public async Task<bool> UpdateAsync(ItemCategory category)
    {
        var existing = await context.ItemCategories.FindAsync(category.CategoryId);
        if (existing is null)
        {
            return false;
        }

        existing.CategoryName = category.CategoryName;
        existing.RecommendedCleaningType = category.RecommendedCleaningType;
        existing.Price = category.Price;
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удаляет категорию из базы по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор категории</param>
    /// <returns>True — категория удалена, False — категория не найдена</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var category = await context.ItemCategories.FindAsync(id);
        if (category is null)
        {
            return false;
        }

        context.ItemCategories.Remove(category);
        await context.SaveChangesAsync();
        return true;
    }
}
