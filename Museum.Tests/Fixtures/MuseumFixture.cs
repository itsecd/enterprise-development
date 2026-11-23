using Museum.Domain.Data;
using Museum.Application.Services;

namespace Museum.Tests.Fixtures;

/// <summary>
/// Предоставляет общий контекст музея с тестовыми данными.
/// </summary>
public class MuseumFixture
{
    /// <summary>Созданный контекст тестовых данных.</summary>
    public MuseumContext Context { get; }

    /// <summary>Сервис для аналитических тестов.</summary>
    public MuseumAnalyticsService AnalyticsService { get; }

    /// <summary>Создаёт фиксированные данные и аналитический сервис.</summary>
    public MuseumFixture()
    {
        Context = DataSeeder.Seed();
        AnalyticsService = new MuseumAnalyticsService(Context);
    }
}