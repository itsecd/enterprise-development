using BeautySalon.Application.Services;
using BeautySalon.Domain.Entities;
using BeautySalon.Infrastructure.InMemory;
using BeautySalon.Infrastructure.InMemory.Repositories;

namespace BeautySalon.Tests;

/// <summary>
/// Проверки аналитических запросов на независимых данных
/// </summary>
public class QueriesTests
{
    private sealed class TestData
    {
        internal CustomerRepository Customers { get; }
        internal SpecialistRepository Specialists { get; }
        internal ServiceRepository Services { get; }
        internal AppointmentRepository Appointments { get; }
        internal AnalyticsService Analytics { get; }

        internal TestData()
        {
            var storage = new SalonStorage();
            Customers = new CustomerRepository(storage);
            Specialists = new SpecialistRepository(storage);
            Services = new ServiceRepository(storage);
            Appointments = new AppointmentRepository(storage);

            foreach (var appointment in Appointments.GetAll())
            {
                Appointments.Delete(appointment.Id);
            }

            Analytics = new AnalyticsService(Appointments, Specialists);
        }

        internal void Add(
            DateTime start,
            int customerId = 1,
            int specialistId = 2,
            int serviceId = 2)
        {
            Appointments.Add(new Appointment
            {
                Id = 0,
                Customer = Customers.GetById(customerId)!,
                Specialist = Specialists.GetById(specialistId)!,
                Service = Services.GetById(serviceId)!,
                AppointmentDateTime = start,
                IsRegularCustomer = false
            });
        }
    }

    /// <summary>
    /// Отбор включает стаж ровно пять лет и исключает меньший стаж
    /// </summary>
    [Fact]
    public void ExperiencedSpecialistsIncludeFiveYearsBoundary()
    {
        var data = new TestData();

        var result = data.Analytics.GetExperiencedSpecialists();

        Assert.Equal(
            new[] { 2, 3, 4, 5, 7, 8, 9 },
            result.Select(specialist => specialist.Id));
        Assert.Equal(5, result.Single(specialist => specialist.Id == 2).WorkExperience);
        Assert.DoesNotContain(result, specialist => specialist.Id == 1);
    }

    /// <summary>
    /// Окна учитывают длительность услуги и выбранное расписание
    /// </summary>
    [Fact]
    public void FreeWindowsUseServiceEndAndSelectedSchedule()
    {
        var data = new TestData();
        var day = new DateTime(2035, 3, 10);

        data.Add(day.AddHours(9), serviceId: 1);
        data.Add(day.AddHours(12));
        data.Add(day.AddHours(15));

        data.Add(day.AddHours(11), customerId: 2, specialistId: 3);
        data.Add(day.AddDays(1).AddHours(11));

        var result = data.Analytics.GetFreeWindows(
            2, DateOnly.FromDateTime(day));

        Assert.Equal(2, result.Count);
        Assert.Equal(day.AddHours(10).AddMinutes(30), result[0].Start);
        Assert.Equal(day.AddHours(12), result[0].End);
        Assert.Equal(90d, result[0].DurationMinutes);
        Assert.Equal(day.AddHours(13), result[1].Start);
        Assert.Equal(day.AddHours(15), result[1].End);
        Assert.Equal(120d, result[1].DurationMinutes);
    }

    /// <summary>
    /// Между соседними записями без промежутка окна нет
    /// </summary>
    [Fact]
    public void AdjacentAppointmentsDoNotProduceFreeWindow()
    {
        var data = new TestData();
        var start = new DateTime(2035, 3, 10, 9, 0, 0);
        data.Add(start);
        data.Add(start.AddHours(1));

        var result = data.Analytics.GetFreeWindows(
            2, DateOnly.FromDateTime(start));

        Assert.Empty(result);
    }

    /// <summary>
    /// Расписание отсутствующего специалиста нельзя получить
    /// </summary>
    [Fact]
    public void UnknownSpecialistIsRejected()
    {
        var data = new TestData();

        Assert.Throws<KeyNotFoundException>(() =>
            data.Analytics.GetFreeWindows(
                int.MaxValue, new DateOnly(2035, 3, 10)));
    }

