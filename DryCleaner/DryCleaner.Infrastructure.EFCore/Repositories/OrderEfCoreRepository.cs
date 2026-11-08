using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;
using DryCleaner.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace DryCleaner.Infrastructure.EfCore.Repositories;

/// <summary>
/// EF Core-реализация репозитория <see cref="IRepository{TEntity, TKey}"/>
/// для работы с сущностью <see cref="Order"/>.
/// </summary>
/// <remarks>
/// Использует <see cref="DryCleanerDbContext"/> для доступа к таблице заказов.
/// </remarks>
public class OrderEfCoreRepository(DryCleanerDbContext context) : IRepository<Order, int>
{
    /// <summary>
    /// Создаёт новый заказ в базе данных.
    /// </summary>
    /// <param name="entity">Экземпляр заказа для добавления.</param>
    /// <returns>Добавленный экземпляр заказа.</returns>
    public async Task<Order> Create(Order entity)
    {
        await context.Orders.AddAsync(entity);
        await context.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Удаляет заказ по его идентификатору.
    /// </summary>
    /// <param name="entityId">Идентификатор заказа для удаления.</param>
    /// <returns>
    /// <see langword="true"/>, если заказ найден и удалён;
    /// иначе <see langword="false"/>.
    /// </returns>
    public async Task<bool> Delete(int entityId)
    {
        var entity = await context.Orders
            .FirstOrDefaultAsync(order => order.Id == entityId);

        if (entity == null)
            return false;

        context.Orders.Remove(entity);
        await context.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Возвращает заказ по его идентификатору.
    /// </summary>
    /// <param name="entityId">Идентификатор искомого заказа.</param>
    /// <returns>
    /// Найденный заказ или <see langword="null"/>, если заказ не найден.
    /// </returns>
    public async Task<Order?> Read(int entityId)
    {
        return await context.Orders
             .Include(o => o.Client)
             .Include(o => o.Product).ThenInclude(p => p.Category)
             .FirstOrDefaultAsync(order => order.Id == entityId);
    }

    /// <summary>
    /// Возвращает список всех заказов.
    /// </summary>
    /// <returns>Список всех заказов из базы данных.</returns>
    public async Task<IList<Order>> ReadAll()
    {
        return await context.Orders.Include(o => o.Client)
            .Include(o => o.Product).ThenInclude(p => p.Category)
            .ToListAsync();
    }

    /// <summary>
    /// Обновляет данные существующего заказа.
    /// </summary>
    /// <param name="entity">Экземпляр заказа с обновлёнными данными.</param>
    /// <returns>Актуальный экземпляр заказа после сохранения изменений.</returns>
    public async Task<Order> Update(Order entity)
    {
        context.Orders.Update(entity);
        await context.SaveChangesAsync();

        return (await Read(entity.Id))!;
    }
}