using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using DryCleaner.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core-реализация репозитория <see cref="IRepository{TEntity, TKey}"/>
/// для работы с сущностью <see cref="Product"/>.
/// </summary>
/// <remarks>
/// Использует <see cref="DryCleanerDbContext"/> для доступа к таблице товаров.
/// </remarks>
public class ProductEfCoreRepository(DryCleanerDbContext context) : IRepository<Product, int>
{
    /// <summary>
    /// Создаёт новый товар в базе данных.
    /// </summary>
    /// <param name="entity">Экземпляр товара для добавления.</param>
    /// <returns>Добавленный экземпляр товара.</returns>
    public async Task<Product> Create(Product entity)
    {
        await context.Products.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Удаляет товар по его идентификатору.
    /// </summary>
    /// <param name="entityId">Идентификатор товара для удаления.</param>
    /// <returns>
    /// <see langword="true"/>, если товар найден и удалён;
    /// иначе <see langword="false"/>.
    /// </returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Products
            .FirstOrDefaultAsync(product => product.Id == entityId);

        if (entity == null)
            return false;

        context.Products.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Возвращает товар по его идентификатору.
    /// </summary>
    /// <param name="entityId">Идентификатор искомого товара.</param>
    /// <returns>
    /// Найденный товар или <see langword="null"/>, если товар не найден.
    /// </returns>
    public async Task<Product?> Read(int entityId)
    {
        return await context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(product => product.Id == entityId);
    }

    /// <summary>
    /// Возвращает список всех товаров.
    /// </summary>
    /// <returns>Список всех товаров из базы данных.</returns>
    public async Task<IList<Product>> ReadAll()
    {
        return await context.Products
            .Include(p => p.Category)
            .ToListAsync();
    }

    /// <summary>
    /// Обновляет данные существующего товара.
    /// </summary>
    /// <param name="entity">Экземпляр товара с обновлёнными данными.</param>
    /// <returns>Актуальный экземпляр товара после сохранения изменений.</returns>
    public async Task<Product> Update(Product entity)
    {
        context.Products.Update(entity);
        await context.SaveChangesAsync();

        return (await Read(entity.Id))!;
    }
}