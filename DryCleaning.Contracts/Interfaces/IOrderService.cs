using DryCleaning.Contracts.Dto;

namespace DryCleaning.Contracts.Interfaces;

public interface IOrderService
{
    Task<IReadOnlyList<OrderDto>> GetAllAsync();
    Task<IReadOnlyList<OrderDto>?> GetByClientIdAsync(int clientId);
    Task<OrderDto?> GetByIdAsync(int id);
    Task<OrderDto> AddAsync(OrderCreateDto order);
    Task<bool> UpdateAsync(int id, OrderEditDto order);
    Task<bool> DeleteAsync(int id);
    Task<IReadOnlyList<OrderDto>> GetInProgressAsync();
}
