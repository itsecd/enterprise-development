using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Tests;

/// <summary>
/// Тесты к данным службы доставки еды
/// </summary>
public class FoodDeliveryTests
{
    /// <summary>
    /// Поиск отмененных заказов
    /// </summary>
    [Fact]
    public void GetCancelledOrders()
    {
        const int expectedCancelledOrderId = 7;

        var cancelledOrders = FoodDeliveryData.Orders
        .Where(order => order.Status == OrderStatus.Cancelled)
        .ToList();

        Assert.Single(cancelledOrders);
        Assert.Equal(expectedCancelledOrderId, cancelledOrders.First().Id);
    }

    /// <summary>
    /// Вывод информации о клиентах заказавших блюда в указанном ресторане
    /// </summary>
    [Fact]
    public void GetCustomersByRestaurantOrderedByFullName()
    {
        const int restaurantId = 1;
        var expectedCustomerNames = new[] { "Иванов А.", "Иванов Б." };

        var customers = FoodDeliveryData.Orders
            .Where(order => order.RestaurantId == restaurantId)
            .Select(order => order.Customer!.FullName)
            .Distinct()
            .OrderBy(fullName => fullName)
            .ToList();

        Assert.Equal(expectedCustomerNames, customers);
    }

    /// <summary>
    /// Вывод топ 5 популярных блюд среди доставленных заказов
    /// </summary>
    [Fact]
    public void GetTopFiveMostOrderedDishesInDeliveredOrders()
    {
        const string expectedTopDishName = "Филадельфия";
        const int expectedTopQuantity = 4;

        var topDishes = FoodDeliveryData.OrderItems
            .Where(item => item.Order!.Status == OrderStatus.Delivered)
            .GroupBy(item => item.Dish!.Name)
            .Select(group => new { DishName = group.Key, TotalQuantity = group.Sum(item => item.Quantity) })
            .OrderByDescending(dish => dish.TotalQuantity)
            .ThenBy(dish => dish.DishName)
            .Take(5)
            .ToList();

        var topDish = topDishes.First();
        Assert.Equal(expectedTopDishName, topDish.DishName);
        Assert.Equal(expectedTopQuantity, topDish.TotalQuantity);
    }

    /// <summary>
    /// Вывод информации о заказах курьера за заданный период
    /// </summary>
    [Fact]
    public void GetCourierOrdersForPeriod()
    {
        const int courierId = 1;
        var from = new DateTime(2024, 5, 1);
        var to = new DateTime(2024, 5, 31);
        var expectedOrderIds = new[] { 1, 4, 14 };

        var orders = FoodDeliveryData.Orders
            .Where(order => order.CourierId == courierId && order.CreatedAt >= from && order.CreatedAt <= to)
            .OrderBy(order => order.CreatedAt)
            .ToList();

        Assert.Equal(expectedOrderIds, orders.Select(order => order.Id));
    }

    /// <summary>
    /// Вывести количество заказов доставляемых каждым транспортом
    /// </summary>
    [Fact]
    public void GetOrderCountByTransportType()
    {
        var expectedTransportStats = new Dictionary<TransportType, int>
        {
            [TransportType.Bicycle] = 4,
            [TransportType.Car] = 4,
            [TransportType.Foot] = 2,
            [TransportType.Scooter] = 2
        };

        var transportStats = FoodDeliveryData.Orders
            .Where(order => order.Courier != null)
            .GroupBy(order => order.Courier!.Transport)
            .ToDictionary(group => group.Key, group => group.Count());

        Assert.Equal(expectedTransportStats, transportStats);
    }

    /// <summary>
    /// Вывести топ 5 ресторанов по количеству заказов
    /// </summary>
    [Fact]
    public void GetTop5RestaurantsByOrderCount()
    {
        var expectedRestaurantNames = new[]
        {
            "Пиццерия Pizza Tower",
            "Суши-бар DANGANRONPA",
            "Бургерная Machine Party",
            "Кафе YAPYAP",
            "Кофейня Leblanc"
        };

        var topRestaurants = FoodDeliveryData.Orders
            .GroupBy(order => order.RestaurantId)
            .Select(group => new
            {
                RestaurantId = group.Key,
                RestaurantName = group.First().Restaurant!.Name,
                OrderCount = group.Count()
            })
            .OrderByDescending(x => x.OrderCount)
            .ThenBy(x => x.RestaurantId)
            .Take(5)
            .Select(x => x.RestaurantName)
            .ToList();

        Assert.Equal(expectedRestaurantNames, topRestaurants);
    }

