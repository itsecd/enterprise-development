using System.Net;
using System.Net.Http.Json;
using Aspire.Hosting;
using Aspire.Hosting.Testing;
using FitnesClub.Api.Contracts.Analytics;
using FitnesClub.Api.Contracts.Bookings;
using FitnesClub.Api.Contracts.Members;
using FitnesClub.Api.Contracts.Trainers;
using FitnesClub.Data.Data;

namespace FitnesClub.Tests;

/// <summary>
/// Фикстура для запуска инфраструктуры .NET Aspire (MS SQL Server + API) перед интеграционными тестами.
/// </summary>
public class AspireAppFixture : IAsyncLifetime
{
    public DistributedApplication App { get; private set; } = null!;
    public HttpClient ApiClient { get; private set; } = null!;

    public async ValueTask InitializeAsync()
    {
        var appHost = await DistributedApplicationTestingBuilder
            .CreateAsync<Projects.FitnesClub_AppHost>();

        App = await appHost.BuildAsync();
        await App.StartAsync();

        using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(3));
        await App.ResourceNotifications.WaitForResourceHealthyAsync("api", cts.Token);

        ApiClient = App.CreateHttpClient("api", "http");
    }

    public async ValueTask DisposeAsync()
    {
        ApiClient?.Dispose();
        if (App is not null)
        {
            await App.DisposeAsync();
        }
    }
}

/// <summary>
/// Интеграционные тесты аналитических запросов и CRUD API через оркестратор .NET Aspire и БД MS SQL Server.
/// </summary>
public class AspireIntegrationTests(AspireAppFixture fixture) : IClassFixture<AspireAppFixture>
{
    private static readonly DateTime _testDate = new(2026, 9, 28, 12, 0, 0);

