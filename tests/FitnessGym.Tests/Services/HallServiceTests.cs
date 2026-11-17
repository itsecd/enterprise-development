using FitnessGym.Application.Services;
using FitnessGym.Domain.Entities;
using FitnessGym.Infrastructure.InMemory;

namespace FitnessGym.Tests.Services;

/// <summary>
/// Проверка доступности залов по времени проведения занятий.
/// </summary>
public class HallServiceTests(FitnessGymFixture fixture) : IClassFixture<FitnessGymFixture>
{
    private HallService CreateService() => new(
        new InMemoryTrainingSessionRepository(fixture.Sessions), () => fixture.Now);

    /// <summary>
    /// Должен определить, что зал недоступен, если в нём идёт занятие, пересекающееся по времени с искомым моментом,
    /// и доступен, если занятие уже закончилось или в зале ещё не было занятий.
    /// </summary>
    /// <param name="hallName">Название проверяемого зала.</param>
    /// <param name="expectedAvailability">Ожидаемая доступность зала.</param>
    [Theory]
    [InlineData("Зал групповых занятий", false)] // в зафиксированный момент в зале идёт занятие
    [InlineData("Зал единоборств", true)] // занятие в зале закончилось 10 минут назад
    [InlineData("Кардио-зал", true)] // в зафиксированный момент занятий в зале нет
    public async Task IsHallAvailableShouldBeDeterminedByOverlappingSession(string hallName, bool expectedAvailability)
    {
        // arrange
        var service = CreateService();

        // act
        var isHallAvailable = await service.IsHallAvailableAsync(hallName);

        // assert
        Assert.Equal(expectedAvailability, isHallAvailable);
    }

    /// <summary>
    /// Начало включено в интервал, окончание исключено; явное окончание имеет приоритет над 60 минутами.
    /// Занятие пересекает полночь, чтобы проверка не ограничивалась календарным днём.
    /// </summary>
    [Theory]
    [InlineData(null, -1, true)]
    [InlineData(null, 0, false)]
    [InlineData(null, 59, false)]
    [InlineData(null, 60, true)]
    [InlineData(30, -1, true)]
    [InlineData(30, 0, false)]
    [InlineData(30, 29, false)]
    [InlineData(30, 30, true)]
    [InlineData(90, 60, false)]
    [InlineData(90, 90, true)]
    public async Task IsHallAvailableShouldRespectEffectiveSessionEnd(
        int? durationMinutes, int minutesFromStart, bool expectedAvailability)
    {
        var start = new DateTime(2026, 11, 8, 23, 30, 0);
        var session = new TrainingSession
        {
            StartsAt = start,
            EndsAt = durationMinutes is { } duration ? start.AddMinutes(duration) : null,
            HallName = "Тестовый зал длительности",
            IsTrial = false,
        };
        var service = new HallService(new InMemoryTrainingSessionRepository([session]));
        var at = start.AddMinutes(minutesFromStart);

        Assert.Equal(expectedAvailability, await service.IsHallAvailableAsync(session.HallName, at));
        Assert.True(await service.IsHallAvailableAsync("Другой зал", at));
    }
}
