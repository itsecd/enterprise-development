using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Repositories;

/// <summary>
/// Проверяет хранение заказов в памяти
/// </summary>
public sealed class OrderRepositoryTests
{
    /// <summary>
    /// Проверяет сохранение позиций при изменении метаданных заказа
    /// </summary>
    [Fact]
    public void Update_PreservesItemsAndTotalAmount()
    {
        var data = new FoodDeliveryData();
        var repository = new OrderRepository(data);
        var order = data.Orders.First();
        var originalItems = order.Items;
        var originalTotal = order.TotalAmount;

        var newCustomer = data.Customers
            .First(customer => customer.Id != order.Customer.Id);

        var updated = repository.Update(new Order
        {
            Id = order.Id,
            Customer = newCustomer,
            Restaurant = order.Restaurant,
            CreatedAt = order.CreatedAt,
            DeliveredAt = order.DeliveredAt,
            Items = []
        });

        Assert.True(updated);
        Assert.Same(order, repository.GetById(order.Id));
        Assert.Same(newCustomer, order.Customer);
        Assert.Same(originalItems, order.Items);
        Assert.NotEmpty(order.Items);
        Assert.Equal(originalTotal, order.TotalAmount);
    }

    /// <summary>
    /// Проверяет актуальность выборок после добавления и удаления заказа
    /// </summary>
    [Fact]
    public void RelatedQueries_ReflectAddedAndDeletedOrder()
    {
        var data = new FoodDeliveryData();
        var repository = new OrderRepository(data);
        var customer = data.Customers.First();
        var restaurant = data.Restaurants.First();

        var initialCustomerCount = repository
            .GetByCustomer(customer.Id)
            .Count;

        var initialRestaurantCount = repository
            .GetByRestaurant(restaurant.Id)
            .Count;

        var order = repository.Add(new Order
        {
            Id = 0,
            Customer = customer,
            Restaurant = restaurant,
            CreatedAt = new DateTime(2026, 9, 21, 12, 0, 0),
            DeliveredAt = null,
            Items = []
        });

        Assert.Contains(
            repository.GetByCustomer(customer.Id),
            item => ReferenceEquals(item, order));

        Assert.Contains(
            repository.GetByRestaurant(restaurant.Id),
            item => ReferenceEquals(item, order));

        Assert.Equal(
            initialCustomerCount + 1,
            repository.GetByCustomer(customer.Id).Count);

        Assert.Equal(
            initialRestaurantCount + 1,
            repository.GetByRestaurant(restaurant.Id).Count);

        repository.Delete(order.Id);

        Assert.DoesNotContain(
            repository.GetByCustomer(customer.Id),
            item => item.Id == order.Id);

        Assert.DoesNotContain(
            repository.GetByRestaurant(restaurant.Id),
            item => item.Id == order.Id);

        Assert.Equal(
            initialCustomerCount,
            repository.GetByCustomer(customer.Id).Count);

        Assert.Equal(
            initialRestaurantCount,
            repository.GetByRestaurant(restaurant.Id).Count);
    }
}
