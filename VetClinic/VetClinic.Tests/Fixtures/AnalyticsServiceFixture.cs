using VetClinic.Application.Services;
using VetClinic.Domain.Seeds;
using VetClinic.Infrastructure.InMemory.Repositories;

namespace VetClinic.Tests.Fixtures;

/// <summary>
/// Сервис аналитики с начальными данными, общий для всех тестов класса
/// </summary>
public sealed class AnalyticsServiceFixture
{
    /// <summary>
    /// Сервис аналитики
    /// </summary>
    public AnalyticsService Service { get; }

    /// <summary>
    /// Создаёт сервис аналитики поверх InMemory репозиториев с начальными данными
    /// </summary>
    public AnalyticsServiceFixture()
    {
        var seed = new VetClinicSeed();
        Service = new AnalyticsService(
            new OwnerRepository(seed),
            new VetRepository(seed),
            new AppointmentRepository(seed));
    }
}
