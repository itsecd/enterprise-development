using FitnessClub.Domain.Entities;

namespace FitnessClub.Tests;

/// <summary>
/// Тесты LINQ-запросов для предметной области фитнес-клуба.
/// </summary>
public class QueriesTest(QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
{
    /// <summary>
    /// Проверяет получение тренеров со стажем не менее пяти лет.
    /// </summary>
    [Fact]
    public void GetExperiencedTrainers_ShouldReturnExpectedTrainers()
    {
        const int minimumExperience = 5;

        var expectedNames = new[]
        {
            "Алексеев Андрей Сергеевич",
            "Васильев Дмитрий Олегович",
            "Жуков Александр Михайлович",
            "Захарова Екатерина Романовна",
            "Ильин Роман Евгеньевич",
            "Кузнецов Алексей Николаевич",
            "Морозов Максим Викторович",
            "Смирнов Сергей Андреевич"
        };

        var trainers = fixture.Trainers
            .Where(trainer => trainer.WorkExperience >= minimumExperience)
            .OrderBy(trainer => trainer.FullName)
            .Select(trainer => trainer.FullName)
            .ToArray();

        Assert.Equal(expectedNames, trainers);
    }

    /// <summary>
    /// Проверяет доступность зала в заданный момент времени.
    /// Ожидаемый признак доступности зала.
    /// </summary>
    /// <param name="hallId">ID зала.</param>
    /// <param name="expectedAvailable">
    /// </param>
    [Theory]
    [InlineData(1, false)]
    [InlineData(2, true)]
    public void IsHallAvailable_ShouldReturnExpectedResult(
        int hallId,
        bool expectedAvailable)
    {
        var currentDateTime = fixture.TestDateTime;

        var isAvailable = !fixture.Trainings
            .Where(training => training.Hall.Id == hallId)
            .Any(training =>
                currentDateTime >= training.StartTime &&
                currentDateTime <
                training.StartTime.AddMinutes(training.DurationMinutes));

        Assert.Equal(expectedAvailable, isAvailable);
    }

    /// <summary>
    /// Проверяет получение клиентов с истёкшим абонементом.
    /// </summary>
    [Fact]
    public void GetExpiredClients_ShouldReturnClientsSortedByFullName()
    {
        var expectedNames = new[]
        {
            "Кузнецов Алексей Викторович",
            "Морозов Максим Николаевич",
            "Николаев Артём Дмитриевич",
            "Попов Дмитрий Олегович",
            "Сидорова Анна Сергеевна"
        };

        var clients = fixture.Clients
            .Where(client => client.SubscriptionEndDate < fixture.TestDateTime)
            .OrderBy(client => client.FullName)
            .Select(client => client.FullName)
            .ToArray();

        Assert.Equal(expectedNames, clients);
    }

    /// <summary>
    /// Проверяет получение занятий текущего месяца в выбранном зале.
    /// </summary>
    [Fact]
    public void GetCurrentMonthTrainings_ShouldReturnTrainingsInSelectedHall()
    {
        const int selectedHallId = 1;

        var monthStart = new DateTime(
            fixture.TestDateTime.Year,
            fixture.TestDateTime.Month,
            1);

        var nextMonthStart = monthStart.AddMonths(1);

        var trainings = fixture.Trainings
            .Where(training =>
                training.Hall.Id == selectedHallId &&
                training.StartTime >= monthStart &&
                training.StartTime < nextMonthStart)
            .OrderBy(training => training.StartTime)
            .ToList();

        Assert.Equal(4, trainings.Count);

        Assert.All(
            trainings,
            training => Assert.Equal(selectedHallId, training.Hall.Id));
    }

    /// <summary>
    /// Проверяет получение пяти наиболее популярных тренеров.
    /// </summary>
    [Fact]
    public void GetTopFiveTrainers_ShouldReturnExpectedTrainers()
    {
        var expectedNames = new[]
        {
            "Васильев Дмитрий Олегович",
            "Алексеев Андрей Сергеевич",
            "Жуков Александр Михайлович",
            "Захарова Екатерина Романовна",
            "Ильин Роман Евгеньевич"
        };

        var trainers = fixture.Trainings
            .GroupBy(training => training.Trainer)
            .Select(group => new
            {
                Trainer = group.Key,
                TrainingCount = group.Count()
            })
            .OrderByDescending(item => item.TrainingCount)
            .ThenBy(item => item.Trainer.FullName)
            .Take(5)
            .Select(item => item.Trainer.FullName)
            .ToArray();

        Assert.Equal(expectedNames, trainers);
    }
}