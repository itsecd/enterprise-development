using FoodDelivery.Tests.TestData;

namespace FoodDelivery.Tests;

public class AggregationTests
{
    private readonly FoodDeliveryTestData _data = new();

    /// <summary>
    /// Проверяет расчет статистики по категориям блюд за выбранный период
    /// </summary>
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public void GetCategorySummaryForPeriod_ShouldReturnExpectedValues(
        int periodNumber)
    {
        DateTime start;
        DateTime endExclusive;
        (int CategoryId, int OrderCount, decimal AverageOrderAmount, decimal TotalOrderAmount)[] expected;

        switch (periodNumber)
        {
            case 1:
                start = new DateTime(2026, 9, 1);
                endExclusive = new DateTime(2026, 9, 6);

                expected =
                [
                    (1, 3, 1250m, 3750m),
                    (2, 4, 1240m, 4960m),
                    (3, 2, 1230m, 2460m),
                    (5, 1, 970m, 970m),
                    (7, 2, 1210m, 2420m),
                    (8, 3, 1446.67m, 4340m)
                ];
                break;

            case 2:
                start = new DateTime(2026, 9, 6);
                endExclusive = new DateTime(2026, 9, 11);

                expected =
                [
                    (1, 1, 1840m, 1840m),
                    (2, 2, 1100m, 2200m),
                    (3, 1, 970m, 970m),
                    (5, 2, 1390m, 2780m),
                    (6, 2, 1095m, 2190m),
                    (7, 1, 1010m, 1010m),
                    (8, 7, 1152.86m, 8070m),
                    (9, 2, 1025m, 2050m),
                    (10, 1, 870m, 870m)
                ];
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(periodNumber));
        }

        var categoryOrders = _data.Orders
            .Where(order =>
                order.OrderTime >= start &&
                order.OrderTime < endExclusive)
            .SelectMany(order => order.Items
                .Select(item => item.Dish.CategoryId)
                .Distinct()
                .Select(categoryId => new
                {
                    Order = order,
                    CategoryId = categoryId
                }));

        var result = categoryOrders
            .GroupBy(item => item.CategoryId)
            .Select(group => new
            {
                CategoryId = group.Key,
                OrderCount = group.Count(),
                AverageOrderAmount = Math.Round(
                    group.Average(item => item.Order.TotalAmount),
                    2),
                TotalOrderAmount = group.Sum(item => item.Order.TotalAmount)
            })
            .OrderBy(item => item.CategoryId)
            .ToList();

        var actual = result.Select(item =>
            (
                item.CategoryId,
                item.OrderCount,
                item.AverageOrderAmount,
                item.TotalOrderAmount));

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Проверяет получение клиента с наибольшей общей суммой заказов
    /// </summary>
    [Fact]
    public void GetCustomerWithMaximumSpending_ShouldReturnExpectedCustomer()
    {
        var result = _data.Orders
            .GroupBy(order => order.CustomerId)
            .Select(group => new
            {
                Customer = _data.Customers.Single(
                    customer => customer.Id == group.Key),
                TotalSpent = group.Sum(order => order.TotalAmount)
            })
            .OrderByDescending(item => item.TotalSpent)
            .First();

        Assert.Equal(1, result.Customer.Id);
        Assert.Equal("Ivanov", result.Customer.LastName);
        Assert.Equal(6100m, result.TotalSpent);
    }
}