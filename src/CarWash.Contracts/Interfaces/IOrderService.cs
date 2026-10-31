using CarWash.Contracts.Dto;

namespace CarWash.Contracts.Interfaces;

public interface IOrderService
{
    public Task<IEnumerable<OrderDto>> GetAllAsync();
    public Task<OrderDto?> GetByIdAsync(int id);
    public Task<OrderDto> CreateAsync(OrderEditDto editDto);
    public Task UpdateAsync(int id, OrderEditDto editDto);
    public Task DeleteAsync(int id);
}