using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Analytics;

/// <summary>
/// Проверяет граничные сценарии аналитических запросов
/// </summary>
public sealed class AnalyticsBoundaryTests
{
    /// <summary>
    /// Проверяет включение обеих границ и исключение соседних моментов
    /// </summary>
    [Fact]
    public void CategoryStatistics_IncludesExactBoundaries()
    {
        var data = new FoodDeliveryData();
        data.Orders.Clear();

        var start = new DateTime(2026, 9, 1, 0, 0, 0);
        var end = new DateTime(2026, 9, 20, 12, 0, 0);
        var dish = data.Dishes.First();

        foreach (var instant in new[]
        {
            start.AddTicks(-1),
            start,
            end,
            end.AddTicks(1)
        })
        {
            var order = CreateOrder(data, dish, 100m);
            order.CreatedAt = instant;
            data.Orders.Add(order);
        }

        var result = new AnalyticsRepository(data)
            .GetCategoryStatistics(start, end)
            .Single(item => item.Category.Id == dish.Category.Id);

        Assert.Equal(2, result.OrderCount);
        Assert.Equal(200m, result.TotalAmount);
        Assert.Equal(100m, result.AverageAmount);
    }

    /// <summary>
    /// Проверяет включение заказа двадцатого сентября при явном времени окончания
    /// </summary>
    [Fact]
    public void CategoryStatistics_IncludesSeptember20Order()
    {
        var data = new FoodDeliveryData();

        var result = new AnalyticsRepository(data)
            .GetCategoryStatistics(
                new DateTime(2026, 9, 1, 0, 0, 0),
                new DateTime(2026, 9, 20, 23, 59, 59))
            .Single(item => item.Category.Id == 1);

        Assert.Equal(4, result.OrderCount);
        Assert.Equal(4400m, result.TotalAmount);
        Assert.Equal(1100m, result.AverageAmount);
    }

    /// <summary>
    /// Проверяет отсутствие доставленных заказов и смешанный набор доставок
    /// </summary>
    [Fact]
    public void MinimumDelivery_IgnoresUndeliveredOrders()
    {
        var data = new FoodDeliveryData();

        foreach (var order in data.Orders)
        {
            order.DeliveredAt = null;
        }

        var repository = new AnalyticsRepository(data);

        Assert.Empty(repository.GetOrdersWithMinimumDeliveryTime());

        var deliveredOrder = data.Orders.First();
        deliveredOrder.DeliveredAt = deliveredOrder.CreatedAt.AddMinutes(10);

        var result = repository.GetOrdersWithMinimumDeliveryTime();

        Assert.Equal(deliveredOrder.Id, Assert.Single(result).Id);
    }

    /// <summary>
    /// Проверяет учёт полной суммы смешанного заказа в каждой его категории
    /// </summary>
    [Fact]
    public void CategoryStatistics_CountsWholeMixedOrder()
    {
        var data = new FoodDeliveryData();
        data.Orders.Clear();

        var firstDish = data.Dishes.First();

        var secondCategory = data.Categories
            .First(category => category.Id != firstDish.Category.Id);

        var secondDish = new Dish
        {
            Id = data.Dishes.Max(dish => dish.Id) + 1,
            Name = "Блюдо другой категории",
            Weight = 200,
            Price = 200m,
            Category = secondCategory,
            Restaurant = firstDish.Restaurant
        };

        data.Dishes.Add(secondDish);

        var order = CreateOrder(data, firstDish, 100m);

        order.Items.Add(new OrderItem
        {
            Id = 2,
            Dish = secondDish,
            UnitPrice = 200m,
            Quantity = 1
        });

        data.Orders.Add(order);

        var statistics = new AnalyticsRepository(data)
            .GetCategoryStatistics(
                new DateTime(2026, 9, 1),
                new DateTime(2026, 9, 30));

        foreach (var categoryId in new[]
        {
            firstDish.Category.Id,
            secondDish.Category.Id
        })
        {
            var result = statistics
                .Single(item => item.Category.Id == categoryId);

            Assert.Equal(1, result.OrderCount);
            Assert.Equal(300m, result.TotalAmount);
            Assert.Equal(300m, result.AverageAmount);
        }
    }

    /// <summary>
    /// Проверяет группировку разных объектов клиента с одним идентификатором
    /// </summary>
    [Fact]
    public void CustomerSpending_GroupsById()
    {
        var data = new FoodDeliveryData();
        data.Orders.Clear();

        var dish = data.Dishes.First();
        var firstOrder = CreateOrder(data, dish, 100m);
        var secondOrder = CreateOrder(data, dish, 200m);
        var customer = firstOrder.Customer;

        secondOrder.Customer = new Customer
        {
            Id = customer.Id,
            LastName = customer.LastName,
            FirstName = customer.FirstName,
            Patronymic = customer.Patronymic,
            Phone = customer.Phone,
            DeliveryAddress = customer.DeliveryAddress
        };

        data.Orders.AddRange([firstOrder, secondOrder]);

        var result = new AnalyticsRepository(data)
            .GetTopSpendingCustomer();

        Assert.NotNull(result);
        Assert.Equal(customer.Id, result.Customer.Id);
        Assert.Equal(300m, result.TotalAmount);
    }

    private static Order CreateOrder(
        FoodDeliveryData data,
        Dish dish,
        decimal unitPrice)
    {
        return new Order
        {
            Id = data.Orders.Count + 1,
            Customer = data.Customers.First(),
            Restaurant = dish.Restaurant,
            CreatedAt = new DateTime(2026, 9, 10, 12, 0, 0),
            DeliveredAt = null,
            Items =
            [
                new OrderItem
                {
                    Id = 1,
                    Dish = dish,
                    UnitPrice = unitPrice,
                    Quantity = 1
                }
            ]
        };
    }
}
