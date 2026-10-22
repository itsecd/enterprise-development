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
    public OrderItemDto Create(OrderItemCreateUpdateDto dto)
    {
        var order = orderRepository.GetById(dto.OrderId)
            ?? throw new KeyNotFoundException(
                $"Заказ с идентификатором {dto.OrderId} не найден");

        var dish = dishRepository.GetById(dto.DishId)
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

        var created = orderItemRepository.Add(item);
        order.Items.Add(created);

        return MapToDto(created);
    }

    public OrderItemDto? Get(int id)
    {
        var item = orderItemRepository.GetById(id);
        return item is null ? null : MapToDto(item);
    }

    public IList<OrderItemDto> GetAll()
    {
        return orderItemRepository.GetAll()
            .Select(MapToDto)
            .ToList();
    }

    public OrderItemDto Update(OrderItemCreateUpdateDto dto, int id)
    {
        var item = orderItemRepository.GetById(id)
            ?? throw new KeyNotFoundException(
                $"Позиция заказа с идентификатором {id} не найдена");

        var order = orderRepository.GetById(dto.OrderId)
            ?? throw new KeyNotFoundException(
                $"Заказ с идентификатором {dto.OrderId} не найден");

        var dish = dishRepository.GetById(dto.DishId)
            ?? throw new KeyNotFoundException(
                $"Блюдо с идентификатором {dto.DishId} не найдено");

        if (item.OrderId != order.Id)
        {
            var oldOrder = orderRepository.GetById(item.OrderId);
            oldOrder?.Items.Remove(item);
            order.Items.Add(item);
        }

        item.OrderId = order.Id;
        item.DishId = dish.Id;
        item.Dish = dish;
        item.Quantity = dto.Quantity;

        var updated = orderItemRepository.Update(item)
            ?? throw new KeyNotFoundException(
                $"Позиция заказа с идентификатором {id} не найдена");

        return MapToDto(updated);
    }

    public bool Delete(int id)
    {
        var item = orderItemRepository.GetById(id);

        if (item is null)
        {
            return false;
        }

        var order = orderRepository.GetById(item.OrderId);
        order?.Items.Remove(item);

        return orderItemRepository.Delete(id);
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
