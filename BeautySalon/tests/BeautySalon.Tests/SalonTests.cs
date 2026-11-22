namespace BeautySalon.Tests;


public class SalonTests(BeautySalonFixture fixture) : IClassFixture<BeautySalonFixture>
{
    /// <summary>
    /// Вывести информацию о всех мастерах, стаж работы которых не менее 5 лет
    /// </summary>
    [Fact]
    public void GetExperiencedMasters_ExperienceAtLeast5Years_ReturnsMasters()
    {
        // Arrange
        var expectedFullNames = new[]
        {
            fixture.Masters[0].FullName,
            fixture.Masters[4].FullName,
            fixture.Masters[5].FullName,
            fixture.Masters[6].FullName,
            fixture.Masters[7].FullName,
            fixture.Masters[8].FullName,
            fixture.Masters[9].FullName
        };

        // Act
        var result = fixture.Masters
            .Where(m => m.WorkExperience >= 5)
            .Select(m => m.FullName)
            .ToList();
       
        // Assert
        Assert.Equal(expectedFullNames, result);
    }

    /// <summary>
    /// Вывести информацию о всех окошках 💅 выбранного мастера
    /// </summary>
    [Fact]
    public void GetFreeTimeSlotsForMasterOnDate_AppointmentsExist_ReturnsFreeTimeSlots()
    {
        // Arrange
        var targetMaster = fixture.Masters[0];
        var targetDate = new DateTime(2024, 6, 1);

        var workStart = new DateTime(2024, 6, 1, 9, 0, 0);
        var workEnd = new DateTime(2024, 6, 1, 18, 0, 0);

        var expectedFreeSlots = new List<(DateTime Start, DateTime End)>
        {
            (new DateTime(2024, 6, 1, 9, 0, 0),  new DateTime(2024, 6, 1, 10, 0, 0)),
            (new DateTime(2024, 6, 1, 10, 30, 0), new DateTime(2024, 6, 1, 18, 0, 0))
        };

        var busySlots = fixture.Appointments
            .Where(a => a.Master == targetMaster && a.DateTime.Date == targetDate.Date)
            .OrderBy(a => a.DateTime)
            .Select(a => new { Start = a.DateTime, End = a.DateTime + a.Service.Duration })
            .ToList();

        // Act
        var freeSlots = new List<(DateTime Start, DateTime End)>();
        var currentPointer = workStart;

        foreach (var slot in busySlots)
        {
            if (slot.Start > currentPointer)
            {
                freeSlots.Add((currentPointer, slot.Start));
            }
            currentPointer = slot.End;
        }

        if (currentPointer < workEnd)
        {
            freeSlots.Add((currentPointer, workEnd));
        }

        // Assert
        Assert.Equal(expectedFreeSlots, freeSlots);
    }

    /// <summary>
    /// Вывести топ 5 наиболее популярных услуг
    /// </summary>
    [Fact]
    public void GetMostPopularServices_OrderedByAppointmentCount_ReturnsTop5()
    {
        // Arrange
        var expectedTopServices = new[]
        {
            (Name: "Стрижка", Count: 2),
            (Name: "Маникюр", Count: 2),
            (Name: "Макияж",  Count: 2),
            (Name: "Брови",   Count: 2),
            (Name: "Ресницы", Count: 2)
        };

        // Act
        var topServices = fixture.Appointments
            .GroupBy(a => a.Service.Name)
            .Select(g => new { ServiceName = g.Key, Count = g.Count() })
            .OrderByDescending(s => s.Count)
            .Take(5)
            .Select(s => (Name: s.ServiceName, Count: s.Count))
            .ToList();

        // Assert
        Assert.Equal(expectedTopServices, topServices);
    }

    /// <summary>
    /// Вывести информацию о количестве повторных записей клиентов за последний месяц
    /// </summary>
    [Fact]
    public void GetClientsWithRepeatVisitsInMonth_HaveMultipleAppointments_ReturnsClients()
    {
        // Arrange
        const int targetYear = 2024;
        const int targetMonth = 6;
        const string expectedFullName = "Иванова Анна Ивановна";

        // Act
        var repeatClients = fixture.Appointments
            .Where(a => a.DateTime.Year == targetYear && a.DateTime.Month == targetMonth)
            .GroupBy(a => a.Client)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        // Assert
        Assert.Single(repeatClients);
        Assert.Equal(expectedFullName, repeatClients[0].FullName);
    }

    /// <summary>
    /// Вывести информацию о клиентах, записанных к нескольким мастерам, упорядочить по дате рождения
    /// </summary>
    [Fact]
    public void GetClientsWhoVisitedMultipleMasters_OrderedByBirthDate_ReturnsClients()
    {
        // Arrange
        const string expectedFullName = "Иванова Анна Ивановна";

        // Act
        var clients = fixture.Appointments
            .GroupBy(a => a.Client)
            .Where(g => g.Select(a => a.Master).Distinct().Count() >= 2)
            .Select(g => g.Key)
            .OrderBy(c => c.BirthDate)
            .ToList();

        // Assert
        Assert.Single(clients);
        Assert.Equal(expectedFullName, clients[0].FullName);
    }
}
