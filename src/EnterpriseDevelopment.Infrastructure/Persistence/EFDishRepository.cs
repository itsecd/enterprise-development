using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Domain.Entities;

namespace EnterpriseDevelopment.Infrastructure.Persistence;

/// <summary>
/// Репозиторий блюд на основе Entity Framework Core
/// </summary>
public class EfDishRepository(ApplicationDbContext context)
    : EfRepository<Dish>(context), IDishRepository;
