using CarWash.Domain.Data;
using CarWash.Domain.Entities;

namespace CarWash.Tests;

/// <summary>
/// Набор тестовых данных для проведения юнит-тестирования автомойки
/// </summary>
public class CarWashFixture
{
    /// <summary>
    /// Заказы из общих тестовых данных
    /// </summary>
    public IReadOnlyList<Order> Orders { get; } = DataSeed.Orders;
}