    /// <summary>
    /// 1. Вывести информацию о всех тренерах, стаж работы которых не менее 5 лет (через API + БД).
    /// </summary>
    [Fact]
    public async Task GetExperiencedTrainers_ShouldReturnExpectedTrainersFromDatabase()
    {
        var expectedIds = new[]
        {
            DataSeeder.Trainers[0].Id,
            DataSeeder.Trainers[1].Id,
            DataSeeder.Trainers[2].Id,
            DataSeeder.Trainers[4].Id,
            DataSeeder.Trainers[6].Id,
            DataSeeder.Trainers[7].Id,
            DataSeeder.Trainers[8].Id
        };

        var response = await fixture.ApiClient.GetAsync(
            "/api/Analytics/experienced-trainers?minExperience=5",
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var actual = await response.Content.ReadFromJsonAsync<List<TrainerDto>>(TestContext.Current.CancellationToken);
        Assert.NotNull(actual);
        Assert.Equal(expectedIds.OrderBy(id => id), actual.Select(t => t.Id).OrderBy(id => id));
    }

    /// <summary>
    /// 2.1. Проверить доступность зала: зал свободен, когда нет активного занятия.
    /// </summary>
    [Fact]
    public async Task CheckRoomAvailability_ShouldReturnTrue_WhenNoCurrentBookingInDatabase()
    {
        var roomName = Uri.EscapeDataString("Зал №1");
        var dateStr = Uri.EscapeDataString(_testDate.ToString("O"));

        var response = await fixture.ApiClient.GetAsync(
            $"/api/Analytics/room-availability?roomName={roomName}&dateTime={dateStr}",
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<RoomAvailabilityDto>(TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.True(result.IsAvailable);
    }

    /// <summary>
    /// 2.2. Проверить доступность зала: зал занят во время идущей тренировки.
    /// </summary>
    [Fact]
    public async Task CheckRoomAvailability_ShouldReturnFalse_WhenBookingIsInProgressInDatabase()
    {
        var busyBooking = DataSeeder.Bookings[0];
        var busyTime = busyBooking.LessonDateTime.AddMinutes(busyBooking.Duration.TotalMinutes / 2);

        var roomName = Uri.EscapeDataString(busyBooking.RoomName);
        var dateStr = Uri.EscapeDataString(busyTime.ToString("O"));

        var response = await fixture.ApiClient.GetAsync(
            $"/api/Analytics/room-availability?roomName={roomName}&dateTime={dateStr}",
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var result = await response.Content.ReadFromJsonAsync<RoomAvailabilityDto>(TestContext.Current.CancellationToken);
        Assert.NotNull(result);
        Assert.False(result.IsAvailable);
    }

    /// <summary>
    /// 3. Вывести информацию о клиентах с просроченным абонементом, упорядочить по ФИО.
    /// </summary>
    [Fact]
    public async Task GetExpiredMemberships_ShouldReturnSortedMembersFromDatabase()
    {
        var expectedIds = new[]
        {
            DataSeeder.Members[6].Id,
            DataSeeder.Members[4].Id,
            DataSeeder.Members[8].Id,
            DataSeeder.Members[2].Id
        };

        var dateStr = Uri.EscapeDataString(_testDate.ToString("O"));
        var response = await fixture.ApiClient.GetAsync(
            $"/api/Analytics/expired-memberships?asOfDate={dateStr}",
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var actual = await response.Content.ReadFromJsonAsync<List<MemberDto>>(TestContext.Current.CancellationToken);
        Assert.NotNull(actual);
        Assert.Equal(expectedIds, actual.Select(m => m.Id).ToArray());
    }

    /// <summary>
    /// 4. Вывести информацию о занятиях за выбранный месяц в выбранном зале.
    /// </summary>
    [Fact]
    public async Task GetRoomBookings_ShouldReturnBookingsForSelectedMonthAndRoomFromDatabase()
    {
        var expectedIds = new[]
        {
            DataSeeder.Bookings[0].Id,
            DataSeeder.Bookings[2].Id
        };

        var roomName = Uri.EscapeDataString("Зал №1");
        var response = await fixture.ApiClient.GetAsync(
            $"/api/Analytics/room-bookings?roomName={roomName}&year={_testDate.Year}&month={_testDate.Month}",
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var actual = await response.Content.ReadFromJsonAsync<List<BookingDto>>(TestContext.Current.CancellationToken);
        Assert.NotNull(actual);
        Assert.Equal(expectedIds.OrderBy(id => id), actual.Select(b => b.Id).OrderBy(id => id));
    }

    /// <summary>
    /// 5. Вывести топ-5 наиболее популярных тренеров по количеству записей в БД.
    /// </summary>
    [Fact]
    public async Task GetTopFiveTrainers_ShouldReturnTopFiveTrainersFromDatabase()
    {
        var expectedIds = new[]
        {
            DataSeeder.Trainers[0].Id,
            DataSeeder.Trainers[2].Id,
            DataSeeder.Trainers[4].Id,
            DataSeeder.Trainers[1].Id,
            DataSeeder.Trainers[3].Id
        };

        var response = await fixture.ApiClient.GetAsync(
            "/api/Analytics/top-trainers",
            TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var actual = await response.Content.ReadFromJsonAsync<List<PopularTrainerDto>>(TestContext.Current.CancellationToken);
        Assert.NotNull(actual);
        Assert.Equal(5, actual.Count);
        Assert.Equal(expectedIds, actual.Select(x => x.Trainer.Id).ToArray());
    }

    /// <summary>
    /// 6. Проверка полного цикла CRUD-операций в БД через REST API.
    /// </summary>
    [Fact]
    public async Task CrudLifecycle_ForSpecialization_ShouldPersistChangesInDatabase()
    {
        var ct = TestContext.Current.CancellationToken;

        var createPayload = new { Name = "Тестовая специализация Aspire" };
        var postResponse = await fixture.ApiClient.PostAsJsonAsync("/api/Specializations", createPayload, ct);
        Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);

        var created = await postResponse.Content.ReadFromJsonAsync<FitnesClub.Domain.Entities.Specialization>(ct);
        Assert.NotNull(created);
        Assert.NotEqual(Guid.Empty, created.Id);

        var getResponse = await fixture.ApiClient.GetAsync($"/api/Specializations/{created.Id}", ct);
        Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);

        var updatePayload = new { Name = "Обновленная специализация Aspire" };
        var putResponse = await fixture.ApiClient.PutAsJsonAsync($"/api/Specializations/{created.Id}", updatePayload, ct);
        Assert.True(putResponse.IsSuccessStatusCode);

        var deleteResponse = await fixture.ApiClient.DeleteAsync($"/api/Specializations/{created.Id}", ct);
        Assert.True(deleteResponse.IsSuccessStatusCode);

        var getAfterDelete = await fixture.ApiClient.GetAsync($"/api/Specializations/{created.Id}", ct);
        Assert.Equal(HttpStatusCode.NotFound, getAfterDelete.StatusCode);
    }
}