using FoodDelivery.Application.Contracts.Dishes;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Repositories;

/// <summary>
/// Проверяет согласованность связанных операций хранения
/// </summary>
public sealed class StorageConcurrencyTests
{
    /// <summary>
    /// Проверяет сохранение ссылочной целостности при создании блюда и удалении категории
    /// </summary>
    [Fact]
    public async Task RelatedOperations_PreserveCategoryReference()
    {
        var data = new FoodDeliveryData();
        var categories = new CategoryRepository(data);
        var dishes = new DishRepository(data);
        var categoryService = new CategoryService(categories);

        var dishService = new DishService(
            dishes,
            categories,
            new RestaurantRepository(data));

        var restaurantId = data.Restaurants.First().Id;

        var category = categories.Add(new Category
        {
            Id = 0,
            Name = "Тестовая категория"
        });

        var create = Task.Run(() =>
        {
            try
            {
                dishService.Create(new DishRequest
                {
                    Name = "Тестовое блюдо",
                    Weight = 100,
                    Price = 100m,
                    CategoryId = category.Id,
                    RestaurantId = restaurantId
                });
            }
            catch (InvalidReferenceException)
            {
            }
        });

        var delete = Task.Run(() =>
        {
            try
            {
                categoryService.Delete(category.Id);
            }
            catch (ResourceConflictException)
            {
            }
        });

        await Task.WhenAll(create, delete);

        categories.Execute(() =>
        {
            foreach (var dish in dishes.GetAll())
            {
                Assert.NotNull(categories.GetById(dish.Category.Id));
            }

            if (categories.GetById(category.Id) is null)
            {
                Assert.DoesNotContain(
                    dishes.GetAll(),
                    dish => dish.Category.Id == category.Id);
            }
            else
            {
                Assert.Contains(
                    dishes.GetAll(),
                    dish => dish.Category.Id == category.Id);
            }
        });
    }
}
