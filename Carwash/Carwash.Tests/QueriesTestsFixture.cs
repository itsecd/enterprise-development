using Carwash.Domain.Data;
using Carwash.Domain.Entities;

namespace Carwash.Tests;

/// <summary>
/// Тестовые данные
/// </summary>
public class QueriesTestsFixture
{
    /// <summary>
    /// Клиенты
    /// </summary>
    public List<Client> Clients { get; } = DataSeed.Clients;

    /// <summary>
    /// Автомобили
    /// </summary>
    public List<Car> Cars { get; } = DataSeed.Cars;

    /// <summary>
    /// Услуги
    /// </summary>
    public List<Service> Services { get; } = DataSeed.Services;

    /// <summary>
    /// Заказы
    /// </summary>
    public List<Order> Orders { get; } = DataSeed.Orders;

    /// <summary>
    /// Время, относительно которого формируются тестовые данные
    /// </summary>
    public DateTime CurrentTime => DataSeed.CurrentTime;
}
