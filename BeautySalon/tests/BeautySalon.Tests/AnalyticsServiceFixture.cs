using BeautySalon.Application.Services;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Infrastructure.InMemory;

namespace BeautySalon.Tests;

/// <summary>
/// Фикстура для тестов сервиса аналитики: in-memory данные из сида и сервис поверх репозиториев.
/// Данные только читаются, поэтому фикстуру можно разделять между тестами.
/// </summary>
public class AnalyticsServiceFixture
{
    /// <summary>
    /// Хранилище с тестовыми данными (для получения ожидаемых значений)
    /// </summary>
    public BeautySalonDataStore DataStore { get; } = new();

    /// <summary>
    /// Тестируемый сервис аналитики
    /// </summary>
    public IAnalyticsService Analytics { get; }

    /// <summary>
    /// Создаёт фикстуру и собирает сервис аналитики поверх репозиториев
    /// </summary>
    public AnalyticsServiceFixture()
    {
        Analytics = new AnalyticsService(
            new MasterRepository(DataStore),
            new AppointmentRepository(DataStore));
    }
}
