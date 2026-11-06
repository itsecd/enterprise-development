using FoodDelivery.Application.Contracts.Restaurants;
using FoodDelivery.Application.Exceptions;
using FoodDelivery.Domain.Models;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Выполняет операции с ресторанами
/// </summary>
/// <param name="repository">Репозиторий ресторанов</param>
public sealed class RestaurantService(IRestaurantRepository repository)
    : CrudServiceBase<Restaurant, RestaurantDto, RestaurantRequest>
{
    /// <inheritdoc/>
    protected override IRepository<Restaurant> Repository => repository;

    /// <inheritdoc/>
    public override void Delete(int id)
    {
        repository.Execute(() =>
        {
            if (repository.HasDishes(id) || repository.HasOrders(id))
            {
                throw new ResourceConflictException(
                    "Нельзя удалить ресторан, у которого есть блюда или заказы");
            }

            base.Delete(id);
        });
    }

    /// <inheritdoc/>
    protected override RestaurantDto ToDto(Restaurant entity)
    {
        return new RestaurantDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Address = entity.Address,
            Rating = entity.Rating,
            OpeningTime = TimeOnly.FromTimeSpan(entity.OpeningTime),
            ClosingTime = TimeOnly.FromTimeSpan(entity.ClosingTime)
        };
    }

    /// <inheritdoc/>
    protected override Restaurant ToEntity(RestaurantRequest request, int id)
    {
        return new Restaurant
        {
            Id = id,
            Name = request.Name.Trim(),
            Address = request.Address.Trim(),
            Rating = request.Rating,
            OpeningTime = request.OpeningTime.ToTimeSpan(),
            ClosingTime = request.ClosingTime.ToTimeSpan()
        };
    }
}
