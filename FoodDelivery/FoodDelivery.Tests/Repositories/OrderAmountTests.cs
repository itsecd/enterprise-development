using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Exceptions;
using FoodDelivery.Domain.Models;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Repositories;

/// <summary>
/// Проверяет сохранение состояния при переполнении стоимости
/// </summary>
public sealed class OrderAmountTests
{
    /// <summary>
    /// Проверяет отказ добавления без изменения заказа
    /// </summary>
    [Fact]
    public void AddItem_OverflowDoesNotChangeOrder()
    {
        var data = new FoodDeliveryData();
        var repository = new OrderRepository(data);
        var order = data.Orders.First();
        var originalCount = order.Items.Count;
        var originalTotal = order.TotalAmount;

        var item = new OrderItem
        {
            Id = 0,
            Dish = order.Items.First().Dish,
            UnitPrice = 40000000000000000000000000000m,
            Quantity = 2
        };

        Assert.Throws<OrderAmountException>(
            () => repository.AddItem(order.Id, item));

        Assert.Equal(originalCount, order.Items.Count);
        Assert.Equal(originalTotal, repository.GetById(order.Id)!.TotalAmount);
        Assert.NotNull(
            new AnalyticsRepository(data).GetTopSpendingCustomer());
    }

    /// <summary>
    /// Проверяет отказ изменения количества без изменения позиции
    /// </summary>
    [Fact]
    public void UpdateItem_OverflowDoesNotChangeQuantity()
    {
        var data = new FoodDeliveryData();
        data.Orders.Clear();

        var repository = new OrderRepository(data);
        var order = CreateOrder(repository, data);

        var item = repository.AddItem(order.Id, new OrderItem
        {
            Id = 0,
            Dish = data.Dishes.First(),
            UnitPrice = 40000000000000000000000000000m,
            Quantity = 1
        })!;

        var originalTotal = order.TotalAmount;

        Assert.Throws<OrderAmountException>(
            () => repository.UpdateItem(order.Id, item.Id, 2));

        Assert.Equal(1, item.Quantity);
        Assert.Equal(originalTotal, order.TotalAmount);
    }

    /// <summary>
    /// Проверяет защиту общей суммы заказов от переполнения
    /// </summary>
    [Fact]
    public void AddItem_RejectsAggregateOverflow()
    {
        var data = new FoodDeliveryData();
        data.Orders.Clear();

        var repository = new OrderRepository(data);
        var firstOrder = CreateOrder(repository, data);
        var secondOrder = CreateOrder(repository, data);

        var item = new OrderItem
        {
            Id = 0,
            Dish = data.Dishes.First(),
            UnitPrice = 50000000000000000000000000000m,
            Quantity = 1
        };

        repository.AddItem(firstOrder.Id, item);

        Assert.Throws<OrderAmountException>(
            () => repository.AddItem(secondOrder.Id, item));

        Assert.Empty(secondOrder.Items);

        var customer = new AnalyticsRepository(data)
            .GetTopSpendingCustomer();

        Assert.NotNull(customer);
        Assert.Equal(item.UnitPrice, customer.TotalAmount);
    }

    private static Order CreateOrder(
        OrderRepository repository,
        FoodDeliveryData data)
    {
        return repository.Add(new Order
        {
            Id = 0,
            Customer = data.Customers.First(),
            Restaurant = data.Restaurants.First(),
            CreatedAt = new DateTime(2026, 9, 21, 12, 0, 0),
            DeliveredAt = null,
            Items = []
        });
    }
}
