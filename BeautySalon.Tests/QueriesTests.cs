using BeautySalon.Domain.Data;

namespace BeautySalon.Tests;

/// <summary>
/// Unit-тесты запросов к данным салона красоты
/// </summary>
public class QueriesTests
{
    /// <summary>
    /// 1. Вывести информацию обо всех специалистах, стаж работы которых не менее 5 лет
    /// </summary>
    [Fact]
    public void GetSpecialistsWithFiveOrMoreYearsOfExperience()
    {
        var result = BeautySalonData.Specialists
            .Where(specialist => specialist.WorkExperience >= 5)
            .ToList();

        Assert.Equal(7, result.Count);
        Assert.Contains(result, specialist => specialist.Id == 5);
    }

    /// <summary>
    /// 2. Вывести информацию обо всех окошках выбранного специалиста
    /// Окошком считается промежуток между окончанием одной записи и началом следующей
    /// </summary>
    [Fact]
    public void GetFreeWindowsForSelectedSpecialist()
    {
        var selectedSpecialistId = 2;
        var selectedDate = DateTime.Today.AddDays(2).Date;

        var appointments = BeautySalonData.Appointments
            .Where(appointment => appointment.Specialist.Id == selectedSpecialistId &&
                                  appointment.AppointmentDateTime.Date == selectedDate)
            .OrderBy(appointment => appointment.AppointmentDateTime)
            .ToList();

        var windows = appointments
            .Zip(appointments.Skip(1), (first, second) =>
            {
                var firstEnd = first.AppointmentDateTime.AddMinutes(first.Service.DurationMinutes);
                return new
                {
                    Start = firstEnd,
                    End = second.AppointmentDateTime,
                    Duration = second.AppointmentDateTime - firstEnd
                };
            })
            .Where(window => window.Duration > TimeSpan.Zero)
            .ToList();

        Assert.Equal(3, windows.Count);
        Assert.Equal(TimeSpan.FromHours(2), windows[0].Duration);
        Assert.Equal(TimeSpan.FromHours(4), windows[1].Duration);
        Assert.Equal(TimeSpan.FromHours(1.5), windows[2].Duration);
    }

    /// <summary>
    /// 3. Вывести топ 5 наиболее популярных услуг
    /// </summary>
    [Fact]
    public void GetTopFivePopularServices()
    {
        var result = BeautySalonData.Appointments
            .GroupBy(appointment => appointment.Service.Id)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key)
            .Take(5)
            .Select(group => group.Key)
            .ToList();

        Assert.Equal(new[] { 1, 2, 4, 6, 7 }, result);
    }

    /// <summary>
    /// 4. Вывести информацию о количестве повторных записей посетителей за последний месяц
    /// </summary>
    [Fact]
    public void GetNumberOfRepeatedAppointmentsDuringLastMonth()
    {
        var startDate = DateTime.Today.AddMonths(-1);
        var endDate = DateTime.Now;

        var lastMonthAppointments = BeautySalonData.Appointments
            .Where(appointment => appointment.AppointmentDateTime >= startDate &&
                                  appointment.AppointmentDateTime <= endDate)
            .ToList();

        var repeatAppointmentsCount = lastMonthAppointments
            .GroupBy(appointment => appointment.Customer.Id)
            .Sum(group => Math.Max(0, group.Count() - 1));

        Assert.Equal(15, repeatAppointmentsCount);
        Assert.DoesNotContain(lastMonthAppointments, appointment => appointment.Id == 8);
        Assert.Contains(BeautySalonData.Appointments, appointment => appointment.Id == 8);
    }

    /// <summary>
    /// 5. Вывести информацию о посетителях, записанных к нескольким специалистам, упорядочить по дате рождения
    /// </summary>
    [Fact]
    public void GetCustomersAssignedToSeveralSpecialistsOrderedByBirthDate()
    {
        var result = BeautySalonData.Appointments
            .GroupBy(appointment => appointment.Customer.Id)
            .Where(group => group.Select(appointment => appointment.Specialist.Id).Distinct().Count() > 1)
            .Select(group => group.First().Customer)
            .OrderBy(customer => customer.DateOfBirth)
            .ToList();

        Assert.Equal(5, result.Count);
        Assert.Equal(new[] { 9, 5, 4, 8, 2 }, result.Select(customer => customer.Id));
    }
}
