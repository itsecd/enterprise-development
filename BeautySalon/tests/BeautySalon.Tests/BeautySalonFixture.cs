using BeautySalon.Application.Services;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Infrastructure.InMemory;

namespace BeautySalon.Tests;

/// <summary>
/// Фикстура для аналитических тестов: in-memory данные из сида и реальный сервис аналитики поверх репозиториев.
/// Данные только читаются, поэтому фикстуру можно разделять между тестами.
/// </summary>
public class BeautySalonFixture
{
    /// <summary>
    /// Хранилище с тестовыми данными (для получения ожидаемых значений)
    /// </summary>
    public BeautySalonDataStore DataStore { get; } = new();

    /// <summary>
    /// Тестируемый сервис аналитики
    /// </summary>
    public IAnalyticsService Analytics { get; }

    public BeautySalonFixture()
    {
        Analytics = new AnalyticsService(
            new MasterRepository(DataStore),
            new AppointmentRepository(DataStore));
    }
}
