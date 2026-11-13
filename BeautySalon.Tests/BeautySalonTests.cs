using BeautySalon.Domain.Data;
using BeautySalon.Domain.Models;
using Xunit;

namespace BeautySalon.Tests;

public sealed class BeautySalonQueriesTests
{
    private sealed record FreeSlot(DateTime Start, DateTime End, int DurationMinutes);

    private sealed record ServicePopularity(int ServiceId, string Name, int AppointmentCount);

    private sealed record ClientMasterInfo(
        int ClientId,
        string FullName,
        DateOnly BirthDate,
        int MasterCount);

    /// <summary>
    /// 1.Вывести информацию о всех мастерах, стаж работы которых не менее 5 лет.
    /// </summary>
    [Fact]
    public void Masters_WithExperienceAtLeastFive_AreReturned()
    {
        // Arrange
        var masters = DataSeed.Masters;
        var expectedIds = new[] { 11, 13, 14, 16, 18, 20 };

        // Act
        var actualIds = masters
            .Where(m => m.ExperienceYears >= 5)
            .OrderBy(m => m.Id)
            .Select(m => m.Id)
            .ToArray();

        // Assert
        Assert.Equal(expectedIds, actualIds);
    }

    /// <summary>
    /// 2.Вывести информацию о всех окошках 💅 выбранного мастера.
    /// </summary>
    [Fact]
    public void FreeSlots_ForSelectedMasterToday_AreReturnedBetweenAppointments()
    {
        // Arrange
        var master = DataSeed.Masters[0];
        var appointments = DataSeed.Appointments;
        var today = DateTime.Today;
        var servicesById = DataSeed.Services.ToDictionary(s => s.Id);
        var expectedSlots = new[]
        {
            new FreeSlot(today.AddHours(10), today.AddHours(11).AddMinutes(30), 90),
            new FreeSlot(today.AddHours(13), today.AddHours(13).AddMinutes(30), 30),
            new FreeSlot(today.AddHours(14).AddMinutes(30), today.AddHours(16), 90)
        };

        // Act
        var scheduled = appointments
            .Where(a => a.MasterId == master.Id && a.StartDateTime.Date == today)
            .Select(a => new
            {
                Start = a.StartDateTime,
                End = a.StartDateTime + servicesById[a.ServiceId].Duration
            })
            .OrderBy(a => a.Start)
            .ToList();

        var actualSlots = scheduled
            .Zip(scheduled.Skip(1), (current, next) => new
            {
                Start = current.End,
                End = next.Start
            })
            .Where(slot => slot.End > slot.Start)
            .Select(slot => new FreeSlot(
                slot.Start,
                slot.End,
                (int)(slot.End - slot.Start).TotalMinutes))
            .ToArray();

        // Assert
        Assert.Equal(expectedSlots, actualSlots);
    }

    /// <summary>
    /// 3.Вывести топ 5 наиболее популярных услуг.
    /// </summary>
    [Fact]
    public void Top5_PopularServices_AreReturnedByAppointmentCount()
    {
        // Arrange
        var appointments = DataSeed.Appointments;
        var services = DataSeed.Services;
        var expectedIds = new[] { 21, 22, 23, 24, 25 };
        var expectedCounts = new[] { 4, 3, 2, 2, 2 };

        // Act
        var actual = appointments
            .GroupBy(a => a.ServiceId)
            .Select(g => new { ServiceId = g.Key, Count = g.Count() })
            .Join(
                services,
                stats => stats.ServiceId,
                service => service.Id,
                (stats, service) => new ServicePopularity(service.Id, service.Name, stats.Count))
            .OrderByDescending(s => s.AppointmentCount)
            .ThenBy(s => s.ServiceId)
            .Take(5)
            .ToArray();

        // Assert
        Assert.Equal(expectedIds, actual.Select(s => s.ServiceId).ToArray());
        Assert.Equal(expectedCounts, actual.Select(s => s.AppointmentCount).ToArray());
    }

    /// <summary>
    /// 4.Вывести информацию о количестве повторных записей клиентов за последний месяц.
    /// </summary>
    [Fact]
    public void RepeatAppointments_LastMonth_AreCounted()
    {
        // Arrange
        var appointments = DataSeed.Appointments;
        var from = DateTime.Today.AddMonths(-1);
        var toExclusive = DateTime.Today.AddDays(1);
        var expectedRepeatClientIds = new[] { 1, 2, 3, 4, 8 };
        var expectedRepeatAppointments = 5;

        // Act
        var appointmentsInPeriod = appointments
            .Where(a => a.StartDateTime >= from && a.StartDateTime < toExclusive)
            .ToList();

        var repeatClientIds = appointmentsInPeriod
            .GroupBy(a => a.ClientId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .OrderBy(id => id)
            .ToArray();

        var additionalRepeatAppointments = appointmentsInPeriod
            .GroupBy(a => a.ClientId)
            .Where(g => g.Count() > 1)
            .Sum(g => g.Count() - 1);

        // Assert
        Assert.Equal(expectedRepeatClientIds, repeatClientIds);
        Assert.Equal(expectedRepeatAppointments, additionalRepeatAppointments);
    }

    /// <summary>
    /// 5. Вывести информацию о клиентах, записанных к нескольким мастерам, упорядочить по дате рождения.
    /// </summary>
    [Fact]
    public void Clients_BookedToSeveralMasters_AreOrderedByBirthDate()
    {
        // Arrange
        var appointments = DataSeed.Appointments;
        var clients = DataSeed.Clients;
        var expected = new[]
        {
            new ClientMasterInfo(4, "Кузнецов Сергей Павлович", new DateOnly(1975, 7, 30), 2),
            new ClientMasterInfo(2, "Петров Дмитрий Алексеевич", new DateOnly(1988, 11, 2), 2),
            new ClientMasterInfo(8, "Федоров Максим Юрьевич", new DateOnly(1990, 6, 18), 2),
            new ClientMasterInfo(1, "Иванова Мария Сергеевна", new DateOnly(1995, 5, 20), 2),
            new ClientMasterInfo(3, "Сидорова Анна Викторовна", new DateOnly(2000, 1, 15), 2)
        };

        // Act
        var actual = appointments
            .GroupBy(a => a.ClientId)
            .Select(g => new
            {
                ClientId = g.Key,
                MasterCount = g.Select(a => a.MasterId).Distinct().Count()
            })
            .Where(stats => stats.MasterCount > 1)
            .Join(
                clients,
                stats => stats.ClientId,
                client => client.Id,
                (stats, client) => new { Client = client, stats.MasterCount })
            .OrderBy(x => x.Client.BirthDate)
            .Select(x => new ClientMasterInfo(
                x.Client.Id,
                x.Client.FullName,
                x.Client.BirthDate,
                x.MasterCount))
            .ToArray();

        //Assert
        Assert.Equal(expected, actual);
    }
}