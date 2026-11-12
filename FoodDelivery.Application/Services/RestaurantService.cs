using Microsoft.Extensions.Logging;
using FoodDelivery.Contracts.Restaurants;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Сервис для управления ресторанами-партнёрами.
/// </summary>
public class RestaurantService(IRepository<Restaurant> repository, ILogger<RestaurantService> logger) : IRestaurantService
{
    public IReadOnlyList<RestaurantDto> GetAll() =>
        [.. repository.GetAll().Select(r => new RestaurantDto(r.Id, r.Name, r.Address, r.Rating, r.OpenTime, r.CloseTime))];

    public RestaurantDto? GetById(int id)
    {
        var r = repository.GetById(id);
        return r is null ? null : new RestaurantDto(r.Id, r.Name, r.Address, r.Rating, r.OpenTime, r.CloseTime);
    }

    public RestaurantDto Create(RestaurantCreateUpdateDto dto)
    {
        var entity = new Restaurant
        {
            Name = dto.Name,
            Address = dto.Address,
            Rating = dto.Rating,
            OpenTime = dto.OpenTime,
            CloseTime = dto.CloseTime
        };
        var created = repository.Create(entity);
        logger.LogInformation("Создан ресторан с Id = {Id}", created.Id);
        return new RestaurantDto(created.Id, created.Name, created.Address, created.Rating, created.OpenTime, created.CloseTime);
    }

    public bool Update(int id, RestaurantCreateUpdateDto dto)
    {
        var existing = repository.GetById(id);
        if (existing is null) return false;

        existing.Name = dto.Name;
        existing.Address = dto.Address;
        existing.Rating = dto.Rating;
        existing.OpenTime = dto.OpenTime;
        existing.CloseTime = dto.CloseTime;

        var result = repository.Update(existing);
        logger.LogInformation("Обновлен ресторан с Id = {Id}", id);
        return result;
    }

    public void Delete(int id)
    {
        repository.Delete(id);
        logger.LogInformation("Удален ресторан с Id = {Id}", id);
    }
}