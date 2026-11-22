using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Domain.Entities;
using EnterpriseDevelopment.Infrastructure.Storage;

namespace EnterpriseDevelopment.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для хранения и управления блюдами
/// </summary>
public class DishRepository(InMemoryDataStore store)
    : Repository<Dish>(
        store.Dishes,
        dish => dish.Id,
        (dish, id) => dish.Id = id),
      IDishRepository;
