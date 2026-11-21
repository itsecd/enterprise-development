using FoodDelivery.Application.Contracts.Dishes;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Выполняет операции с блюдами
/// </summary>
/// <param name="repository">Репозиторий блюд</param>
/// <param name="categoryRepository">Репозиторий категорий</param>
/// <param name="restaurantRepository">Репозиторий ресторанов</param>
/// <param name="transaction">Транзакция записи связанных данных заказа</param>
public sealed class DishService(
    IDishRepository repository,
    ICategoryRepository categoryRepository,
    IRestaurantRepository restaurantRepository,
    IOrderWriteTransaction transaction)
    : CrudServiceBase<Dish, DishDto, DishRequest>, IDishService
{
    /// <inheritdoc/>
    protected override IRepository<Dish> Repository => repository;

    /// <inheritdoc/>
    public override Task<DishDto> UpdateAsync(
        int id, DishRequest request, CancellationToken cancellationToken = default)
    {
        return transaction.ExecuteAsync(token => base.UpdateAsync(id, request, token), cancellationToken);
    }

    /// <inheritdoc/>
    public override async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await repository.IsUsedInOrdersAsync(id, cancellationToken))
        {
            throw new ResourceConflictException(
                "Нельзя удалить блюдо, которое используется в заказах");
        }

        await base.DeleteAsync(id, cancellationToken);
    }

    /// <inheritdoc/>
    protected override DishDto ToDto(Dish entity)
    {
        return new DishDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Weight = entity.Weight,
            Price = entity.Price,
            CategoryId = entity.Category.Id,
            RestaurantId = entity.Restaurant.Id
        };
    }

    /// <inheritdoc/>
    protected override async Task<Dish> ToEntityAsync(
        DishRequest request,
        int id,
        Dish? existing,
        CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetByIdAsync(request.CategoryId, cancellationToken)
            ?? throw new InvalidReferenceException(
                $"Категория с идентификатором {request.CategoryId} не найдена");

        var restaurant = await restaurantRepository.GetByIdAsync(request.RestaurantId, cancellationToken)
            ?? throw new InvalidReferenceException(
                $"Ресторан с идентификатором {request.RestaurantId} не найден");

        if (existing is not null && existing.Restaurant.Id != restaurant.Id &&
            await repository.IsUsedInOrdersAsync(id, cancellationToken))
        {
            throw new ResourceConflictException(
                "Нельзя изменить ресторан блюда, которое используется в заказах");
        }

        return new Dish
        {
            Id = id,
            Name = request.Name.Trim(),
            Weight = request.Weight,
            Price = request.Price,
            CategoryId = category.Id,
            RestaurantId = restaurant.Id,
            Category = category,
            Restaurant = restaurant
        };
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<DishDto>> GetByCategoryAsync(int categoryId, CancellationToken cancellationToken = default)
    {
        if (await categoryRepository.GetByIdAsync(categoryId, cancellationToken) is null)
        {
            throw new ResourceNotFoundException(
                $"Категория с идентификатором {categoryId} не найдена");
        }

        return (await repository.GetByCategoryAsync(categoryId, cancellationToken))
            .Select(ToDto)
            .ToArray();
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<DishDto>> GetByRestaurantAsync(int restaurantId, CancellationToken cancellationToken = default)
    {
        if (await restaurantRepository.GetByIdAsync(restaurantId, cancellationToken) is null)
        {
            throw new ResourceNotFoundException(
                $"Ресторан с идентификатором {restaurantId} не найден");
        }

        return (await repository.GetByRestaurantAsync(restaurantId, cancellationToken))
            .Select(ToDto)
            .ToArray();
    }
}
