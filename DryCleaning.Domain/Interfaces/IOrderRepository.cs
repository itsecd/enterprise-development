namespace DryCleaning.Domain.Interfaces;

public interface IOrderRepository
{
    Task<IReadOnlyList<Order>> GetAllAsync();
    Task<IReadOnlyList<Order>> GetByClientIdAsync(int clientId);
    Task<Order?> GetByIdAsync(int id);
    Task<Order> AddAsync(Order order);
    Task<bool> UpdateAsync(Order order);
    Task<bool> DeleteAsync(int id);
    Task<bool> HasOrdersForClientAsync(int clientId);
    Task<bool> HasOrdersForItemAsync(int itemId);
}
