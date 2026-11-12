using Microsoft.Extensions.Logging;
using FoodDelivery.Contracts.Orders;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Сервис управления заказами.
/// </summary>
public class OrderService(
    IRepository<Order> orderRepo,
    IRepository<Client> clientRepo,
    IRepository<Restaurant> restaurantRepo,
    IRepository<Dish> dishRepo,
    ILogger<OrderService> logger) : IOrderService
{
    public IReadOnlyList<OrderDto> GetAll() =>
        [.. orderRepo.GetAll().Select(MapToDto)];

    public OrderDto? GetById(int id)
    {
        var order = orderRepo.GetById(id);
        return order is null ? null : MapToDto(order);
    }

    public OrderDto Create(OrderCreateUpdateDto dto)
    {
        var dishes = ValidateAndResolveOrderEntities(dto);

        var entity = new Order
        {
            ClientId = dto.ClientId,
            Client = clientRepo.GetById(dto.ClientId),
            RestaurantId = dto.RestaurantId,
            Restaurant = restaurantRepo.GetById(dto.RestaurantId),
            OrderDateTime = dto.OrderDateTime,
            DeliveryDateTime = dto.DeliveryDateTime,
            TotalPrice = dto.TotalPrice,
            Dishes = dishes
        };

        var created = orderRepo.Create(entity);
        logger.LogInformation("Создан заказ с Id = {Id} для ресторана {RestaurantId}", created.Id, created.RestaurantId);
        return MapToDto(created);
    }

    public bool Update(int id, OrderCreateUpdateDto dto)
    {
        var existing = orderRepo.GetById(id);
        if (existing is null)
            return false;

        var dishes = ValidateAndResolveOrderEntities(dto);

        existing.ClientId = dto.ClientId;
        existing.Client = clientRepo.GetById(dto.ClientId);
        existing.RestaurantId = dto.RestaurantId;
        existing.Restaurant = restaurantRepo.GetById(dto.RestaurantId);
        existing.OrderDateTime = dto.OrderDateTime;
        existing.DeliveryDateTime = dto.DeliveryDateTime;
        existing.TotalPrice = dto.TotalPrice;
        existing.Dishes = dishes;

        var result = orderRepo.Update(existing);
        logger.LogInformation("Обновлён заказ с Id = {Id}", id);
        return result;
    }

    public void Delete(int id)
    {
        orderRepo.Delete(id);
        logger.LogInformation("Удалён заказ с Id = {Id}", id);
    }

    private List<Dish> ValidateAndResolveOrderEntities(OrderCreateUpdateDto dto)
    {
        if (clientRepo.GetById(dto.ClientId) is null)
            throw new ArgumentException($"Клиент с Id = {dto.ClientId} не найден.");

        if (restaurantRepo.GetById(dto.RestaurantId) is null)
            throw new ArgumentException($"Ресторан с Id = {dto.RestaurantId} не найден.");

        var resolvedDishes = new List<Dish>();
        foreach (var dishId in dto.DishIds)
        {
            var dish = dishRepo.GetById(dishId);
            if (dish is null)
                throw new ArgumentException($"Блюдо с Id = {dishId} не найдено.");

            if (dish.RestaurantId != dto.RestaurantId)
                throw new ArgumentException(
                    $"Блюдо «{dish.Name}» (Id = {dish.Id}) принадлежит ресторану Id = {dish.RestaurantId}, а заказ оформляется в ресторан Id = {dto.RestaurantId}.");

            resolvedDishes.Add(dish);
        }

        return resolvedDishes;
    }

    private static OrderDto MapToDto(Order o) => new(
        o.Id,
        o.ClientId,
        o.RestaurantId,
        o.OrderDateTime,
        o.DeliveryDateTime,
        o.TotalPrice,
        [.. o.Dishes.Select(d => d.Id)]);
}