using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Repositories;

/// <summary>
/// Проверяет хранение блюд в памяти
/// </summary>
public sealed class DishRepositoryTests
{
    /// <summary>
    /// Проверяет сохранение ссылки из позиции заказа при обновлении блюда
    /// </summary>
    [Fact]
    public void Update_PreservesDishReferenceInOrderItem()
    {
        var data = new FoodDeliveryData();
        var repository = new DishRepository(data);
        var orderItem = data.Orders
            .SelectMany(order => order.Items)
            .First();
        var originalDish = orderItem.Dish;

        var updated = repository.Update(new Dish
        {
            Id = originalDish.Id,
            Name = "Обновлённое блюдо",
            Weight = originalDish.Weight,
            Price = originalDish.Price,
            Category = originalDish.Category,
            Restaurant = originalDish.Restaurant
        });

        Assert.True(updated);
        Assert.Same(originalDish, orderItem.Dish);
        Assert.Same(originalDish, repository.GetById(originalDish.Id));
        Assert.Equal("Обновлённое блюдо", orderItem.Dish.Name);
    }

    /// <summary>
    /// Проверяет актуальность связи блюда с категорией после обновления
    /// </summary>
    [Fact]
    public void Update_ChangesCategoryAssociation()
    {
        var data = new FoodDeliveryData();
        var categoryRepository = new CategoryRepository(data);
        var dishRepository = new DishRepository(data);

        var firstCategory = categoryRepository.Add(new Category
        {
            Id = 0,
            Name = "Первая тестовая категория"
        });

        var secondCategory = categoryRepository.Add(new Category
        {
            Id = 0,
            Name = "Вторая тестовая категория"
        });

        var dish = dishRepository.Add(new Dish
        {
            Id = 0,
            Name = "Тестовое блюдо",
            Weight = 250,
            Price = 300m,
            Category = firstCategory,
            Restaurant = data.Restaurants.First()
        });

        Assert.True(categoryRepository.HasDishes(firstCategory.Id));
        Assert.False(categoryRepository.HasDishes(secondCategory.Id));

        var updated = dishRepository.Update(new Dish
        {
            Id = dish.Id,
            Name = dish.Name,
            Weight = dish.Weight,
            Price = dish.Price,
            Category = secondCategory,
            Restaurant = dish.Restaurant
        });

        Assert.True(updated);
        Assert.False(categoryRepository.HasDishes(firstCategory.Id));
        Assert.True(categoryRepository.HasDishes(secondCategory.Id));
        Assert.Same(secondCategory, dish.Category);
    }
}
