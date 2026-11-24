using DryCleaner.Application.Contracts.DTOs.Order;

namespace DryCleaner.Application.Contracts.Interfaces;

/// <summary>
/// Определяет стандартные операции CRUD для работы с заказами
/// </summary>
public interface IOrderService
    : IApplicationService<OrderDto, OrderCreateUpdateDto, int>
{
    Task<IList<OrderDto>> GetByCustomerIdAsync(int customerId);
    Task<IList<OrderDto>> GetByItemIdAsync(int itemId);
}