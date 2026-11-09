using EnterpriseDevelopment.Application.Interfaces;
using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Contracts.DTOs.Orders;
using EnterpriseDevelopment.Domain.Entities;

namespace EnterpriseDevelopment.Application.Services;

/// <summary>
/// Сервис для управления заказами
/// </summary>
public class OrderService(
    IOrderRepository repository,
    IClientRepository clientRepository,
    IRestaurantRepository restaurantRepository,
    IDishRepository dishRepository) : IOrderService
{
    /// <summary>
    /// Получает все заказы
    /// </summary>
    /// <returns>Коллекция заказов</returns>
    public IEnumerable<OrderDto> GetAll()
    {
        return repository.GetAll().Select(ToDto);
    }

    /// <summary>
    /// Получает заказ по идентификатору
    /// </summary>
    /// <returns>Данные заказа или null</returns>
    public OrderDto? GetById(int id)
    {
        var order = repository.GetById(id);

        return order is null ? null : ToDto(order);
    }

    /// <summary>
    /// Создаёт новый заказ
    /// </summary>
    /// <returns>Данные созданного заказа</returns>
    public OrderDto Create(CreateOrderDto dto)
    {
        var (client, restaurant, dishes) = Validate(
            dto.ClientId,
            dto.RestaurantId,
            dto.DishIds,
            dto.CreatedAt,
            dto.DeliveredAt);

        var order = new Order
        {
            ClientId = dto.ClientId,
            RestaurantId = dto.RestaurantId,
            Client = client,
            Restaurant = restaurant,
            Dishes = dishes,
            CreatedAt = dto.CreatedAt,
            DeliveredAt = dto.DeliveredAt
        };

        return ToDto(repository.Add(order));
    }

    /// <summary>
    /// Обновляет существующий заказ
    /// </summary>
    /// <returns>true, если заказ обновлён, иначе false</returns>
    public bool Update(int id, UpdateOrderDto dto)
    {
        var order = repository.GetById(id);

        if (order is null)
        {
            return false;
        }

        var (client, restaurant, dishes) = Validate(
            dto.ClientId,
            dto.RestaurantId,
            dto.DishIds,
            dto.CreatedAt,
            dto.DeliveredAt);

        order.ClientId = dto.ClientId;
        order.RestaurantId = dto.RestaurantId;
        order.Client = client;
        order.Restaurant = restaurant;
        order.Dishes = dishes;
        order.CreatedAt = dto.CreatedAt;
        order.DeliveredAt = dto.DeliveredAt;

        repository.Update(order);

        return true;
    }

    /// <summary>
    /// Удаляет заказ
    /// </summary>
    /// <returns>true, если заказ существовал, иначе false</returns>
    public bool Delete(int id)
    {
        var order = repository.GetById(id);

        if (order is null)
        {
            return false;
        }

        repository.Delete(order);

        return true;
    }

    private static OrderDto ToDto(Order order)
    {
        return new OrderDto(
            order.Id,
            order.ClientId,
            order.RestaurantId,
            order.Dishes.Select(dish => dish.Id).ToList(),
            order.CreatedAt,
            order.DeliveredAt,
            order.TotalAmount,
            order.DeliveryTime);
    }

    private (Client Client, Restaurant Restaurant, List<Dish> Dishes) Validate(
        int clientId,
        int restaurantId,
        List<int> dishIds,
        DateTimeOffset createdAt,
        DateTimeOffset? deliveredAt)
    {
        var client = clientRepository.GetById(clientId)
            ?? throw new KeyNotFoundException("Клиент не найден");

        var restaurant = restaurantRepository.GetById(restaurantId)
            ?? throw new KeyNotFoundException("Ресторан не найден");

        if (dishIds is null || dishIds.Count == 0)
        {
            throw new ArgumentException(
                "Заказ должен содержать хотя бы одно блюдо");
        }

        var dishes = new List<Dish>();

        foreach (var dishId in dishIds)
        {
            var dish = dishRepository.GetById(dishId)
                ?? throw new KeyNotFoundException(
                    $"Блюдо с идентификатором {dishId} не найдено");

            if (dish.RestaurantId != restaurantId)
            {
                throw new InvalidOperationException(
                    "Все блюда заказа должны принадлежать выбранному ресторану");
            }

            dishes.Add(dish);
        }

        if (deliveredAt < createdAt)
        {
            throw new ArgumentException(
                "Дата доставки не может быть раньше даты создания заказа");
        }

        return (client, restaurant, dishes);
    }
}
