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
    /// <inheritdoc />
    public async Task<RestaurantDto> CreateAsync(
        RestaurantCreateUpdateDto dto)
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

        var createdRestaurant =
            await restaurantRepository.AddAsync(restaurant);

        return MapToDto(createdRestaurant);
    }

    /// <inheritdoc />
    public async Task<RestaurantDto?> GetAsync(int id)
    {
        var restaurant =
            await restaurantRepository.GetByIdAsync(id);

        return restaurant is null
            ? null
            : MapToDto(restaurant);
    }

    /// <inheritdoc />
    public async Task<IList<RestaurantDto>> GetAllAsync()
    {
        var restaurants =
            await restaurantRepository.GetAllAsync();

        return restaurants
            .Select(MapToDto)
            .ToList();
    }

    /// <inheritdoc />
    public async Task<RestaurantDto> UpdateAsync(
        RestaurantCreateUpdateDto dto,
        int id)
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

        var updatedRestaurant =
            await restaurantRepository.UpdateAsync(restaurant);

        if (updatedRestaurant is null)
        {
            throw new KeyNotFoundException(
                $"Ресторан с идентификатором {id} не найден");
        }

        return MapToDto(updatedRestaurant);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        return await restaurantRepository.DeleteAsync(id);
    }

    private static RestaurantDto MapToDto(
        Restaurant restaurant)
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