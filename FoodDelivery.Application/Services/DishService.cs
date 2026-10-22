using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Реализует прикладные операции над блюдами.
/// </summary>
public class DishService(
    IDishRepository dishRepository,
    ICategoryRepository categoryRepository,
    IRestaurantRepository restaurantRepository)
    : IDishService
{
    public DishDto Create(DishCreateUpdateDto dto)
    {
        var category = categoryRepository.GetById(dto.CategoryId)
            ?? throw new KeyNotFoundException(
                $"Категория с идентификатором {dto.CategoryId} не найдена");

        var restaurant = restaurantRepository.GetById(dto.RestaurantId)
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

        var created = dishRepository.Add(dish);
        restaurant.Dishes.Add(created);

        return MapToDto(created);
    }

    public DishDto? Get(int id)
    {
        var dish = dishRepository.GetById(id);
        return dish is null ? null : MapToDto(dish);
    }

    public IList<DishDto> GetAll()
    {
        return dishRepository.GetAll()
            .Select(MapToDto)
            .ToList();
    }

    public DishDto Update(DishCreateUpdateDto dto, int id)
    {
        var existing = dishRepository.GetById(id)
            ?? throw new KeyNotFoundException(
                $"Блюдо с идентификатором {id} не найдено");

        var category = categoryRepository.GetById(dto.CategoryId)
            ?? throw new KeyNotFoundException(
                $"Категория с идентификатором {dto.CategoryId} не найдена");

        var restaurant = restaurantRepository.GetById(dto.RestaurantId)
            ?? throw new KeyNotFoundException(
                $"Ресторан с идентификатором {dto.RestaurantId} не найден");

        if (existing.RestaurantId != restaurant.Id)
        {
            existing.Restaurant.Dishes.Remove(existing);
            restaurant.Dishes.Add(existing);
        }

        existing.Name = dto.Name;
        existing.WeightGrams = dto.WeightGrams;
        existing.Price = dto.Price;
        existing.CategoryId = category.Id;
        existing.Category = category;
        existing.RestaurantId = restaurant.Id;
        existing.Restaurant = restaurant;

        var updated = dishRepository.Update(existing)
            ?? throw new KeyNotFoundException(
                $"Блюдо с идентификатором {id} не найдено");

        return MapToDto(updated);
    }

    public bool Delete(int id)
    {
        var dish = dishRepository.GetById(id);

        if (dish is null)
        {
            return false;
        }

        dish.Restaurant.Dishes.Remove(dish);
        return dishRepository.Delete(id);
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
