using FoodDelivery.Application.Contracts.Orders;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Services;

/// <summary>
/// Проверяет прикладные операции с заказами
/// </summary>
public sealed class OrderServiceTests
{
    /// <summary>
    /// Проверяет создание заказа без позиций
    /// </summary>
    [Fact]
    public void Create_AssignsIdAndZeroTotal()
    {
        var data = new FoodDeliveryData();
        var service = CreateService(data);
        var request = CreateRequest(data.Orders.First());

        var created = service.Create(request);

        Assert.True(created.Id > 0);
        Assert.Equal(0m, created.TotalAmount);

        var stored = data.Orders.Single(order => order.Id == created.Id);

        Assert.Empty(stored.Items);
        Assert.Equal(request.CustomerId, stored.Customer.Id);
        Assert.Equal(request.RestaurantId, stored.Restaurant.Id);
    }

    /// <summary>
    /// Проверяет сохранение позиций и суммы при изменении клиента
    /// </summary>
    [Fact]
    public void Update_PreservesItemsAndReturnedTotal()
    {
        var data = new FoodDeliveryData();
        var service = CreateService(data);
        var order = data.Orders.First();
        var originalItems = order.Items;
        var originalTotal = order.TotalAmount;

        var otherCustomer = data.Customers
            .First(customer => customer.Id != order.Customer.Id);

        var updated = service.Update(
            order.Id,
            new OrderRequest
            {
                CustomerId = otherCustomer.Id,
                RestaurantId = order.Restaurant.Id,
                CreatedAt = order.CreatedAt,
                DeliveredAt = order.DeliveredAt
            });

        Assert.NotNull(updated);
        Assert.Same(originalItems, order.Items);
        Assert.Same(otherCustomer, order.Customer);
        Assert.Equal(originalTotal, order.TotalAmount);
        Assert.Equal(originalTotal, updated.TotalAmount);
    }

    /// <summary>
    /// Проверяет запрет смены ресторана заказа с позициями
    /// </summary>
    [Fact]
    public void Update_RejectsRestaurantChangeWhenItemsExist()
    {
        var data = new FoodDeliveryData();
        var service = CreateService(data);
        var order = data.Orders.First();
        var originalRestaurant = order.Restaurant;

        var otherRestaurant = data.Restaurants
            .First(restaurant => restaurant.Id != originalRestaurant.Id);

        Assert.Throws<ResourceConflictException>(() =>
            service.Update(order.Id, new OrderRequest
            {
                CustomerId = order.Customer.Id,
                RestaurantId = otherRestaurant.Id,
                CreatedAt = order.CreatedAt,
                DeliveredAt = order.DeliveredAt
            }));

        Assert.Same(originalRestaurant, order.Restaurant);
    }

    /// <summary>
    /// Проверяет запрет создания заказа с отсутствующей связью
    /// </summary>
    /// <param name="missingCustomer">Признак отсутствующего клиента</param>
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Create_RejectsMissingReference(bool missingCustomer)
    {
        var data = new FoodDeliveryData();
        var service = CreateService(data);
        var initialCount = data.Orders.Count;

        var request = new OrderRequest
        {
            CustomerId = missingCustomer
                ? int.MaxValue
                : data.Customers.First().Id,
            RestaurantId = missingCustomer
                ? data.Restaurants.First().Id
                : int.MaxValue,
            CreatedAt = new DateTime(2026, 9, 21, 12, 0, 0)
        };

        Assert.Throws<InvalidReferenceException>(
            () => service.Create(request));

        Assert.Equal(initialCount, data.Orders.Count);
    }

    /// <summary>
    /// Проверяет удаление заказа из связанных выборок
    /// </summary>
    [Fact]
    public void Delete_RemovesOrderFromRelatedQueries()
    {
        var data = new FoodDeliveryData();
        var service = CreateService(data);
        var created = service.Create(CreateRequest(data.Orders.First()));

        service.Delete(created.Id);
        service.Delete(created.Id);

        Assert.Null(service.GetById(created.Id));

        Assert.DoesNotContain(
            service.GetByCustomer(created.CustomerId),
            order => order.Id == created.Id);

        Assert.DoesNotContain(
            service.GetByRestaurant(created.RestaurantId),
            order => order.Id == created.Id);
    }

    private static OrderService CreateService(FoodDeliveryData data)
    {
        return new OrderService(
            new OrderRepository(data),
            new CustomerRepository(data),
            new RestaurantRepository(data));
    }

    private static OrderRequest CreateRequest(Order order)
    {
        return new OrderRequest
        {
            CustomerId = order.Customer.Id,
            RestaurantId = order.Restaurant.Id,
            CreatedAt = order.CreatedAt,
            DeliveredAt = null
        };
    }
}
