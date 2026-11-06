using FoodDelivery.Application.Contracts.Dishes;
using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Data;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Models;

/// <summary>
/// Проверяет расчёт суммы заказа по сохранённым ценам
/// </summary>
public sealed class OrderPriceTests
{
    /// <summary>
    /// Проверяет сохранение суммы заказа после изменения цены блюда
    /// </summary>
    [Fact]
    public void TotalAmount_DoesNotChangeWhenDishPriceChanges()
    {
        var data = new FoodDeliveryData();
        var order = data.Orders.First();
        var item = order.Items.First();
        var dish = item.Dish;
        var originalTotal = order.TotalAmount;
        var originalUnitPrice = item.UnitPrice;
        var newPrice = dish.Price + 100m;

        var service = new DishService(
            new DishRepository(data),
            new CategoryRepository(data),
            new RestaurantRepository(data));

        var updatedDish = service.Update(
            dish.Id,
            new DishRequest
            {
                Name = dish.Name,
                Weight = dish.Weight,
                Price = newPrice,
                CategoryId = dish.Category.Id,
                RestaurantId = dish.Restaurant.Id
            });

        Assert.NotNull(updatedDish);
        Assert.Equal(newPrice, dish.Price);
        Assert.Equal(originalUnitPrice, item.UnitPrice);
        Assert.Equal(originalTotal, order.TotalAmount);
    }

    /// <summary>
    /// Проверяет пересчёт суммы по сохранённой цене при изменении количества
    /// </summary>
    [Fact]
    public void TotalAmount_UsesSavedPriceWhenQuantityChanges()
    {
        var data = new FoodDeliveryData();
        var order = data.Orders.First();
        var item = order.Items.First();
        var originalTotal = order.TotalAmount;
        var originalUnitPrice = item.UnitPrice;

        item.Dish.Price += 100m;
        item.Quantity += 1;

        Assert.Equal(
            originalTotal + originalUnitPrice,
            order.TotalAmount);
    }

    /// <summary>
    /// Проверяет положительные и уникальные идентификаторы позиций внутри заказа
    /// </summary>
    [Fact]
    public void Seed_AssignsUniqueItemIdsWithinEachOrder()
    {
        var data = new FoodDeliveryData();

        Assert.All(data.Orders, order =>
        {
            Assert.All(
                order.Items,
                item => Assert.True(item.Id > 0));

            Assert.Equal(
                order.Items.Count,
                order.Items.Select(item => item.Id).Distinct().Count());
        });
    }
}
