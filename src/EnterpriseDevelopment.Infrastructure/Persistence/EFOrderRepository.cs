using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseDevelopment.Infrastructure.Persistence;

/// <summary>
/// Репозиторий заказов на основе Entity Framework Core
/// </summary>
public class EfOrderRepository(ApplicationDbContext context)
    : IOrderRepository
{
    /// <summary>
    /// Получает все заказы вместе с блюдами
    /// </summary>
    public IEnumerable<Order> GetAll()
    {
        var orders = context.Orders.ToList();

        var items = context.OrderItems
            .Include(item => item.Dish)
            .ToList();

        foreach (var order in orders)
        {
            order.Dishes = items
                .Where(item => item.OrderId == order.Id)
                .SelectMany(item =>
                    Enumerable.Repeat(item.Dish, item.Quantity))
                .ToList();
        }

        return orders;
    }

    /// <summary>
    /// Получает заказ по идентификатору
    /// </summary>
    public Order? GetById(int id)
    {
        var order = context.Orders.Find(id);

        if (order is null)
        {
            return null;
        }

        order.Dishes = context.OrderItems
            .Where(item => item.OrderId == id)
            .Include(item => item.Dish)
            .AsEnumerable()
            .SelectMany(item =>
                Enumerable.Repeat(item.Dish, item.Quantity))
            .ToList();

        return order;
    }

    /// <summary>
    /// Добавляет заказ с позициями блюд
    /// </summary>
    public Order Add(Order order)
    {
        using var transaction = context.Database.BeginTransaction();

        context.Orders.Add(order);
        context.SaveChanges();

        var items = order.Dishes
            .GroupBy(dish => dish.Id)
            .Select(group => new OrderItem
            {
                OrderId = order.Id,
                DishId = group.Key,
                Quantity = group.Count()
            });

        context.OrderItems.AddRange(items);
        context.SaveChanges();

        transaction.Commit();

        return order;
    }

    /// <summary>
    /// Обновляет заказ и количество блюд
    /// </summary>
    public void Update(Order order)
    {
        using var transaction = context.Database.BeginTransaction();

        var quantities = order.Dishes
            .GroupBy(dish => dish.Id)
            .ToDictionary(
                group => group.Key,
                group => group.Count());

        var existingItems = context.OrderItems
            .Where(item => item.OrderId == order.Id)
            .ToList();

        foreach (var item in existingItems)
        {
            if (quantities.TryGetValue(item.DishId, out var quantity))
            {
                item.Quantity = quantity;
                quantities.Remove(item.DishId);
            }
            else
            {
                context.OrderItems.Remove(item);
            }
        }

        foreach (var pair in quantities)
        {
            context.OrderItems.Add(new OrderItem
            {
                OrderId = order.Id,
                DishId = pair.Key,
                Quantity = pair.Value
            });
        }

        context.SaveChanges();
        transaction.Commit();
    }

    /// <summary>
    /// Удаляет заказ и связанные позиции
    /// </summary>
    public void Delete(Order order)
    {
        using var transaction = context.Database.BeginTransaction();

        var items = context.OrderItems
            .Where(item => item.OrderId == order.Id)
            .ToList();

        context.OrderItems.RemoveRange(items);
        context.Orders.Remove(order);

        context.SaveChanges();
        transaction.Commit();
    }
}
