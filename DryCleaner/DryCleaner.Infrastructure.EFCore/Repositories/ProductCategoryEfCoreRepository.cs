using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using DryCleaner.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core-реализация репозитория <see cref="IRepository{TEntity, TKey}"/>
/// для работы с сущностью <see cref="ProductCategory"/>.
/// </summary>
/// <remarks>
/// Использует <see cref="DryCleanerDbContext"/> для доступа к таблице категорий товаров.
/// </remarks>
public class ProductCategoryEfCoreRepository(DryCleanerDbContext context) : IRepository<ProductCategory, int>
{
    /// <summary>
    /// Создаёт новую категорию товаров в базе данных.
    /// </summary>
    /// <param name="entity">Экземпляр категории товаров для добавления.</param>
    /// <returns>Добавленный экземпляр категории товаров.</returns>
    public async Task<ProductCategory> Create(ProductCategory entity)
    {
        await context.ProductCategories.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Удаляет категорию товаров по её идентификатору.
    /// </summary>
    /// <param name="entityId">Идентификатор категории товаров для удаления.</param>
    /// <returns>
    /// <see langword="true"/>, если категория найдена и удалена;
    /// иначе <see langword="false"/>.
    /// </returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.ProductCategories
            .FirstOrDefaultAsync(category => category.Id == entityId);

        if (entity == null)
            return false;

        context.ProductCategories.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Возвращает категорию товаров по её идентификатору.
    /// </summary>
    /// <param name="entityId">Идентификатор искомой категории товаров.</param>
    /// <returns>
    /// Найденная категория товаров или <see langword="null"/>, если категория не найдена.
    /// </returns>
    public async Task<ProductCategory?> Read(int entityId) => await context.ProductCategories.FirstOrDefaultAsync(category => category.Id == entityId);

    /// <summary>
    /// Возвращает список всех категорий товаров.
    /// </summary>
    /// <returns>Список всех категорий товаров из базы данных.</returns>
    public async Task<IList<ProductCategory>> ReadAll() => await context.ProductCategories.ToListAsync();

    /// <summary>
    /// Обновляет категорию товара.
    /// </summary>
    /// <param name="entity">Сущность категории с обновлёнными данными.</param>
    /// <returns>Обновлённая сущность категории.</returns>
    public async Task<ProductCategory> Update(ProductCategory entity)
    {
        var existingEntity = await context.Set<ProductCategory>().FindAsync(entity.Id);

        if (existingEntity is null)
            throw new KeyNotFoundException(
                $"ProductCategory with id {entity.Id} not found.");

        context.Set<ProductCategory>().Update(entity);
        await context.SaveChangesAsync();

        return entity;
    }
}