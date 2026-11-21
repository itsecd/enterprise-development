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
    public async Task TotalAmount_DoesNotChangeWhenDishPriceChanges()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
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
            new RestaurantRepository(data),
            new InMemoryOrderWriteTransaction(data));

        var updatedDish = await service.UpdateAsync(
            dish.Id,
            new DishRequest
            {
                Name = dish.Name,
                Weight = dish.Weight,
                Price = newPrice,
                CategoryId = dish.Category.Id,
                RestaurantId = dish.Restaurant.Id
            }, cancellationToken);

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
    /// Проверяет положительные и уникальные идентификаторы позиций заказов
    /// </summary>
    [Fact]
    public void Seed_AssignsUniqueItemIds()
    {
        var data = new FoodDeliveryData();
        var items = data.Orders.SelectMany(order => order.Items).ToArray();

        Assert.All(items, item => Assert.True(item.Id > 0));
        Assert.Equal(items.Length, items.Select(item => item.Id).Distinct().Count());
    }
}
