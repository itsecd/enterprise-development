using FoodDelivery.Application.Contracts.Orders;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Services;

/// <summary>
/// Проверяет операции с позициями заказа
/// </summary>
public sealed class OrderItemServiceTests
{
    /// <summary>
    /// Проверяет сохранение цены и пересчёт суммы при изменении количества
    /// </summary>
    [Fact]
    public void Update_UsesSavedPrice()
    {
        var data = new FoodDeliveryData();
        var dish = data.Dishes.First();
        var repository = new OrderRepository(data);
        var order = CreateOrder(repository, data, dish);
        var service = new OrderItemService(repository, new DishRepository(data));
        var originalPrice = dish.Price;

        var item = service.Create(order.Id, new AddOrderItemRequest
        {
            DishId = dish.Id,
            Quantity = 2
        });

        Assert.Equal(originalPrice, item.UnitPrice);
        Assert.Equal(originalPrice * 2, order.TotalAmount);

        dish.Price += 100m;

        var updated = service.Update(
            order.Id,
            item.Id,
            new UpdateOrderItemRequest { Quantity = 3 });

        Assert.Equal(originalPrice, updated.UnitPrice);
        Assert.Equal(originalPrice * 3, updated.TotalAmount);
        Assert.Equal(updated.TotalAmount, order.TotalAmount);
    }

    /// <summary>
    /// Проверяет запрет добавления блюда другого ресторана
    /// </summary>
    [Fact]
    public void Create_RejectsDishFromAnotherRestaurant()
    {
        var data = new FoodDeliveryData();
        var dish = data.Dishes.First();
        var repository = new OrderRepository(data);
        var order = CreateOrder(repository, data, dish);
        var service = new OrderItemService(repository, new DishRepository(data));

        var otherDish = data.Dishes
            .First(item => item.Restaurant.Id != order.Restaurant.Id);

        Assert.Throws<ResourceConflictException>(() =>
            service.Create(order.Id, new AddOrderItemRequest
            {
                DishId = otherDish.Id,
                Quantity = 1
            }));

        Assert.Empty(order.Items);
    }

    /// <summary>
    /// Проверяет отсутствие повторного использования номера удалённой позиции
    /// </summary>
    [Fact]
    public void Create_DoesNotReuseDeletedItemId()
    {
        var data = new FoodDeliveryData();
        var dish = data.Dishes.First();
        var repository = new OrderRepository(data);
        var order = CreateOrder(repository, data, dish);
        var service = new OrderItemService(repository, new DishRepository(data));

        var request = new AddOrderItemRequest
        {
            DishId = dish.Id,
            Quantity = 1
        };

        var first = service.Create(order.Id, request);

        service.Delete(order.Id, first.Id);
        service.Delete(order.Id, first.Id);

        Assert.Empty(order.Items);
        Assert.Equal(0m, order.TotalAmount);

        var second = service.Create(order.Id, request);

        Assert.True(second.Id > first.Id);
        Assert.Single(order.Items);
    }

    private static Order CreateOrder(
        OrderRepository repository,
        FoodDeliveryData data,
        Dish dish)
    {
        return repository.Add(new Order
        {
            Id = 0,
            Customer = data.Customers.First(),
            Restaurant = dish.Restaurant,
            CreatedAt = new DateTime(2026, 9, 21, 12, 0, 0),
            DeliveredAt = null,
            Items = []
        });
    }
}
