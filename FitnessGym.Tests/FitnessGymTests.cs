using FitnessGym.Domain.Enums;
using Xunit;

namespace FitnessGym.Tests;

/// <summary>
/// Тесты для проверки аналитических запросов предметной области «Фитнес-клуб»
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными</param>
public class FitnessGymTests(FitnessGymFixture fixture) : IClassFixture<FitnessGymFixture>
{
    /// <summary>
    /// Проверяет, что в результат попадают только тренеры со стажем не менее 5 лет
    /// </summary>
    [Fact]
    public void GetTrainersByExperience_MinimumFiveYears_ReturnsOnlyEligibleTrainers()
    {
        // arrange
        const int minimumExperience = 5;
        string[] expectedFullNames =
        [
            "Громова Елена Сергеевна",
            "Киселёва Ольга Андреевна",
            "Крылов Дмитрий Андреевич",
            "Николаев Артём Павлович",
            "Орлова Наталья Ивановна",
            "Полякова Марина Владимировна",
            "Романова Екатерина Дмитриевна",
            "Соловьёва Анна Михайловна",
            "Тихонов Игорь Алексеевич",
        ];

        // act
        var actualFullNames = fixture.Trainers
            .Where(t => t.ExperienceYears >= minimumExperience)
            .OrderBy(t => t.FullName)
            .Select(t => t.FullName)
            .ToList();

        // assert
        Assert.Equal(expectedFullNames, actualFullNames);
    }

    /// <summary>
    /// Проверяет, что зал считается недоступным, если в нём идёт занятие,
    /// и доступным, если занятие уже завершилось
    /// </summary>
    /// <param name="hall">Проверяемый зал</param>
    /// <param name="expectedAvailability">Ожидаемая доступность</param>
    [Theory]
    [InlineData(Hall.YogaRoom, false)] // занятие идёт прямо сейчас
    [InlineData(Hall.Ring, true)]      // занятие завершилось 15 минут назад
    public void CheckHallAvailability_HallAtCurrentMoment_ReturnsExpectedAvailability(Hall hall, bool expectedAvailability)
    {
        // arrange
        var now = fixture.Now;

        // act
        var isAvailable = !fixture.Sessions.Any(s => s.HallName == hall && s.IsInProgressAt(now));

        // assert
        Assert.Equal(expectedAvailability, isAvailable);
    }

    /// <summary>
    /// Проверяет, что возвращаются только клиенты с истёкшим абонементом,
    /// упорядоченные по ФИО
    /// </summary>
    [Fact]
    public void GetClientsWithExpiredSubscription_ExpiredSubscription_ReturnsClientsOrderedByFullName()
    {
        // arrange
        var today = fixture.Today;
        string[] expectedFullNames =
        [
            "Гусев Роман Валерьевич",
            "Ершова Полина Дмитриевна",
            "Медведев Артур Игоревич",
            "Соколова Виктория Андреевна",
            "Фролов Никита Сергеевич",
        ];

        // act
        var actualFullNames = fixture.Clients
            .Where(c => c.SubscriptionEnd < today)
            .OrderBy(c => c.FullName)
            .Select(c => c.FullName)
            .ToList();

        // assert
        Assert.Equal(expectedFullNames, actualFullNames);
    }

    /// <summary>
    /// Проверяет, что возвращаются только занятия выбранного зала за текущий месяц
    /// </summary>
    [Fact]
    public void GetCurrentMonthSessionsInHall_SelectedHall_ReturnsOnlyCurrentMonthSessions()
    {
        // arrange
        var selectedHall = Hall.Gym;
        var today = fixture.Today;
        int[] expectedSessionIds = [0, 1, 2];

        // act
        var actualSessionIds = fixture.Sessions
            .Where(s => s.HallName == selectedHall
                        && s.StartsTraining.Year == today.Year
                        && s.StartsTraining.Month == today.Month)
            .OrderBy(s => s.Id)
            .Select(s => s.Id)
            .ToList();

        // assert
        Assert.Equal(expectedSessionIds, actualSessionIds);
    }

    /// <summary>
    /// Проверяет, что возвращаются пять тренеров с наибольшим числом занятий,
    /// упорядоченные по убыванию популярности
    /// </summary>
    [Fact]
    public void GetTopFivePopularTrainers_AllSessions_ReturnsTopFiveTrainers()
    {
        // arrange
        string[] expectedFullNames =
        [
            "Тарасов Андрей Викторович",
            "Громова Елена Сергеевна",
            "Белов Максим Олегович",
            "Орлова Наталья Ивановна",
            "Крылов Дмитрий Андреевич",
        ];

        // act
        var actualFullNames = fixture.Sessions
            .GroupBy(s => s.Trainer)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key.FullName)
            .ToList();

        // assert
        Assert.Equal(expectedFullNames, actualFullNames);
    }
}