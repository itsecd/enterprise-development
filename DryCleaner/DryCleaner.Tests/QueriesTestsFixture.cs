using DryCleaner.Domain.Data;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Tests;

/// <summary>
/// Подготовленные данные дял тестов
/// </summary>
public class QueriesTestsFixture
{
    /// <summary>
    /// Списик категорий изделия
    /// </summary>
    public IReadOnlyList<ProductCategory> Categories { get; } = DataSeed.Categories;

    /// <summary>
    /// Список клиентов
    /// </summary>
    public IReadOnlyList<Client> Clients { get; } = DataSeed.Clients;

    /// <summary>
    /// Список изделий
    /// </summary>
    public IReadOnlyList<Product> Products { get; } = DataSeed.Products;

    /// <summary>
    /// Список заказов
    /// </summary>
    public IReadOnlyList<Order> Orders { get; } = DataSeed.Orders;
}
