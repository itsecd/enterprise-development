using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Domain.Entities;
using EnterpriseDevelopment.Infrastructure.Storage;

namespace EnterpriseDevelopment.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для хранения и управления заказами
/// </summary>
public class OrderRepository(InMemoryDataStore store)
    : Repository<Order>(
        store.Orders,
        order => order.Id,
        (order, id) => order.Id = id),
      IOrderRepository;
