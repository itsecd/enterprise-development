using FoodDelivery.Application.Contracts.Dishes;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Data;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Services;

/// <summary>
/// Проверяет прикладные операции с блюдами
/// </summary>
public sealed class DishServiceTests
{
    /// <summary>
    /// Проверяет отказ в создании блюда с отсутствующей связанной сущностью
    /// </summary>
    /// <param name="missingCategory">Признак проверки отсутствующей категории</param>
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Create_RejectsMissingReference(bool missingCategory)
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var data = new FoodDeliveryData();
        var service = CreateService(data);
        var initialCount = data.Dishes.Count;

        var request = new DishRequest
        {
            Name = "Новое блюдо",
            Weight = 250,
            Price = 300m,
            CategoryId = missingCategory
                ? int.MaxValue
                : data.Categories.First().Id,
            RestaurantId = missingCategory
                ? data.Restaurants.First().Id
                : int.MaxValue
        };

        await Assert.ThrowsAsync<InvalidReferenceException>(
            async () => await service.CreateAsync(request, cancellationToken));

        Assert.Equal(initialCount, data.Dishes.Count);
    }

    /// <summary>
    /// Проверяет запрет удаления блюда из заказа
    /// </summary>
    [Fact]
    public async Task Delete_RejectsDishUsedInOrder()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var data = new FoodDeliveryData();
        var service = CreateService(data);
        var dish = data.Orders
            .SelectMany(order => order.Items)
            .First()
            .Dish;

        await Assert.ThrowsAsync<ResourceConflictException>(
            async () => await service.DeleteAsync(dish.Id, cancellationToken));

        Assert.Contains(data.Dishes, item => ReferenceEquals(item, dish));
    }

    /// <summary>
    /// Проверяет запрет смены ресторана блюда из заказа
    /// </summary>
    [Fact]
    public async Task Update_RejectsRestaurantChangeForOrderedDish()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var data = new FoodDeliveryData();
        var service = CreateService(data);
        var dish = data.Orders
            .SelectMany(order => order.Items)
            .First()
            .Dish;
        var originalRestaurant = dish.Restaurant;

        var otherRestaurant = data.Restaurants
            .First(restaurant => restaurant.Id != originalRestaurant.Id);

        var request = new DishRequest
        {
            Name = dish.Name,
            Weight = dish.Weight,
            Price = dish.Price,
            CategoryId = dish.Category.Id,
            RestaurantId = otherRestaurant.Id
        };

        await Assert.ThrowsAsync<ResourceConflictException>(
            async () => await service.UpdateAsync(dish.Id, request, cancellationToken));

        Assert.Same(originalRestaurant, dish.Restaurant);
    }

    private static DishService CreateService(FoodDeliveryData data)
    {
        return new DishService(
            new DishRepository(data),
            new CategoryRepository(data),
            new RestaurantRepository(data),
            new InMemoryOrderWriteTransaction(data));
    }
}
