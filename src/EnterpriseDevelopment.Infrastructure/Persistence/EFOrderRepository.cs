using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Domain.Entities;

namespace EnterpriseDevelopment.Infrastructure.Persistence;

/// <summary>
/// Репозиторий заказов на основе Entity Framework Core
/// </summary>
public class EfOrderRepository(ApplicationDbContext context)
    : EfRepository<Order>(context), IOrderRepository;
