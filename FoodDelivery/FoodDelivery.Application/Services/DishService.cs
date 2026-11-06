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
public sealed class DishService(
    IDishRepository repository,
    ICategoryRepository categoryRepository,
    IRestaurantRepository restaurantRepository)
    : CrudServiceBase<Dish, DishDto, DishRequest>, IDishService
{
    /// <inheritdoc/>
    protected override IRepository<Dish> Repository => repository;

    /// <inheritdoc/>
    public override void Delete(int id)
    {
        repository.Execute(() =>
        {
            if (repository.IsUsedInOrders(id))
            {
                throw new ResourceConflictException(
                    "Нельзя удалить блюдо, которое используется в заказах");
            }

            base.Delete(id);
        });
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
    protected override Dish ToEntity(DishRequest request, int id)
    {
        var category = categoryRepository.GetById(request.CategoryId)
            ?? throw new InvalidReferenceException(
                $"Категория с идентификатором {request.CategoryId} не найдена");

        var restaurant = restaurantRepository.GetById(request.RestaurantId)
            ?? throw new InvalidReferenceException(
                $"Ресторан с идентификатором {request.RestaurantId} не найден");

        if (id > 0 && repository.IsUsedInOrders(id))
        {
            var existingDish = repository.GetById(id);

            if (existingDish is not null &&
                existingDish.Restaurant.Id != restaurant.Id)
            {
                throw new ResourceConflictException(
                    "Нельзя изменить ресторан блюда, которое используется в заказах");
            }
        }

        return new Dish
        {
            Id = id,
            Name = request.Name.Trim(),
            Weight = request.Weight,
            Price = request.Price,
            Category = category,
            Restaurant = restaurant
        };
    }

    /// <inheritdoc/>
    public IReadOnlyList<DishDto> GetByCategory(int categoryId)
    {
        return repository.Execute<IReadOnlyList<DishDto>>(() =>
        {
            if (categoryRepository.GetById(categoryId) is null)
            {
                throw new ResourceNotFoundException(
                    $"Категория с идентификатором {categoryId} не найдена");
            }

            return repository.GetByCategory(categoryId)
                .Select(ToDto)
                .ToArray();
        });
    }

    /// <inheritdoc/>
    public IReadOnlyList<DishDto> GetByRestaurant(int restaurantId)
    {
        return repository.Execute<IReadOnlyList<DishDto>>(() =>
        {
            if (restaurantRepository.GetById(restaurantId) is null)
            {
                throw new ResourceNotFoundException(
                    $"Ресторан с идентификатором {restaurantId} не найден");
            }

            return repository.GetByRestaurant(restaurantId)
                .Select(ToDto)
                .ToArray();
        });
    }
}
