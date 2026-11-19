using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoService.Infrastructure.Persistence.Repositories;

/// <summary>
/// Реализация репозитория заказов на Entity Framework Core.
/// </summary>
public class EfOrderRepository(AutoServiceDbContext context) : IOrderRepository
{
    /// <summary>
    /// Получить список всех заказов.
    /// </summary>
    public async Task<IEnumerable<Order>> GetAllAsync() =>
        await context.Orders.Include(o => o.Works).ToListAsync();

    /// <summary>
    /// Получить заказ по его идентификатору.
    /// </summary>
    public async Task<Order?> GetByIdAsync(Guid id) =>
        await context.Orders.Include(o => o.Works).FirstOrDefaultAsync(o => o.Id == id);

    /// <summary>
    /// Добавить новый заказ.
    /// </summary>
    public async Task AddAsync(Order order)
    {
        foreach (var work in order.Works)
            context.WorkTypes.Attach(work);

        context.Orders.Add(order);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Обновить данные существующего заказа.
    /// </summary>
    public async Task UpdateAsync(Order order)
    {
        var existing = await context.Orders.Include(o => o.Works).FirstOrDefaultAsync(o => o.Id == order.Id);
        if (existing is null) return;

        existing.CarId = order.CarId;
        existing.ClientId = order.ClientId;
        existing.MechanicId = order.MechanicId;
        existing.AcceptedAt = order.AcceptedAt;
        existing.IssuedAt = order.IssuedAt;
        existing.Works = order.Works;

        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Удалить заказ.
    /// </summary>
    public async Task DeleteAsync(Order order)
    {
        context.Orders.Remove(order);
        await context.SaveChangesAsync();
    }
}