    /// <summary>
    /// Популярные услуги ограничены пятью и имеют устойчивый порядок
    /// </summary>
    [Fact]
    public void PopularServicesAreOrderedByCountThenIdentifier()
    {
        var data = new TestData();
        var counts = new[] { 3, 4, 1, 3, 2, 1 };
        var day = new DateTime(2035, 4, 1);

        for (var index = 0; index < counts.Length; index++)
        {
            for (var visit = 0; visit < counts[index]; visit++)
            {
                data.Add(day.AddHours(10), serviceId: index + 1);
                day = day.AddDays(1);
            }
        }

        var result = data.Analytics.GetPopularServices();

        Assert.Equal(new[] { 2, 1, 4, 5, 3 },
            result.Select(service => service.ServiceId));
        Assert.Equal(new[] { 4, 3, 3, 2, 1 },
            result.Select(service => service.AppointmentCount));
        Assert.Equal(data.Services.GetById(2)!.Name, result[0].Name);
    }

    /// <summary>
    /// Первый визит до месяца не исключает повторные записи внутри месяца
    /// </summary>
    [Fact]
    public void RepeatAppointmentsUseHistoryBeforePeriod()
    {
        var data = new TestData();

        data.Add(new DateTime(2035, 1, 10, 10, 0, 0));
        data.Add(new DateTime(2035, 2, 5, 10, 0, 0));
        data.Add(new DateTime(2035, 2, 15, 10, 0, 0));
        data.Add(new DateTime(2035, 2, 20, 10, 0, 0), customerId: 2);

        var result = data.Analytics.GetRepeatAppointments(
            new DateTime(2035, 3, 1));

        Assert.Equal(2, result.Count);
        Assert.Equal(new DateTime(2035, 2, 1), result.From);
        Assert.Equal(new DateTime(2035, 3, 1), result.To);
    }

    /// <summary>
    /// Начало отчётного периода включается а конец исключается
    /// </summary>
    [Fact]
    public void RepeatAppointmentsRespectPeriodBoundaries()
    {
        var data = new TestData();

        data.Add(new DateTime(2035, 1, 1, 10, 0, 0));
        data.Add(new DateTime(2035, 1, 31, 10, 0, 0));
        data.Add(new DateTime(2035, 2, 1));
        data.Add(new DateTime(2035, 2, 28, 23, 0, 0));
        data.Add(new DateTime(2035, 3, 1));

        var result = data.Analytics.GetRepeatAppointments(
            new DateTime(2035, 3, 1));

        Assert.Equal(2, result.Count);
    }

    /// <summary>
    /// Несколько записей к одному специалисту не заменяют разных специалистов
    /// </summary>
    [Fact]
    public void CustomersWithMultipleSpecialistsAreOrderedByBirthDate()
    {
        var data = new TestData();
        var day = new DateTime(2035, 5, 1);

        data.Add(day.AddHours(9), customerId: 1);
        data.Add(day.AddDays(1).AddHours(9), customerId: 1);

        data.Add(day.AddHours(10), customerId: 2, specialistId: 2);
        data.Add(day.AddDays(1).AddHours(10), customerId: 2, specialistId: 3);

        data.Add(day.AddHours(11), customerId: 9, specialistId: 2);
        data.Add(day.AddDays(1).AddHours(11), customerId: 9, specialistId: 3);

        var result = data.Analytics.GetCustomersWithMultipleSpecialists();

        Assert.Equal(new[] { 9, 2 }, result.Select(customer => customer.Id));
        Assert.Equal(data.Customers.GetById(9)!.DateOfBirth, result[0].DateOfBirth);
    }

    /// <summary>
    /// Без записей аналитика возвращает пустые результаты и нулевой счётчик
    /// </summary>
    [Fact]
    public void EmptyScheduleProducesEmptyAnalytics()
    {
        var data = new TestData();

        Assert.Empty(data.Analytics.GetPopularServices());
        Assert.Empty(data.Analytics.GetCustomersWithMultipleSpecialists());
        Assert.Empty(data.Analytics.GetFreeWindows(2, new DateOnly(2035, 3, 10)));
        Assert.Equal(0, data.Analytics.GetRepeatAppointments(
            new DateTime(2035, 3, 1)).Count);
    }
}