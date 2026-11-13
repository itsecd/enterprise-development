using FoodDelivery.Application.Contracts.DTOs;
using FoodDelivery.Application.Contracts.Interfaces;
using FoodDelivery.Application.Interfaces;
using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Реализует прикладные операции над позициями заказов
/// </summary>
public class OrderItemService(
    IOrderItemRepository orderItemRepository,
    IOrderRepository orderRepository,
    IDishRepository dishRepository)
    : IOrderItemService
{
    /// <inheritdoc />
    public async Task<OrderItemDto> CreateAsync(
        OrderItemCreateUpdateDto dto)
    {
        var order = await orderRepository.GetByIdAsync(dto.OrderId)
            ?? throw new KeyNotFoundException(
                $"Заказ с идентификатором {dto.OrderId} не найден");

        var dish = await dishRepository.GetByIdAsync(dto.DishId)
            ?? throw new KeyNotFoundException(
                $"Блюдо с идентификатором {dto.DishId} не найдено");

        var item = new OrderItem
        {
            Id = 0,
            OrderId = order.Id,
            DishId = dish.Id,
            Dish = dish,
            Quantity = dto.Quantity
        };

        var created = await orderItemRepository.AddAsync(item);

        return MapToDto(created);
    }

    /// <inheritdoc />
    public async Task<OrderItemDto?> GetAsync(int id)
    {
        var item = await orderItemRepository.GetByIdAsync(id);

        return item is null ? null : MapToDto(item);
    }

    /// <inheritdoc />
    public async Task<IList<OrderItemDto>> GetAllAsync()
    {
        var orderItems = await orderItemRepository.GetAllAsync();

        return orderItems
            .Select(MapToDto)
            .ToList();
    }

    /// <inheritdoc />
    public async Task<OrderItemDto> UpdateAsync(
        OrderItemCreateUpdateDto dto,
        int id)
    {
        var item = await orderItemRepository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException(
                $"Позиция заказа с идентификатором {id} не найдена");

        var order = await orderRepository.GetByIdAsync(dto.OrderId)
            ?? throw new KeyNotFoundException(
                $"Заказ с идентификатором {dto.OrderId} не найден");

        var dish = await dishRepository.GetByIdAsync(dto.DishId)
            ?? throw new KeyNotFoundException(
                $"Блюдо с идентификатором {dto.DishId} не найдено");

        item.OrderId = order.Id;
        item.DishId = dish.Id;
        item.Dish = dish;
        item.Quantity = dto.Quantity;

        var updated = await orderItemRepository.UpdateAsync(item)
            ?? throw new KeyNotFoundException(
                $"Позиция заказа с идентификатором {id} не найдена");

        return MapToDto(updated);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        return await orderItemRepository.DeleteAsync(id);
    }

    private static OrderItemDto MapToDto(OrderItem item)
    {
        return new OrderItemDto(
            item.Id,
            item.OrderId,
            item.DishId,
            item.Quantity);
    }
}