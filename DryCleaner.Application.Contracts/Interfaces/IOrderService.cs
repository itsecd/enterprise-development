using DryCleaner.Application.Contracts.DTOs.Order;

namespace DryCleaner.Application.Contracts.Interfaces;

public interface IOrderService
    : IApplicationService<OrderDto, OrderCreateUpdateDto, int>
{
    IList<OrderDto> GetByCustomerId(int customerId);
    IList<OrderDto> GetByItemId(int itemId);
}