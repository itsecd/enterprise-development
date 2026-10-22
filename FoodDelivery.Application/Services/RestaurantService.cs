using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Реализует прикладные операции над ресторанами
/// </summary>
public class RestaurantService(
    IRestaurantRepository restaurantRepository)
    : IRestaurantService
{
    public RestaurantDto Create(RestaurantCreateUpdateDto dto)
    {
        var restaurant = new Restaurant
        {
            Id = 0,
            Name = dto.Name,
            Address = dto.Address,
            Rating = dto.Rating,
            OpeningTime = dto.OpeningTime,
            ClosingTime = dto.ClosingTime
        };

        return MapToDto(restaurantRepository.Add(restaurant));
    }

    public RestaurantDto? Get(int id)
    {
        var restaurant = restaurantRepository.GetById(id);
        return restaurant is null ? null : MapToDto(restaurant);
    }

    public IList<RestaurantDto> GetAll()
    {
        return restaurantRepository.GetAll()
            .Select(MapToDto)
            .ToList();
    }

    public RestaurantDto Update(RestaurantCreateUpdateDto dto, int id)
    {
        var restaurant = new Restaurant
        {
            Id = id,
            Name = dto.Name,
            Address = dto.Address,
            Rating = dto.Rating,
            OpeningTime = dto.OpeningTime,
            ClosingTime = dto.ClosingTime
        };

        var updated = restaurantRepository.Update(restaurant)
            ?? throw new KeyNotFoundException(
                $"Ресторан с идентификатором {id} не найден");

        return MapToDto(updated);
    }

    public bool Delete(int id)
    {
        return restaurantRepository.Delete(id);
    }

    private static RestaurantDto MapToDto(Restaurant restaurant)
    {
        return new RestaurantDto(
            restaurant.Id,
            restaurant.Name,
            restaurant.Address,
            restaurant.Rating,
            restaurant.OpeningTime,
            restaurant.ClosingTime);
    }
}
