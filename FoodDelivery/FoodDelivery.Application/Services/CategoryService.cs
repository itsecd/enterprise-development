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
    public override void Delete(int id)
    {
        repository.Execute(() =>
        {
            if (repository.HasDishes(id))
            {
                throw new ResourceConflictException(
                    "Нельзя удалить категорию, которая используется блюдами");
            }

            base.Delete(id);
        });
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
    protected override Category ToEntity(CategoryRequest request, int id)
    {
        return new Category
        {
            Id = id,
            Name = request.Name.Trim()
        };
    }
}
