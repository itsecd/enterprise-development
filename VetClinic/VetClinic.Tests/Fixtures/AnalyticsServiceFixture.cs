using VetClinic.Application.Services;
using VetClinic.Infrastructure.SqlServer.Repositories;

namespace VetClinic.Tests.Fixtures;

/// <summary>
/// Сервис аналитики поверх одной базы на весь класс тестов, тесты данные только читают
/// </summary>
public sealed class AnalyticsServiceFixture : IAsyncDisposable
{
    private readonly TestDatabase _database = new();

    /// <summary>
    /// Сервис аналитики
    /// </summary>
    public AnalyticsService Service { get; }

    /// <summary>
    /// Создает сервис аналитики поверх EF-репозиториев
    /// </summary>
    public AnalyticsServiceFixture()
    {
        var context = _database.Context;
        Service = new AnalyticsService(
            new OwnerRepository(context),
            new PetRepository(context),
            new VetRepository(context),
            new AppointmentRepository(context));
    }

    /// <inheritdoc/>
    public ValueTask DisposeAsync() => _database.DisposeAsync();
}
