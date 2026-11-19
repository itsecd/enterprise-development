using AutoService.Domain.Data;
using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;

namespace AutoService.Infrastructure.InMemory;

/// <summary>
/// Реализация репозитория заказов в памяти.
/// </summary>
public class InMemoryOrderRepository : IOrderRepository
{
    private readonly List<Order> _orders = DataSeed.Orders.ToList();

    /// <summary>
    /// Получить список всех заказов.
    /// </summary>
    public Task<IEnumerable<Order>> GetAllAsync() => Task.FromResult(_orders.AsEnumerable());

    /// <summary>
    /// Получить заказ по идентификатору.
    /// </summary>
    public Task<Order?> GetByIdAsync(Guid id) => Task.FromResult(_orders.FirstOrDefault(o => o.Id == id));

    /// <summary>
    /// Добавить заказ.
    /// </summary>
    public Task AddAsync(Order order)
    {
        _orders.Add(order);
        return Task.CompletedTask;
    }

    /// <summary>
    /// Обновить данные заказа.
    /// </summary>
    public Task UpdateAsync(Order order)
    {
        var index = _orders.FindIndex(o => o.Id == order.Id);
        if (index != -1) _orders[index] = order;
        return Task.CompletedTask;
    }

    /// <summary>
    /// Удалить заказ.
    /// </summary>
    public Task DeleteAsync(Order order)
    {
        _orders.Remove(order);
        return Task.CompletedTask;
    }
}