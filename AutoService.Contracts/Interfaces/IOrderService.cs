using AutoService.Contracts.Dtos;

namespace AutoService.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с заказами.
/// </summary>
public interface IOrderService
{
    public Task<IEnumerable<OrderDto>> GetAllAsync();
    public Task<OrderDto?> GetByIdAsync(Guid id);
    public Task<OrderDto> CreateAsync(OrderDto orderDto);
    public Task UpdateAsync(Guid id, OrderUpdateDto orderDto);
    public Task DeleteAsync(Guid id);
}