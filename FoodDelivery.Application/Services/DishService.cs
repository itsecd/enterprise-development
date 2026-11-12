using Microsoft.Extensions.Logging;
using FoodDelivery.Contracts.Dishes;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Сервис для управления блюдами в меню ресторанов.
/// </summary>
public class DishService(
    IRepository<Dish> dishRepo,
    IRepository<DishCategory> categoryRepo,
    IRepository<Restaurant> restaurantRepo,
    ILogger<DishService> logger) : IDishService
{
    public IReadOnlyList<DishDto> GetAll() =>
        [.. dishRepo.GetAll().Select(d => new DishDto(d.Id, d.Name, d.Weight, d.Price, d.CategoryId, d.RestaurantId))];

    public DishDto? GetById(int id)
    {
        var d = dishRepo.GetById(id);
        return d is null ? null : new DishDto(d.Id, d.Name, d.Weight, d.Price, d.CategoryId, d.RestaurantId);
    }

    public DishDto Create(DishCreateUpdateDto dto)
    {
        var entity = new Dish
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Price = dto.Price,
            CategoryId = dto.CategoryId,
            Category = categoryRepo.GetById(dto.CategoryId),
            RestaurantId = dto.RestaurantId,
            Restaurant = restaurantRepo.GetById(dto.RestaurantId)
        };
        var created = dishRepo.Create(entity);
        logger.LogInformation("Создано блюдо с Id = {Id}", created.Id);
        return new DishDto(created.Id, created.Name, created.Weight, created.Price, created.CategoryId, created.RestaurantId);
    }

    public bool Update(int id, DishCreateUpdateDto dto)
    {
        var existing = dishRepo.GetById(id);
        if (existing is null) return false;

        existing.Name = dto.Name;
        existing.Weight = dto.Weight;
        existing.Price = dto.Price;
        existing.CategoryId = dto.CategoryId;
        existing.Category = categoryRepo.GetById(dto.CategoryId);
        existing.RestaurantId = dto.RestaurantId;
        existing.Restaurant = restaurantRepo.GetById(dto.RestaurantId);

        var result = dishRepo.Update(existing);
        logger.LogInformation("Обновлено блюдо с Id = {Id}", id);
        return result;
    }

    public void Delete(int id)
    {
        dishRepo.Delete(id);
        logger.LogInformation("Удалено блюдо с Id = {Id}", id);
    }
}