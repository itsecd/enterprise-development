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
    public async Task Delete_ThrowsConflictWhenCategoryHasDishes()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var data = new FoodDeliveryData();
        var repository = new CategoryRepository(data);
        var service = new CategoryService(repository);
        var category = data.Dishes.First().Category;

        await Assert.ThrowsAsync<ResourceConflictException>(
            async () => await service.DeleteAsync(category.Id, cancellationToken));

        Assert.Same(category, repository.GetById(category.Id));
    }

    /// <summary>
    /// Проверяет создание и повторное удаление свободной категории
    /// </summary>
    [Fact]
    public async Task Delete_AllowsRepeatedDeletionOfUnusedCategory()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var data = new FoodDeliveryData();
        var repository = new CategoryRepository(data);
        var service = new CategoryService(repository);

        var category = await service.CreateAsync(new CategoryRequest
        {
            Name = "  Новая категория  "
        }, cancellationToken);

        Assert.Equal("Новая категория", category.Name);

        await service.DeleteAsync(category.Id, cancellationToken);
        await service.DeleteAsync(category.Id, cancellationToken);

        await Assert.ThrowsAsync<ResourceNotFoundException>(async () => await service.GetByIdAsync(category.Id, cancellationToken));
    }
}
