using FoodDelivery.Domain.Data;
using FoodDelivery.Domain.Models;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Repositories;

/// <summary>
/// Проверяет хранение категорий в памяти
/// </summary>
public sealed class CategoryRepositoryTests
{
    /// <summary>
    /// Проверяет сохранение ссылок на категорию при изменении названия
    /// </summary>
    [Fact]
    public void Update_PreservesCategoryReference()
    {
        var data = new FoodDeliveryData();
        var repository = new CategoryRepository(data);
        var dish = data.Dishes.First();
        var originalCategory = dish.Category;

        var updatedCategory = new Category
        {
            Id = originalCategory.Id,
            Name = "Обновлённая категория"
        };

        var updated = repository.Update(updatedCategory);
        var storedCategory = repository.GetById(originalCategory.Id);

        Assert.True(updated);
        Assert.Same(originalCategory, storedCategory);
        Assert.Same(storedCategory, dish.Category);
        Assert.Equal(updatedCategory.Name, dish.Category.Name);
    }

    /// <summary>
    /// Проверяет назначение новых идентификаторов после удаления категории
    /// </summary>
    [Fact]
    public void Add_DoesNotReuseLastDeletedId()
    {
        var data = new FoodDeliveryData();
        var repository = new CategoryRepository(data);
        var initialMaximumId = data.Categories.Max(category => category.Id);

        var firstCategory = repository.Add(new Category
        {
            Id = 0,
            Name = "Первая новая категория"
        });

        repository.Delete(firstCategory.Id);

        var secondCategory = repository.Add(new Category
        {
            Id = 0,
            Name = "Вторая новая категория"
        });

        Assert.Equal(initialMaximumId + 1, firstCategory.Id);
        Assert.Equal(initialMaximumId + 2, secondCategory.Id);
        Assert.Null(repository.GetById(firstCategory.Id));
        Assert.Same(secondCategory, repository.GetById(secondCategory.Id));
    }

    /// <summary>
    /// Проверяет отсутствие изменений при обновлении несуществующей категории
    /// </summary>
    [Fact]
    public void Update_ReturnsFalseWhenCategoryDoesNotExist()
    {
        var data = new FoodDeliveryData();
        var repository = new CategoryRepository(data);
        var initialCategories = repository.GetAll()
            .Select(category => (category.Id, category.Name))
            .ToArray();

        var missingId = data.Categories.Max(category => category.Id) + 1;

        var updated = repository.Update(new Category
        {
            Id = missingId,
            Name = "Несуществующая категория"
        });

        var actualCategories = repository.GetAll()
            .Select(category => (category.Id, category.Name))
            .ToArray();

        Assert.False(updated);
        Assert.Equal(initialCategories, actualCategories);
    }

    /// <summary>
    /// Проверяет повторное удаление категории
    /// </summary>
    [Fact]
    public void Delete_AllowsRepeatedDeletion()
    {
        var data = new FoodDeliveryData();
        var repository = new CategoryRepository(data);
        var categoryId = data.Categories.First().Id;
        var initialCount = repository.GetAll().Count;

        repository.Delete(categoryId);
        repository.Delete(categoryId);

        Assert.Null(repository.GetById(categoryId));
        Assert.Equal(initialCount - 1, repository.GetAll().Count);
    }

    /// <summary>
    /// Проверяет отражение добавления и удаления в коллекции приложения
    /// </summary>
    [Fact]
    public void AddAndDelete_UpdateApplicationCollection()
    {
        var data = new FoodDeliveryData();
        var repository = new CategoryRepository(data);

        var category = repository.Add(new Category
        {
            Id = 0,
            Name = "Новая категория"
        });

        Assert.Contains(data.Categories, item => ReferenceEquals(item, category));

        repository.Delete(category.Id);

        Assert.DoesNotContain(
            data.Categories,
            item => item.Id == category.Id);
    }
}
