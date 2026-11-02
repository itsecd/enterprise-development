namespace FitnessClub.Application.Tests;
/// <summary>
/// Класс тестов для запросов бронирования
/// </summary>
/// 
public class BookingQueriesTests(BookingQueriesFixture fixture) : IClassFixture<BookingQueriesFixture>
{
    /// <summary>
    /// Зафиксированный момент времени
    /// </summary>
    private readonly DateTime _checkTime = new(2026, 9, 15, 10, 30, 0);
    /// <summary>
    /// Вывести информацию о всех тренерах, стаж работы которых не менее 5 лет. 
    /// </summary>
    [Fact]
    public async Task ExperiencedTrainers_ShouldHaveAtLeast5YearsExperience()
    {
        var expectedTrainers = new[]
        {
            fixture.SeedData.Trainers[0],
            fixture.SeedData.Trainers[2],
            fixture.SeedData.Trainers[3],
            fixture.SeedData.Trainers[5],
            fixture.SeedData.Trainers[6],
            fixture.SeedData.Trainers[7],
            fixture.SeedData.Trainers[8],
            fixture.SeedData.Trainers[9]
        };

        var result = await fixture.AnalyticsService.GetExperiencedTrainersAsync();

        Assert.Equal(expectedTrainers.Select(trainer => trainer.FullName),
            result.Select(trainer => trainer.FullName));
        Assert.All(result, trainer => Assert.True(trainer.Experience >= 5));
    }
    /// <summary>
    /// Вывести информацию о клиентах, у которых просрочен абонемент, упорядочить по ФИО. 
    /// </summary>
    [Fact]
    public async Task ClientsWithOverdueSubscriptions_ShouldBeOrderedByFullName()
    {
        var checkDate = DateOnly.FromDateTime(_checkTime.Date);
        var expectedClients = new[]
        {
            fixture.SeedData.Clients[11],
            fixture.SeedData.Clients[10],
            fixture.SeedData.Clients[2],
            fixture.SeedData.Clients[4]
        };

        var result = await fixture.AnalyticsService
            .GetClientsWithOverdueSubscriptionsAsync(checkDate);

        Assert.Equal(expectedClients.Select(client => client.FullName),
            result.Select(client => client.FullName));
        Assert.All(result, client => Assert.True(client.SubscriptionEnd < checkDate));
    }
    /// <summary>
    /// Вывести топ 5 наиболее популярных тренеров.
    /// </summary>
    [Fact]
    public async Task MostPopularTrainers_ShouldBeOrderedByNumberOfBookings()
    {
        var result = await fixture.AnalyticsService.GetMostPopularTrainersAsync();

        Assert.Equal([3, 4, 2, 1, 7], result.Select(trainer => trainer.TrainerId));
        Assert.Equal([5, 4, 3, 2, 2], result.Select(trainer => trainer.BookingCount));
        Assert.Equal(
            [
                fixture.SeedData.Trainers[2].FullName,
                fixture.SeedData.Trainers[3].FullName,
                fixture.SeedData.Trainers[1].FullName,
                fixture.SeedData.Trainers[0].FullName,
                fixture.SeedData.Trainers[6].FullName
            ],
            result.Select(trainer => trainer.TrainerFullName));
    }
    /// <summary>
    /// Проверить, является ли зал доступным для записи в момент времени _checkTime. 
    /// </summary> dotnet add tests/FitnessClub.IntegrationTests/FitnessClub.IntegrationTests.csproj package Aspire.Hosting.Testing


    [Fact]
    public async Task Hall_ShouldBeUnavailable_WhenBookingOverlapsCheckTime()
    {
        var busyHallId = fixture.SeedData.Halls[4].Id;

        var isAvailable = await fixture.AnalyticsService
            .IsHallAvailableAsync(busyHallId, _checkTime);

        Assert.False(isAvailable);
    }
    /// <summary>
    /// Вывести информацию о занятиях за текущий месяц, проходящих в выбранном зале.
    /// </summary>
    [Fact]
    public async Task SessionsInTheChosenHall_ShouldBeFilteredByCurrentMonth()
    {
        var checkDate = DateOnly.FromDateTime(_checkTime.Date);
        var chosenHallId = fixture.SeedData.Halls[4].Id;
        var expectedBookingIds = new[]
        {
            fixture.SeedData.Bookings[10].Id,
            fixture.SeedData.Bookings[11].Id,
            fixture.SeedData.Bookings[12].Id
        };

        var result = await fixture.AnalyticsService
            .GetBookingsInHallForMonthAsync(chosenHallId, checkDate);

        Assert.Equal(expectedBookingIds, result.Select(booking => booking.Id));
    }
}