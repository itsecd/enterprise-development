using EnterpriseDevelopment.Contracts.DTOs.Restaurants;

namespace EnterpriseDevelopment.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для работы с ресторанами
/// </summary>
public interface IRestaurantService
    : ICrudService<RestaurantDto, CreateRestaurantDto, UpdateRestaurantDto>
{
}
