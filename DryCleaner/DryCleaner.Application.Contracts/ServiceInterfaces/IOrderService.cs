using DryCleaner.Application.Contracts.Order;

namespace DryCleaner.Application.Contracts.ServiceInterfaces;

/// <summary>
/// Определяет контракт для работы с заказами
/// </summary>
public interface IOrderService : IAplicationService<OrderDto, OrderCreateUpdateDto, int>;
