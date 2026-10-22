using FoodDelivery.Application.Contracts.DTOs;

namespace FoodDelivery.Application.Contracts.Interfaces;

public interface IOrderService
    : IApplicationService<OrderDto, OrderCreateUpdateDto, int>
{
}