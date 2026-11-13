using FoodDelivery.Application.Contracts.DTOs;

namespace FoodDelivery.Application.Contracts.Interfaces;

public interface IRestaurantService
    : IApplicationService<RestaurantDto, RestaurantCreateUpdateDto, int>
{
}