    /// <summary>
    /// Вывести список заказов с минимальным временем доставки
    /// </summary>
    [Fact]
    public void GetOrdersWithMinDeliveryTime()
    {
        const int expectedOrderId = 2;
        var expectedMinDuration = TimeSpan.FromMinutes(20);

        var deliveredOrders = FoodDeliveryData.Orders
            .Where(order => order.Status == OrderStatus.Delivered && order.DeliveredAt.HasValue);

        var minDeliveryDuration = deliveredOrders
            .Min(order => order.DeliveredAt!.Value - order.CreatedAt);

        var ordersWithMinDelivery = deliveredOrders
            .Where(order => (order.DeliveredAt!.Value - order.CreatedAt) == minDeliveryDuration)
            .ToList();

        Assert.Single(ordersWithMinDelivery);
        Assert.Equal(expectedOrderId, ordersWithMinDelivery.First().Id);
        Assert.Equal(expectedMinDuration, ordersWithMinDelivery.First().DeliveryDuration);
    }

    /// <summary>
    /// Вывести сводную информацию о заказах по каждой категории блюд за указанный период
    /// </summary>
    [Fact]
    public void GetOrderSummaryByCategoryForPeriod()
    {
        var periodStart = new DateTime(2024, 5, 1, 0, 0, 0);
        var periodEnd = new DateTime(2024, 5, 31, 23, 59, 59);
        const string targetCategory = "Пицца";
        const int expectedOrderCount = 5;
        const decimal expectedTotalAmount = 4420;
        const decimal expectedAverageAmount = 884;

        var categorySummaries = FoodDeliveryData.Categories
            .Select(category =>
            {
                var ordersInPeriod = FoodDeliveryData.Orders
                    .Where(order => order.CreatedAt >= periodStart && order.CreatedAt <= periodEnd
                                 && order.Items.Any(item => item.Dish != null && item.Dish.CategoryId == category.Id))
                    .ToList();

                var orderCount = ordersInPeriod.Count;
                var totalAmount = ordersInPeriod.Sum(order => order.Total);
                var averageAmount = orderCount > 0 ? ordersInPeriod.Average(order => order.Total) : 0;

                return new
                {
                    CategoryName = category.Name,
                    OrderCount = orderCount,
                    TotalAmount = totalAmount,
                    AverageAmount = Math.Round(averageAmount, 2)
                };
            })
            .ToDictionary(x => x.CategoryName);

        var pizza = categorySummaries[targetCategory];
        Assert.Equal(expectedOrderCount, pizza.OrderCount);
        Assert.Equal(expectedTotalAmount, pizza.TotalAmount);
        Assert.Equal(expectedAverageAmount, pizza.AverageAmount);
    }

    /// <summary>
    /// Вывести информацию о клиенте который потратил на доставку наибольшую сумму за все время работы приложения
    /// </summary>
    [Fact]
    public void GetCustomerWithMaxSpendOnDelivery()
    {
        const int expectedCustomerId = 3;
        const string expectedCustomerName = "Иванов В.";
        const decimal expectedTotalSpent = 5930;

        var topCustomer = FoodDeliveryData.Orders
            .GroupBy(order => order.Customer!)
            .Select(group => new
            {
                Customer = group.Key,
                TotalSpent = group.Sum(order => order.Total)
            })
            .OrderByDescending(x => x.TotalSpent)
            .First();

        Assert.Equal(expectedCustomerId, topCustomer.Customer.Id);
        Assert.Equal(expectedCustomerName, topCustomer.Customer.FullName);
        Assert.Equal(expectedTotalSpent, topCustomer.TotalSpent);
    }
}