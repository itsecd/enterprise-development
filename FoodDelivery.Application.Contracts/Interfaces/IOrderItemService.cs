using FoodDelivery.Application.Contracts.DTOs;

namespace FoodDelivery.Application.Contracts.Interfaces;

/// <summary>
/// Прикладной сервис для работы с позициями заказов
/// </summary>
public interface IOrderItemService
    : IApplicationService<OrderItemDto, OrderItemCreateUpdateDto, int>
{
}