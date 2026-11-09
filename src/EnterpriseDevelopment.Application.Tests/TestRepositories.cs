using EnterpriseDevelopment.Domain;
using EnterpriseDevelopment.Infrastructure.Repositories;
using EnterpriseDevelopment.Infrastructure.Storage;

namespace EnterpriseDevelopment.Application.Tests;

/// <summary>
/// Создаёт независимые репозитории и тестовое хранилище для каждого теста
/// </summary>
public class TestRepositories
{
    /// <summary>
    /// Создаёт набор репозиториев с начальными данными
    /// </summary>
    public TestRepositories(DataSeeder? seeder = null)
    {
        Store = new InMemoryDataStore(seeder ?? new DataSeeder());
        Restaurants = new RestaurantRepository(Store);
        Clients = new ClientRepository(Store);
        Categories = new CategoryRepository(Store);
        Dishes = new DishRepository(Store);
        Orders = new OrderRepository(Store);
    }

    /// <summary>
    /// Общее хранилище тестовых данных
    /// </summary>
    public InMemoryDataStore Store { get; }

    /// <summary>
    /// Репозиторий ресторанов
    /// </summary>
    public RestaurantRepository Restaurants { get; }

    /// <summary>
    /// Репозиторий клиентов
    /// </summary>
    public ClientRepository Clients { get; }

    /// <summary>
    /// Репозиторий категорий
    /// </summary>
    public CategoryRepository Categories { get; }

    /// <summary>
    /// Репозиторий блюд
    /// </summary>
    public DishRepository Dishes { get; }

    /// <summary>
    /// Репозиторий заказов
    /// </summary>
    public OrderRepository Orders { get; }
}
