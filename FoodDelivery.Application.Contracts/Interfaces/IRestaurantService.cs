using FoodDelivery.Application.Contracts.DTOs;

namespace FoodDelivery.Application.Contracts.Interfaces;

/// <summary>
/// Прикладной сервис для работы с ресторанами
/// </summary>
public interface IRestaurantService
    : IApplicationService<RestaurantDto, RestaurantCreateUpdateDto, int>
{
}