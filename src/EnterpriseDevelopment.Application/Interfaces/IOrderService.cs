using EnterpriseDevelopment.Contracts.DTOs.Orders;

namespace EnterpriseDevelopment.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для работы с заказами
/// </summary>
public interface IOrderService
    : ICrudService<OrderDto, CreateOrderDto, UpdateOrderDto>
{
}
