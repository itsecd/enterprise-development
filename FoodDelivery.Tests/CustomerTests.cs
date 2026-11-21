using FoodDelivery.Domain.Data;

namespace FoodDelivery.Tests;

/// <summary>
/// Тесты для проверки аналитики по клиентам
/// </summary>
public class CustomerTests
{
    /// <summary>
    /// Задание: Вывести сведения обо всех клиентах, заказывавших в выбранном ресторане, упорядочить по ФИО.
    /// </summary>
    [Fact]
    public void CustomersWhoOrderedFromSelectedRestaurant()
    {
        var selectedRestaurantId = 1;

        var expected = new[] { 2, 1 };

        var customers = TestData.Orders
            .Where(order => order.RestaurantId == selectedRestaurantId)
            .Select(order => order.Customer)
            .DistinctBy(customer => customer.Id)
            .OrderBy(customer => customer.LastName)
            .ThenBy(customer => customer.FirstName)
            .ThenBy(customer => customer.Patronymic)
            .Select(customer => customer.Id)
            .ToList();

        Assert.Equal(expected, customers);
    }

    /// <summary>
    /// Задание: Вывести информацию о клиенте, который потратил на доставку наибольшую сумму за все время работы приложения.
    /// </summary>
    [Fact]
    public void CustomerWithMaxTotalOrderAmount()
    {
        var expectedCustomerId = 2;
        var expectedTotalAmount = 2050m;

        var customer = TestData.Orders
            .GroupBy(order => order.CustomerId)

            .Select(group => new
            {
                CustomerId = group.Key,
                TotalAmount = group.Sum(order => order.TotalAmount)
            })

            .OrderByDescending(item => item.TotalAmount)
            .First();

        Assert.Equal(
            expectedCustomerId,
            customer.CustomerId);

        Assert.Equal(
            expectedTotalAmount,
            customer.TotalAmount);
    }
}