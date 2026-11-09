using EnterpriseDevelopment.Application.Interfaces;
using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Contracts.DTOs.Restaurants;
using EnterpriseDevelopment.Domain.Entities;

namespace EnterpriseDevelopment.Application.Services;

/// <summary>
/// Сервис для управления ресторанами
/// </summary>
public class RestaurantService(
    IRestaurantRepository repository,
    IDishRepository dishRepository,
    IOrderRepository orderRepository) : IRestaurantService
{
    /// <summary>
    /// Получает все рестораны
    /// </summary>
    /// <returns>Коллекция ресторанов</returns>
    public IEnumerable<RestaurantDto> GetAll()
    {
        return repository.GetAll().Select(ToDto);
    }

    /// <summary>
    /// Получает ресторан по идентификатору
    /// </summary>
    /// <returns>Данные ресторана или null</returns>
    public RestaurantDto? GetById(int id)
    {
        var restaurant = repository.GetById(id);
        return restaurant is null ? null : ToDto(restaurant);
    }

    /// <summary>
    /// Создаёт новый ресторан
    /// </summary>
    /// <returns>Данные созданного ресторана</returns>
    public RestaurantDto Create(CreateRestaurantDto dto)
    {
        Validate(dto.Name, dto.Address, dto.Rating, dto.OpeningTime, dto.ClosingTime);

        var restaurant = new Restaurant
        {
            Name = dto.Name,
            Address = dto.Address,
            Rating = dto.Rating,
            OpeningTime = dto.OpeningTime,
            ClosingTime = dto.ClosingTime
        };

        return ToDto(repository.Add(restaurant));
    }

    /// <summary>
    /// Обновляет существующий ресторан
    /// </summary>
    /// <returns>true, если ресторан обновлён, иначе false</returns>
    public bool Update(int id, UpdateRestaurantDto dto)
    {
        var restaurant = repository.GetById(id);
        if (restaurant is null)
        {
            return false;
        }

        Validate(dto.Name, dto.Address, dto.Rating, dto.OpeningTime, dto.ClosingTime);

        restaurant.Name = dto.Name;
        restaurant.Address = dto.Address;
        restaurant.Rating = dto.Rating;
        restaurant.OpeningTime = dto.OpeningTime;
        restaurant.ClosingTime = dto.ClosingTime;
        repository.Update(restaurant);

        return true;
    }

    /// <summary>
    /// Удаляет ресторан без связанных блюд и заказов
    /// </summary>
    /// <returns>true, если ресторан удалён, иначе false</returns>
    public bool Delete(int id)
    {
        var restaurant = repository.GetById(id);
        if (restaurant is null)
        {
            return false;
        }

        if (dishRepository.GetAll().Any(dish => dish.RestaurantId == id) ||
            orderRepository.GetAll().Any(order => order.RestaurantId == id))
        {
            return false;
        }

        repository.Delete(restaurant);
        return true;
    }

    private static RestaurantDto ToDto(Restaurant restaurant)
    {
        return new RestaurantDto(
            restaurant.Id,
            restaurant.Name,
            restaurant.Address,
            restaurant.Rating,
            restaurant.OpeningTime,
            restaurant.ClosingTime);
    }

    private static void Validate(
        string name,
        string address,
        double rating,
        TimeOnly openingTime,
        TimeOnly closingTime)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Название ресторана не может быть пустым");
        }

        if (string.IsNullOrWhiteSpace(address))
        {
            throw new ArgumentException("Адрес ресторана не может быть пустым");
        }

        if (rating < 0 || rating > 5 || double.IsNaN(rating))
        {
            throw new ArgumentException("Рейтинг ресторана должен быть от 0 до 5");
        }

        if (openingTime >= closingTime)
        {
            throw new ArgumentException("Время открытия должно быть раньше времени закрытия");
        }
    }
}
