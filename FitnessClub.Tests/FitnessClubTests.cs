namespace FitnessClub.Tests;

public class FitnessClubTests(FitnessClubFixture fixture) : IClassFixture<FitnessClubFixture>
{
    /// <summary>
    /// Проверяет выборку клиентов с истёкшим абонементом с сортировкой по ФИО.
    /// </summary>
    [Fact]
    public void ShouldReturnClientsWithExpiredSubscriptionsOrderedByName()
    {
        string[] expectedNames =
        [
            "Андреев Алексей Сергеевич",
            "Беляева Мария Олеговна",
            "Васильев Дмитрий Игоревич",
            "Громова Елена Павловна",
            "Денисов Павел Андреевич"
        ];

        var today = DateOnly.FromDateTime(fixture.ReferenceTime);

        var actualNames = fixture.Data.Clients
            .Where(client => client.SubscriptionEndDate < today)
            .OrderBy(client => client.FullName)
            .Select(client => client.FullName)
            .ToArray();

        Assert.Equal(expectedNames, actualNames);
    }

    /// <summary>
    /// Проверяет доступность выбранного зала в текущий момент времени.
    /// </summary>
    /// <param name="gymId">Идентификатор зала.</param>
    /// <param name="expectedAvailability">Ожидаемая доступность зала.</param>
    [Theory]
    [InlineData(2, false)]
    [InlineData(7, true)]
    public void ShouldDetermineGymAvailability(int gymId, bool expectedAvailability)
    {
        var now = fixture.ReferenceTime;

        var isAvailable = !fixture.Data.TrainingSessions.Any(session =>
            session.Gym.Id == gymId && session.EndsAt.HasValue && session.StartsAt <= now && now < session.EndsAt.Value);

        Assert.Equal(expectedAvailability, isAvailable);
    }

    /// <summary>
    /// Проверяет получение занятий текущего месяца для выбранного зала.
    /// </summary>
    [Fact]
    public void ShouldReturnCurrentMonthTrainingsForSelectedGym()
    {
        const int gymId = 1;

        int[] expectedSessionIds = [1, 2, 3];

        var actualSessionIds = fixture.Data.TrainingSessions
            .Where(session => session.Gym.Id == gymId &&
                session.StartsAt.Year == fixture.ReferenceTime.Year &&
                session.StartsAt.Month == fixture.ReferenceTime.Month)
            .Select(session => session.Id)
            .ToArray();

        Assert.Equal(expectedSessionIds, actualSessionIds);
    }

    /// <summary>
    /// Проверяет выборку тренеров со стажем не менее пяти лет.
    /// </summary>
    [Fact]
    public void ShouldReturnTrainersWithAtLeastFiveYearsExperience()
    {
        string[] expectedNames =
        [
            "Соколов Артём Викторович",
            "Крылова Наталья Сергеевна",
            "Попов Максим Ильич",
            "Лебедева Дарья Александровна",
            "Новиков Артур Валерьевич",
            "Павлова Светлана Юрьевна"
        ];

        var actualNames = fixture.Data.Trainers
            .Where(trainer => trainer.WorkExperience >= 5)
            .Select(trainer => trainer.FullName)
            .ToArray();

        Assert.Equal(expectedNames, actualNames);
    }

    /// <summary>
    /// Проверяет получение пяти тренеров с наибольшим количеством записей на занятия.
    /// </summary>
    [Fact]
    public void ShouldReturnFiveMostPopularTrainers()
    {
        int[] expectedTrainerIds = [11, 12, 13, 14, 15];

        var actualTrainerIds = fixture.Data.TrainingSessions
            .GroupBy(session => session.Trainer)
            .OrderByDescending(group => group.Count())
            .Take(5)
            .Select(group => group.Key.Id)
            .ToArray();

        Assert.Equal(expectedTrainerIds, actualTrainerIds);
    }
}
