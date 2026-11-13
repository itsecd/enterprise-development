using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Реализует прикладные операции над блюдами
/// </summary>
public class DishService(
    IDishRepository dishRepository,
    ICategoryRepository categoryRepository,
    IRestaurantRepository restaurantRepository)
    : IDishService
{
    /// <inheritdoc />
    public async Task<DishDto> CreateAsync(DishCreateUpdateDto dto)
    {
        var category = await categoryRepository.GetByIdAsync(dto.CategoryId)
            ?? throw new KeyNotFoundException(
                $"Категория с идентификатором {dto.CategoryId} не найдена");

        var restaurant = await restaurantRepository.GetByIdAsync(dto.RestaurantId)
            ?? throw new KeyNotFoundException(
                $"Ресторан с идентификатором {dto.RestaurantId} не найден");

        var dish = new Dish
        {
            Id = 0,
            Name = dto.Name,
            WeightGrams = dto.WeightGrams,
            Price = dto.Price,
            CategoryId = category.Id,
            Category = category,
            RestaurantId = restaurant.Id,
            Restaurant = restaurant
        };

        var created = await dishRepository.AddAsync(dish);

        return MapToDto(created);
    }

    /// <inheritdoc />
    public async Task<DishDto?> GetAsync(int id)
    {
        var dish = await dishRepository.GetByIdAsync(id);

        return dish is null ? null : MapToDto(dish);
    }

    /// <inheritdoc />
    public async Task<IList<DishDto>> GetAllAsync()
    {
        var dishes = await dishRepository.GetAllAsync();

        return dishes
            .Select(MapToDto)
            .ToList();
    }

    /// <inheritdoc />
    public async Task<DishDto> UpdateAsync(
        DishCreateUpdateDto dto,
        int id)
    {
        var existing = await dishRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                $"Блюдо с идентификатором {id} не найдено");

        _ = await categoryRepository.GetByIdAsync(dto.CategoryId)
            ?? throw new KeyNotFoundException(
                $"Категория с идентификатором {dto.CategoryId} не найдена");

        _ = await restaurantRepository.GetByIdAsync(dto.RestaurantId)
            ?? throw new KeyNotFoundException(
                $"Ресторан с идентификатором {dto.RestaurantId} не найден");

        existing.Name = dto.Name;
        existing.WeightGrams = dto.WeightGrams;
        existing.Price = dto.Price;
        existing.CategoryId = dto.CategoryId;
        existing.RestaurantId = dto.RestaurantId;

        var updated = await dishRepository.UpdateAsync(existing)
            ?? throw new KeyNotFoundException(
                $"Блюдо с идентификатором {id} не найдено");

        return MapToDto(updated);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        return await dishRepository.DeleteAsync(id);
    }

    private static DishDto MapToDto(Dish dish)
    {
        return new DishDto(
            dish.Id,
            dish.Name,
            dish.WeightGrams,
            dish.Price,
            dish.CategoryId,
            dish.RestaurantId);
    }
}