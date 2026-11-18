using FitnessClub.Domain.Interfaces;
using FitnessClub.Infrastructure.PostgreSql.Data;
using FitnessClub.Tests.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace FitnessClub.Tests;

/// <summary>
/// Интеграционные тесты
/// </summary>
[Collection("PostgreSql")]
public class FitnessClubTests(PostgresFixture postgres)
{
    /// <summary>
    /// Опорная текущая дата тестов — дата, относительно которой составлены seed-данные
    /// </summary>
    private static DateTime Today => SeedData.SeedDate.Date;

    /// <summary>
    /// Проверка: вывод информации о всех тренерах, стаж работы которых не менее 5 лет
    /// </summary>
    [Fact]
    public async Task TrainersWithExperienceAtLeast5Years_ReturnsSortedByFullName()
    {
        string[] expected =
        [
            "Волкова Анна Александровна",
            "Кузнецова Мария Сергеевна",
            "Лебедев Артём Олегович",
            "Новикова Ольга Дмитриевна",
            "Попова Елена Викторовна",
            "Смирнов Алексей Петрович",
        ];

        using var scope = await postgres.CreateScopeAsync();
        var trainers = await scope.ServiceProvider
            .GetRequiredService<ITrainerRepository>()
            .ReadAll();

        var actual = trainers
            .Where(t => t.ExperienceYears >= 5)
            .OrderBy(t => t.FullName)
            .Select(t => t.FullName)
            .ToArray();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Проверка: вывод информации о клиентах с просроченным абонементом, упорядоченный по ФИО
    /// </summary>
    [Fact]
    public async Task ClientsWithExpiredSubscription_ReturnsOrderedByName()
    {
        string[] expected =
        [
            "Гусев Андрей Юрьевич",
            "Зайцев Роман Викторович",
            "Львов Григорий Артёмович",
            "Петров Пётр Сергеевич",
            "Сидоров Николай Петрович",
        ];

        using var scope = await postgres.CreateScopeAsync();
        var clients = await scope.ServiceProvider
            .GetRequiredService<IClientRepository>()
            .ReadAll();

        var actual = clients
            .Where(c => c.SubscriptionEnd < Today)
            .OrderBy(c => c.FullName)
            .Select(c => c.FullName)
            .ToArray();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Проверка того, является ли зал доступным для записи в данный момент
    /// </summary>
    [Theory]
    [InlineData(7, 9, 59, true)]
    [InlineData(7, 10, 0, false)]
    [InlineData(7, 11, 29, false)]
    [InlineData(7, 11, 30, true)]
    [InlineData(7, 12, 0, true)]
    public async Task HallAvailability_MatchesSchedule(int day, int hour, int minute, bool expected)
    {
        using var scope = await postgres.CreateScopeAsync();
        var lessons = await scope.ServiceProvider
            .GetRequiredService<ILessonRepository>()
            .ReadAll();

        bool IsHallAvailable(string hall, DateTime time) =>
            !lessons.Any(l =>
                l.Hall == hall &&
                l.DateStart <= time &&
                time < l.DateEnd);

        var time = new DateTime(Today.Year, Today.Month, day, hour, minute, 0);

        Assert.Equal(expected, IsHallAvailable("Зал единоборств", time));
    }

    /// <summary>
    /// Проверка: вывод информации о занятиях за текущий месяц, проводимых в этом зале
    /// </summary>
    [Fact]
    public async Task LessonsOfCurrentMonthInSelectedHall_ReturnsSortedByDate()
    {
        string[] expected =
        [
            "Кузнецова Мария Сергеевна",
            "Кузнецова Мария Сергеевна",
            "Попова Елена Викторовна",
        ];

        using var scope = await postgres.CreateScopeAsync();
        var lessons = await scope.ServiceProvider
            .GetRequiredService<ILessonRepository>()
            .ReadAll();

        var actual = lessons
            .Where(l => l.Hall == "Зал йоги" &&
                        l.DateStart.Year == Today.Year &&
                        l.DateStart.Month == Today.Month)
            .OrderBy(l => l.DateStart)
            .Select(l => l.Trainer.FullName)
            .ToArray();

        Assert.Equal(expected, actual);
    }

    /// <summary>
    /// Проверка: вывод топ 5 наиболее популярных тренеров
    /// </summary>
    [Fact]
    public async Task Top5PopularTrainers_ReturnsOrderedByLessonsCount()
    {
        string[] expected =
        [
            "Иванов Пётр Иванович",
            "Смирнов Алексей Петрович",
            "Кузнецова Мария Сергеевна",
            "Соколов Дмитрий Андреевич",
            "Попова Елена Викторовна",
        ];
        int[] expectedCounts = [6, 4, 3, 2, 1];

        using var scope = await postgres.CreateScopeAsync();
        var lessons = await scope.ServiceProvider
            .GetRequiredService<ILessonRepository>()
            .ReadAll();

        var top = lessons
            .GroupBy(l => l.Trainer)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .ToArray();

        Assert.Equal(expected, top.Select(g => g.Key.FullName).ToArray());
        Assert.Equal(expectedCounts, top.Select(g => g.Count()).ToArray());
    }
}
