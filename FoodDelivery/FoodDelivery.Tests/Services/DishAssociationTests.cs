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
    public void Queries_ReflectUpdatedAndDeletedDish()
    {
        var data = new FoodDeliveryData();
        var service = CreateService(data);
        var firstCategory = data.Categories[0];
        var secondCategory = data.Categories[1];
        var firstRestaurant = data.Restaurants[0];
        var secondRestaurant = data.Restaurants[1];

        var created = service.Create(new DishRequest
        {
            Name = "Свободное тестовое блюдо",
            Weight = 250,
            Price = 300m,
            CategoryId = firstCategory.Id,
            RestaurantId = firstRestaurant.Id
        });

        Assert.Contains(
            service.GetByCategory(firstCategory.Id),
            dish => dish.Id == created.Id);

        Assert.Contains(
            service.GetByRestaurant(firstRestaurant.Id),
            dish => dish.Id == created.Id);

        var updated = service.Update(created.Id, new DishRequest
        {
            Name = created.Name,
            Weight = created.Weight,
            Price = created.Price,
            CategoryId = secondCategory.Id,
            RestaurantId = secondRestaurant.Id
        });

        Assert.NotNull(updated);

        Assert.DoesNotContain(
            service.GetByCategory(firstCategory.Id),
            dish => dish.Id == created.Id);

        Assert.DoesNotContain(
            service.GetByRestaurant(firstRestaurant.Id),
            dish => dish.Id == created.Id);

        Assert.Contains(
            service.GetByCategory(secondCategory.Id),
            dish => dish.Id == created.Id);

        Assert.Contains(
            service.GetByRestaurant(secondRestaurant.Id),
            dish => dish.Id == created.Id);

        service.Delete(created.Id);

        Assert.DoesNotContain(
            service.GetByCategory(secondCategory.Id),
            dish => dish.Id == created.Id);

        Assert.DoesNotContain(
            service.GetByRestaurant(secondRestaurant.Id),
            dish => dish.Id == created.Id);
    }

    /// <summary>
    /// Проверяет ошибку при отсутствии родительского ресурса
    /// </summary>
    /// <param name="category">Признак проверки категории</param>
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public void Queries_RejectMissingParent(bool category)
    {
        var data = new FoodDeliveryData();
        var service = CreateService(data);

        Assert.Throws<ResourceNotFoundException>(() =>
        {
            if (category)
            {
                service.GetByCategory(int.MaxValue);
            }
            else
            {
                service.GetByRestaurant(int.MaxValue);
            }
        });
    }

    private static DishService CreateService(FoodDeliveryData data)
    {
        return new DishService(
            new DishRepository(data),
            new CategoryRepository(data),
            new RestaurantRepository(data));
    }
}
