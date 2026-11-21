using CarWash.Application.Services;
using CarWash.Contracts.Interfaces;
using CarWash.Infrastructure.InMemory.Repositories;

namespace CarWash.Tests;

/// <summary>
/// Набор объектов для проведения юнит-тестирования автомойки.
/// Тесты работают с настоящим сервисом аналитики и тестовыми данными из Domain
/// </summary>
public class CarWashFixture
{
    /// <summary>
    /// Сервис аналитики, работающий с репозиторием заказов в памяти
    /// </summary>
    public IAnalyticsService AnalyticsService { get; } = new AnalyticsService(new OrderRepository());
}
