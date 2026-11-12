using FitnessGym.Application.Services;
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
    [InlineData("Кардио-зал", true)] // занятий с известным интервалом в зале ещё не было
    public async Task IsHallAvailableShouldBeDeterminedByOverlappingSession(string hallName, bool expectedAvailability)
    {
        // arrange
        var service = CreateService();

        // act
        var isHallAvailable = await service.IsHallAvailableAsync(hallName);

        // assert
        Assert.Equal(expectedAvailability, isHallAvailable);
    }

}
