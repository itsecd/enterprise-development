using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Domain.Entities;

namespace EnterpriseDevelopment.Infrastructure.Persistence;

/// <summary>
/// Репозиторий категорий блюд на основе Entity Framework Core
/// </summary>
public class EfCategoryRepository(ApplicationDbContext context)
    : EfRepository<DishCategory>(context), ICategoryRepository;
