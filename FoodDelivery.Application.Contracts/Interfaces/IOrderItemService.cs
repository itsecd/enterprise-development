using FoodDelivery.Application.Contracts.DTOs;

namespace FoodDelivery.Application.Contracts.Interfaces;

public interface IOrderItemService
    : IApplicationService<OrderItemDto, OrderItemCreateUpdateDto, int>
{
}