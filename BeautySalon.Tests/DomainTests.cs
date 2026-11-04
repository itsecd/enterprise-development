using BeautySalon.Domain.Data;

namespace BeautySalon.Tests;

/// <summary>
/// Юнит-тесты аналитических запросов.
/// </summary>
public class DomainTests
{
    /// <summary>
    /// 1. Мастера со стажем не менее 5 лет
    /// </summary>
    [Fact]
    public void ShouldReturnOnlyMastersWithAtLeast5YearsExperience()
    {
        // Act
        var result = SeederData.Masters
            .Where(m => m.ExperienceYears >= 5)
            .ToList();

        // Assert
        Assert.Equal(8, result.Count);
        Assert.All(result, m => Assert.True(m.ExperienceYears >= 5));
        Assert.DoesNotContain(result, m => m.Id == 103);
    }

    /// </summary>
    /// 2. Окошки выбранного мастера
    /// </summary>
    [Fact]
    public void ShouldReturnPositiveTimeGapsBetweenMasterAppointments()
    {
        // Arrange
        var masterId = 101;
        var appointments = SeederData.Appointments
            .Where(a => a.MasterId == masterId)
            .OrderBy(a => a.StartTime)
            .ToList();

        // Act
        var gaps = new List<TimeSpan>();
        for (var i = 0; i < appointments.Count - 1; i++)
        {
            var current = appointments[i];
            var next = appointments[i + 1];
            var currentEnd = current.StartTime + current.Service.Duration;

            if (next.StartTime > currentEnd)
            {
                gaps.Add(next.StartTime - currentEnd);
            }
        }

        // Assert
        Assert.Equal(2, gaps.Count);
        Assert.Equal(TimeSpan.FromHours(1), gaps[0]);
        Assert.Equal(TimeSpan.FromHours(1), gaps[1]);
    }

    /// <summary>
    /// 3. Топ 5 популярных услуг
    /// </summary>
    [Fact]
    public void ShouldReturnTop5ServicesOrderedByAppointmentCount()
    {
        // Act
        var top5 = SeederData.Appointments
            .GroupBy(a => a.ServiceId)
            .Select(g => new
            {
                ServiceId = g.Key,
                Count = g.Count()
            })
            .OrderByDescending(x => x.Count)
            .Take(5)
            .ToList();

        // Assert
        Assert.Equal(5, top5.Count);

        // Топ: Стрижка(5), Маникюр(4), Массаж(3).
        Assert.Equal(201, top5[0].ServiceId);
        Assert.Equal(202, top5[1].ServiceId);
        Assert.Equal(203, top5[2].ServiceId);
        
        // Педикюр(2) и Ботокс(2) делят 4-5 места, порядок может быть разным.
        var top4And5 = top5.Skip(3).Select(x => x.ServiceId).ToList();
        Assert.Contains(204, top4And5);
        Assert.Contains(206, top4And5);

        Assert.Equal(5, top5[0].Count);
        Assert.Equal(4, top5[1].Count);
        Assert.Equal(3, top5[2].Count);
        Assert.Equal(2, top5[3].Count);
        Assert.Equal(2, top5[4].Count);
    }

    /// <summary>
    /// 4. Повторные записи клиентов за последний месяц
    /// </summary>
    [Fact]
    public void ShouldReturnRepeatAppointmentCountsForLastMonth()
    {
        // Arrange
        var lastMonth = DateTime.Now.AddDays(-30);

        // Act
        var repeatClients = SeederData.Appointments
            .Where(a => a.StartTime >= lastMonth)
            .GroupBy(a => a.ClientId)
            .Where(g => g.Count() > 1)
            .Select(g => new { ClientId = g.Key, Count = g.Count() })
            .ToList();

        // Assert        
        Assert.Equal(3, repeatClients.Count);
        Assert.Contains(repeatClients, rc => rc.ClientId == 1);
        Assert.Contains(repeatClients, rc => rc.ClientId == 2);
        Assert.Contains(repeatClients, rc => rc.ClientId == 3);
        Assert.DoesNotContain(repeatClients, rc => rc.ClientId == 4);
    }

    /// <summary>
    /// 5. Клиенты у нескольких мастеров, сортировка по дате рождения
    /// </summary>
    [Fact]
    public void ShouldReturnClientsWithMultipleMastersOrderedByBirthDate()
    {
        // Act
        var clients = SeederData.Appointments
            .GroupBy(a => a.ClientId)
            .Where(g => g.Select(a => a.MasterId).Distinct().Count() > 1)
            .Select(g => SeederData.Clients.First(c => c.Id == g.Key))
            .OrderBy(c => c.BirthDate)
            .ToList();

        // Assert
        Assert.Equal(3, clients.Count);
        Assert.Equal(1, clients[0].Id);
        Assert.Equal(2, clients[1].Id);
        Assert.Equal(3, clients[2].Id);
    }
}