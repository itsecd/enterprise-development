using FoodDelivery.Application.Contracts.Restaurants;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Data;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Services;

/// <summary>
/// Проверяет прикладные операции с ресторанами
/// </summary>
public sealed class RestaurantServiceTests
{
    /// <summary>
    /// Проверяет обновление ресторана без потери ссылки из заказа
    /// </summary>
    [Fact]
    public async Task Update_PreservesRestaurantReferenceInOrder()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var data = new FoodDeliveryData();
        var repository = new RestaurantRepository(data);
        var service = new RestaurantService(repository);
        var order = data.Orders.First();
        var originalRestaurant = order.Restaurant;

        var updatedRestaurant = await service.UpdateAsync(
            originalRestaurant.Id,
            new RestaurantRequest
            {
                Name = "Обновлённый ресторан",
                Address = originalRestaurant.Address,
                Rating = originalRestaurant.Rating,
                OpeningTime = TimeOnly.FromTimeSpan(
                    originalRestaurant.OpeningTime),
                ClosingTime = TimeOnly.FromTimeSpan(
                    originalRestaurant.ClosingTime)
            }, cancellationToken);

        Assert.NotNull(updatedRestaurant);
        Assert.Same(originalRestaurant, order.Restaurant);
        Assert.Equal("Обновлённый ресторан", order.Restaurant.Name);
        Assert.Equal(order.Restaurant.Name, updatedRestaurant.Name);
    }

    /// <summary>
    /// Проверяет запрет удаления ресторана с заказами при отсутствии блюд
    /// </summary>
    [Fact]
    public async Task Delete_ThrowsConflictWhenRestaurantHasOrders()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var data = new FoodDeliveryData();
        var restaurant = data.Orders.First().Restaurant;

        data.Dishes.RemoveAll(
            dish => dish.Restaurant.Id == restaurant.Id);

        var repository = new RestaurantRepository(data);
        var service = new RestaurantService(repository);

        Assert.False(repository.HasDishes(restaurant.Id));
        Assert.True(repository.HasOrders(restaurant.Id));

        await Assert.ThrowsAsync<ResourceConflictException>(
            async () => await service.DeleteAsync(restaurant.Id, cancellationToken));

        Assert.Same(restaurant, repository.GetById(restaurant.Id));
    }
}
