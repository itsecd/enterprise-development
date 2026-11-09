using EnterpriseDevelopment.Application.Repositories;
using EnterpriseDevelopment.Domain.Entities;
using EnterpriseDevelopment.Infrastructure.Storage;

namespace EnterpriseDevelopment.Infrastructure.Repositories;

/// <summary>
/// Репозиторий для хранения и управления ресторанами
/// </summary>
public class RestaurantRepository(InMemoryDataStore store)
    : Repository<Restaurant>(
        store.Restaurants,
        restaurant => restaurant.Id,
        (restaurant, id) => restaurant.Id = id),
      IRestaurantRepository;
