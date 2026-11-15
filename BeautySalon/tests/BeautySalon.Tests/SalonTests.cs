using BeautySalon.Application.Exceptions;

namespace BeautySalon.Tests;

/// <summary>
/// Тесты аналитических запросов (IAnalyticsService) на тестовых данных
/// </summary>
public class SalonTests(BeautySalonFixture fixture) : IClassFixture<BeautySalonFixture>
{
    private static CancellationToken Ct => TestContext.Current.CancellationToken;

    /// <summary>
    /// Вывести информацию о всех мастерах, стаж работы которых не менее 5 лет
    /// </summary>
    [Fact]
    public async Task GetExperiencedMasters_ExperienceAtLeast5Years_ReturnsMasters()
    {
        // Arrange
        var masters = fixture.DataStore.Masters;
        var expectedFullNames = new[]
        {
            masters[0].FullName,
            masters[4].FullName,
            masters[5].FullName,
            masters[6].FullName,
            masters[7].FullName,
            masters[8].FullName,
            masters[9].FullName
        };

        // Act
        var result = await fixture.Analytics.GetExperiencedMastersAsync(5, Ct);

        // Assert
        Assert.Equal(expectedFullNames, result.Select(m => m.FullName));
    }

    /// <summary>
    /// Вывести информацию о всех окошках выбранного мастера
    /// </summary>
    [Fact]
    public async Task GetFreeSlots_AppointmentsExist_ReturnsFreeTimeSlots()
    {
        // Arrange
        var expected = new List<(DateTime Start, DateTime End)>
        {
            (new DateTime(2024, 6, 1, 9, 0, 0), new DateTime(2024, 6, 1, 10, 0, 0)),
            (new DateTime(2024, 6, 1, 10, 30, 0), new DateTime(2024, 6, 1, 18, 0, 0))
        };

        // Act
        var result = await fixture.Analytics.GetFreeSlotsAsync(1, new DateOnly(2024, 6, 1), Ct);

        // Assert
        Assert.Equal(expected, result.Select(s => (s.Start, s.End)));
    }

    /// <summary>
    /// Для дня без записей свободно всё рабочее время
    /// </summary>
    [Fact]
    public async Task GetFreeSlots_NoAppointmentsOnDate_ReturnsWholeWorkDay()
    {
        var result = await fixture.Analytics.GetFreeSlotsAsync(1, new DateOnly(2024, 7, 1), Ct);

        var slot = Assert.Single(result);
        Assert.Equal(new DateTime(2024, 7, 1, 9, 0, 0), slot.Start);
        Assert.Equal(new DateTime(2024, 7, 1, 18, 0, 0), slot.End);
    }

    /// <summary>
    /// Для несуществующего мастера выбрасывается исключение «не найдено»
    /// </summary>
    [Fact]
    public async Task GetFreeSlots_MasterNotFound_ThrowsNotFound()
    {
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            fixture.Analytics.GetFreeSlotsAsync(999, new DateOnly(2024, 6, 1), Ct));

        Assert.Equal(999, exception.EntityId);
    }

    /// <summary>
    /// Вывести топ 5 наиболее популярных услуг
    /// </summary>
    [Fact]
    public async Task GetPopularServices_OrderedByAppointmentCount_ReturnsTop5()
    {
        // Arrange
        var expected = new[]
        {
            (Name: "Стрижка", Count: 2),
            (Name: "Маникюр", Count: 2),
            (Name: "Макияж", Count: 2),
            (Name: "Брови", Count: 2),
            (Name: "Ресницы", Count: 2)
        };

        // Act
        var result = await fixture.Analytics.GetPopularServicesAsync(5, Ct);

        // Assert
        Assert.Equal(expected, result.Select(s => (Name: s.ServiceName, Count: s.AppointmentsCount)));
    }

    /// <summary>
    /// Вывести информацию о клиентах с повторными записями за месяц
    /// </summary>
    [Fact]
    public async Task GetRepeatClients_HaveMultipleAppointmentsInMonth_ReturnsClients()
    {
        // Act
        var result = await fixture.Analytics.GetRepeatClientsAsync(2024, 6, Ct);

        // Assert
        var repeat = Assert.Single(result);
        Assert.Equal("Иванова Анна Ивановна", repeat.Client.FullName);
        Assert.Equal(2, repeat.AppointmentsCount);
    }

    /// <summary>
    /// В месяце без записей повторных клиентов нет
    /// </summary>
    [Fact]
    public async Task GetRepeatClients_MonthWithoutAppointments_ReturnsEmpty()
    {
        var result = await fixture.Analytics.GetRepeatClientsAsync(2024, 7, Ct);

        Assert.Empty(result);
    }

    /// <summary>
    /// Вывести информацию о клиентах, записанных к нескольким мастерам, упорядочить по дате рождения
    /// </summary>
    [Fact]
    public async Task GetClientsWithMultipleMasters_OrderedByBirthDate_ReturnsClients()
    {
        // Act
        var result = await fixture.Analytics.GetClientsWithMultipleMastersAsync(Ct);

        // Assert
        var client = Assert.Single(result);
        Assert.Equal("Иванова Анна Ивановна", client.FullName);
    }
}
