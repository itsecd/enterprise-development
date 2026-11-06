using FoodDelivery.Application.Contracts.Categories;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Application.Services;
using FoodDelivery.Domain.Data;
using FoodDelivery.Infrastructure.InMemory.Repositories;

namespace FoodDelivery.Tests.Services;

/// <summary>
/// Проверяет прикладные операции с категориями
/// </summary>
public sealed class CategoryServiceTests
{
    /// <summary>
    /// Проверяет запрет удаления категории с блюдами
    /// </summary>
    [Fact]
    public void Delete_ThrowsConflictWhenCategoryHasDishes()
    {
        var data = new FoodDeliveryData();
        var repository = new CategoryRepository(data);
        var service = new CategoryService(repository);
        var category = data.Dishes.First().Category;

        Assert.Throws<ResourceConflictException>(
            () => service.Delete(category.Id));

        Assert.Same(category, repository.GetById(category.Id));
    }

    /// <summary>
    /// Проверяет создание и повторное удаление свободной категории
    /// </summary>
    [Fact]
    public void Delete_AllowsRepeatedDeletionOfUnusedCategory()
    {
        var data = new FoodDeliveryData();
        var repository = new CategoryRepository(data);
        var service = new CategoryService(repository);

        var category = service.Create(new CategoryRequest
        {
            Name = "  Новая категория  "
        });

        Assert.Equal("Новая категория", category.Name);

        service.Delete(category.Id);
        service.Delete(category.Id);

        Assert.Null(service.GetById(category.Id));
    }
}
