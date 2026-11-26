using VetClinic.Application.Services;
using VetClinic.Domain.Seeds;
using VetClinic.Infrastructure.InMemory.Repositories;

namespace VetClinic.Tests.Fixtures;

/// <summary>
/// Сервис аналитики поверх репозиториев в памяти, тесты данные только читают
/// </summary>
public sealed class AnalyticsServiceFixture
{
    /// <summary>
    /// Сервис аналитики
    /// </summary>
    public AnalyticsService Service { get; }

    /// <summary>
    /// Создает сервис аналитики поверх репозиториев в памяти
    /// </summary>
    public AnalyticsServiceFixture()
    {
        var seed = new VetClinicSeed();
        Service = new AnalyticsService(
            new OwnerRepository(seed),
            new PetRepository(seed),
            new VetRepository(seed),
            new AppointmentRepository(seed));
    }
}
