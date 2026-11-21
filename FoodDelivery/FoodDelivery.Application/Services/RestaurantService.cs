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
    public override async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await repository.HasDishesAsync(id, cancellationToken) || await repository.HasOrdersAsync(id, cancellationToken))
        {
            throw new ResourceConflictException(
                "Нельзя удалить ресторан, у которого есть блюда или заказы");
        }

        await base.DeleteAsync(id, cancellationToken);
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
    protected override Task<Restaurant> ToEntityAsync(
        RestaurantRequest request,
        int id,
        Restaurant? existing,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(new Restaurant
        {
            Id = id,
            Name = request.Name.Trim(),
            Address = request.Address.Trim(),
            Rating = request.Rating,
            OpeningTime = request.OpeningTime.ToTimeSpan(),
            ClosingTime = request.ClosingTime.ToTimeSpan()
        });
    }
}
