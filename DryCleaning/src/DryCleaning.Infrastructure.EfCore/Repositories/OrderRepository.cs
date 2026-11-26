using DryCleaning.Domain;
using DryCleaning.Domain.Interfaces;
using DryCleaning.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace DryCleaning.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий заказов в базе данных.
/// Заказы загружаются вместе с клиентом, изделием и категорией изделия
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class OrderRepository(DryCleaningDbContext context) : IOrderRepository
{
    /// <summary>
    /// Загружает все заказы из базы вместе с клиентами, изделиями и их категориями
    /// </summary>
    /// <returns>Список заказов</returns>
    public async Task<IReadOnlyList<Order>> GetAllAsync()
    {
        return await context.Orders
            .Include(o => o.Client)
            .Include(o => o.Item)
                .ThenInclude(i => i.Category)
            .ToListAsync();
    }

    /// <summary>
    /// Загружает заказы одного клиента вместе со связанными данными
    /// </summary>
    /// <param name="clientId">Идентификатор клиента</param>
    /// <returns>Список заказов клиента</returns>
    public async Task<IReadOnlyList<Order>> GetByClientIdAsync(int clientId)
    {
        return await context.Orders
            .Include(o => o.Client)
            .Include(o => o.Item)
                .ThenInclude(i => i.Category)
            .Where(o => o.ClientId == clientId)
            .ToListAsync();
    }

    /// <summary>
    /// Ищет заказ по идентификатору вместе с клиентом, изделием и категорией изделия
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <returns>Заказ или null, если он не найден</returns>
    public async Task<Order?> GetByIdAsync(int id)
    {
        return await context.Orders
            .Include(o => o.Client)
            .Include(o => o.Item)
                .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    /// <summary>
    /// Добавляет заказ в базу.
    /// Идентификатор назначает база, внешние ключи берутся из ClientId и ItemId
    /// </summary>
    /// <param name="order">Новый заказ с загруженными клиентом и изделием</param>
    /// <returns>Заказ с назначенным идентификатором</returns>
    public async Task<Order> AddAsync(Order order)
    {
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return order;
    }

    /// <summary>
    /// Загружает заказ из базы и сохраняет новые клиента, изделие, статус,
    /// срок обработки и даты
    /// </summary>
    /// <param name="order">Заказ с идентификатором и новыми данными</param>
    /// <returns>True — заказ обновлён, False — заказ не найден</returns>
    public async Task<bool> UpdateAsync(Order order)
    {
        var existing = await context.Orders.FindAsync(order.Id);
        if (existing is null)
        {
            return false;
        }

        existing.ClientId = order.ClientId;
        existing.ItemId = order.ItemId;
        existing.Status = order.Status;
        existing.ProcessingDays = order.ProcessingDays;
        existing.AcceptDate = order.AcceptDate;
        existing.CompletedDate = order.CompletedDate;
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Удаляет заказ из базы по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор заказа</param>
    /// <returns>True — заказ удалён, False — заказ не найден</returns>
    public async Task<bool> DeleteAsync(int id)
    {
        var order = await context.Orders.FindAsync(id);
        if (order is null)
        {
            return false;
        }

        context.Orders.Remove(order);
        await context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Проверяет, есть ли у клиента заказы.
    /// Выполняется как SQL EXISTS, сами заказы не загружаются
    /// </summary>
    /// <param name="clientId">Идентификатор клиента</param>
    /// <returns>True — заказы есть, False — заказов нет</returns>
    public async Task<bool> HasOrdersForClientAsync(int clientId)
    {
        return await context.Orders.AnyAsync(o => o.ClientId == clientId);
    }

    /// <summary>
    /// Проверяет, есть ли заказы с изделием.
    /// Выполняется как SQL EXISTS, сами заказы не загружаются
    /// </summary>
    /// <param name="itemId">Идентификатор изделия</param>
    /// <returns>True — заказы есть, False — заказов нет</returns>
    public async Task<bool> HasOrdersForItemAsync(int itemId)
    {
        return await context.Orders.AnyAsync(o => o.ItemId == itemId);
    }
}
