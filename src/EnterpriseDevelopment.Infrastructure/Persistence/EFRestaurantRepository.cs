using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Domain.Entities;

namespace EnterpriseDevelopment.Infrastructure.Persistence;

/// <summary>
/// Репозиторий ресторанов на основе Entity Framework Core
/// </summary>
public class EfRestaurantRepository(ApplicationDbContext context)
    : EfRepository<Restaurant>(context), IRestaurantRepository;
