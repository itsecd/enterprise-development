using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Реализует прикладные операции над заказами
/// </summary>
public class OrderService(
    IOrderRepository orderRepository,
    ICustomerRepository customerRepository,
    IRestaurantRepository restaurantRepository)
    : IOrderService
{
    /// <inheritdoc />
    public async Task<OrderDto> CreateAsync(OrderCreateUpdateDto dto)
    {
        var customer = await customerRepository.GetByIdAsync(dto.CustomerId)
            ?? throw new KeyNotFoundException(
                $"Покупатель с идентификатором {dto.CustomerId} не найден");

        var restaurant = await restaurantRepository.GetByIdAsync(dto.RestaurantId)
            ?? throw new KeyNotFoundException(
                $"Ресторан с идентификатором {dto.RestaurantId} не найден");

        var order = new Order
        {
            Id = 0,
            CustomerId = customer.Id,
            Customer = customer,
            RestaurantId = restaurant.Id,
            Restaurant = restaurant,
            CreatedAt = dto.CreatedAt,
            DeliveredAt = dto.DeliveredAt,
            Items = []
        };

        var createdOrder = await orderRepository.AddAsync(order);

        return MapToDto(createdOrder);
    }

    /// <inheritdoc />
    public async Task<OrderDto?> GetAsync(int id)
    {
        var order = await orderRepository.GetByIdAsync(id);

        return order is null ? null : MapToDto(order);
    }

    /// <inheritdoc />
    public async Task<IList<OrderDto>> GetAllAsync()
    {
        var orders = await orderRepository.GetAllAsync();

        return orders
            .Select(MapToDto)
            .ToList();
    }

    /// <inheritdoc />
    public async Task<OrderDto> UpdateAsync(
        OrderCreateUpdateDto dto,
        int id)
    {
        var order = await orderRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                $"Заказ с идентификатором {id} не найден");

        var customer = await customerRepository.GetByIdAsync(dto.CustomerId)
            ?? throw new KeyNotFoundException(
                $"Покупатель с идентификатором {dto.CustomerId} не найден");

        var restaurant = await restaurantRepository.GetByIdAsync(dto.RestaurantId)
            ?? throw new KeyNotFoundException(
                $"Ресторан с идентификатором {dto.RestaurantId} не найден");

        order.CustomerId = customer.Id;
        order.Customer = customer;
        order.RestaurantId = restaurant.Id;
        order.Restaurant = restaurant;
        order.CreatedAt = dto.CreatedAt;
        order.DeliveredAt = dto.DeliveredAt;

        var updatedOrder = await orderRepository.UpdateAsync(order)
            ?? throw new KeyNotFoundException(
                $"Заказ с идентификатором {id} не найден");

        return MapToDto(updatedOrder);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        return await orderRepository.DeleteAsync(id);
    }

    private static OrderDto MapToDto(Order order)
    {
        return new OrderDto(
            order.Id,
            order.CustomerId,
            order.RestaurantId,
            order.CreatedAt,
            order.DeliveredAt,
            order.TotalAmount,
            order.Items
                .Select(item => new OrderItemDto(
                    item.Id,
                    item.OrderId,
                    item.DishId,
                    item.Quantity))
                .ToList());
    }
}