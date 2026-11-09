using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Domain.Entities;
using EnterpriseDevelopment.Infrastructure.Storage;

namespace EnterpriseDevelopment.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для хранения и управления категориями блюд
/// </summary>
public class CategoryRepository(InMemoryDataStore store)
    : Repository<DishCategory>(
        store.Categories,
        category => category.Id,
        (category, id) => category.Id = id),
      ICategoryRepository;
