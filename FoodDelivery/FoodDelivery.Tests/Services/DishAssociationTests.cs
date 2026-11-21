using FoodDelivery.Application.Contracts.Dishes;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Data;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Services;

/// <summary>
/// Проверяет получение блюд связанных ресурсов
/// </summary>
public sealed class DishAssociationTests
{
    /// <summary>
    /// Проверяет актуальность выборок после смены связей и удаления блюда
    /// </summary>
    [Fact]
    public async Task Queries_ReflectUpdatedAndDeletedDish()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var data = new FoodDeliveryData();
        var service = CreateService(data);
        var firstCategory = data.Categories[0];
        var secondCategory = data.Categories[1];
        var firstRestaurant = data.Restaurants[0];
        var secondRestaurant = data.Restaurants[1];

        var created = await service.CreateAsync(new DishRequest
        {
            Name = "Свободное тестовое блюдо",
            Weight = 250,
            Price = 300m,
            CategoryId = firstCategory.Id,
            RestaurantId = firstRestaurant.Id
        }, cancellationToken);

        Assert.Contains(
            await service.GetByCategoryAsync(firstCategory.Id, cancellationToken),
            dish => dish.Id == created.Id);

        Assert.Contains(
            await service.GetByRestaurantAsync(firstRestaurant.Id, cancellationToken),
            dish => dish.Id == created.Id);

        var updated = await service.UpdateAsync(created.Id, new DishRequest
        {
            Name = created.Name,
            Weight = created.Weight,
            Price = created.Price,
            CategoryId = secondCategory.Id,
            RestaurantId = secondRestaurant.Id
        }, cancellationToken);

        Assert.NotNull(updated);

        Assert.DoesNotContain(
            await service.GetByCategoryAsync(firstCategory.Id, cancellationToken),
            dish => dish.Id == created.Id);

        Assert.DoesNotContain(
            await service.GetByRestaurantAsync(firstRestaurant.Id, cancellationToken),
            dish => dish.Id == created.Id);

        Assert.Contains(
            await service.GetByCategoryAsync(secondCategory.Id, cancellationToken),
            dish => dish.Id == created.Id);

        Assert.Contains(
            await service.GetByRestaurantAsync(secondRestaurant.Id, cancellationToken),
            dish => dish.Id == created.Id);

        await service.DeleteAsync(created.Id, cancellationToken);

        Assert.DoesNotContain(
            await service.GetByCategoryAsync(secondCategory.Id, cancellationToken),
            dish => dish.Id == created.Id);

        Assert.DoesNotContain(
            await service.GetByRestaurantAsync(secondRestaurant.Id, cancellationToken),
            dish => dish.Id == created.Id);
    }

    /// <summary>
    /// Проверяет ошибку при отсутствии родительского ресурса
    /// </summary>
    /// <param name="category">Признак проверки категории</param>
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Queries_RejectMissingParent(bool category)
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var data = new FoodDeliveryData();
        var service = CreateService(data);

        await Assert.ThrowsAsync<ResourceNotFoundException>(async () =>
        {
            if (category)
            {
                await service.GetByCategoryAsync(int.MaxValue, cancellationToken);
            }
            else
            {
                await service.GetByRestaurantAsync(int.MaxValue, cancellationToken);
            }
        });
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
