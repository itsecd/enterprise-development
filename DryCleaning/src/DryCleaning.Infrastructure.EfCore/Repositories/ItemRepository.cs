using DryCleaning.Domain;
using DryCleaning.Domain.Interfaces;
using DryCleaning.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace DryCleaning.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий изделий в базе данных
/// Изделия загружаются вместе с категорией
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class ItemRepository(DryCleaningDbContext context) : IItemRepository
{
    /// <summary>
    /// Загружает все изделия из базы вместе с их категориями
    /// </summary>
    /// <returns>Список изделий</returns>
    public async Task<IReadOnlyList<Item>> GetAllAsync()
    {
        return await context.Items
            .Include(i => i.Category)
            .ToListAsync();
    }

    /// <summary>
    /// Загружает изделия одной категории вместе с категорией
    /// </summary>
    /// <param name="categoryId">Идентификатор категории</param>
    /// <returns>Список изделий категории</returns>
    public async Task<IReadOnlyList<Item>> GetByCategoryIdAsync(int categoryId)
    {
        return await context.Items
            .Include(i => i.Category)
            .Where(i => i.Category.CategoryId == categoryId)
            .ToListAsync();
    }

    /// <summary>
    /// Ищет изделие по идентификатору вместе с категорией
    /// </summary>
    /// <param name="id">Идентификатор изделия</param>
    /// <returns>Изделие или null, если оно не найдено</returns>
    public async Task<Item?> GetByIdAsync(int id)
    {
        return await context.Items
            .Include(i => i.Category)
            .FirstOrDefaultAsync(i => i.ItemId == id);
    }

    /// <summary>
    /// Добавляет изделие в базу
    /// Идентификатор назначает база, внешний ключ категории EF берёт из item.Category
    /// </summary>
    /// <param name="item">Новое изделие с загруженной категорией</param>
    /// <returns>Изделие с назначенным идентификатором</returns>
    public async Task<Item> AddAsync(Item item)
    {
        context.Items.Add(item);
        await context.SaveChangesAsync();
        return item;
    }

    /// <summary>
    /// Загружает изделие из базы и сохраняет новые название, материал и категорию
    /// </summary>
    /// <param name="item">Изделие с идентификатором и новыми данными</param>
    /// <returns>True — изделие обновлено, False — изделие не найдено</returns>
    public async Task<bool> UpdateAsync(Item item)
    {
        var existing = await context.Items.FindAsync(item.ItemId);
        if (existing is null)
        {
            return false;
        }

        existing.Name = item.Name;
        existing.Material = item.Material;
        existing.Category = item.Category;
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удаляет изделие из базы по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор изделия</param>
    /// <returns>True — изделие удалено, False — изделие не найдено</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var item = await context.Items.FindAsync(id);
        if (item is null)
        {
            return false;
        }

        context.Items.Remove(item);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Проверяет, есть ли в категории изделия
    /// Выполняется как SQL EXISTS, сами изделия не загружаются
    /// </summary>
    /// <param name="categoryId">Идентификатор категории</param>
    /// <returns>True — изделия есть, False — изделий нет</returns>
    public async Task<bool> HasItemsForCategoryAsync(int categoryId)
    {
        return await context.Items.AnyAsync(i => i.Category.CategoryId == categoryId);
    }
}
