using FoodDelivery.Application.Contracts.Categories;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Выполняет операции с категориями блюд
/// </summary>
/// <param name="repository">Репозиторий категорий</param>
public sealed class CategoryService(ICategoryRepository repository)
    : CrudServiceBase<Category, CategoryDto, CategoryRequest>
{
    /// <inheritdoc/>
    protected override IRepository<Category> Repository => repository;

    /// <inheritdoc/>
    public override async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await repository.HasDishesAsync(id, cancellationToken))
        {
            throw new ResourceConflictException(
                "Нельзя удалить категорию, которая используется блюдами");
        }

        await base.DeleteAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    protected override CategoryDto ToDto(Category entity)
    {
        return new CategoryDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    /// <inheritdoc/>
    protected override Task<Category> ToEntityAsync(
        CategoryRequest request,
        int id,
        Category? existing,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(new Category
        {
            Id = id,
            Name = request.Name.Trim()
        });
    }
}
