using BeautySalon.Contracts.Interfaces;
using BeautySalon.Contracts.Analytics;
using BeautySalon.Contracts.Customers;
using BeautySalon.Contracts.Specialists;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Application.Services;

/// <summary>
/// Аналитические запросы по данным салона
/// </summary>
/// <param name="appointments">Репозиторий записей</param>
/// <param name="specialists">Репозиторий специалистов</param>
public sealed class AnalyticsService(
    IAppointmentRepository appointments,
    ISpecialistRepository specialists) : IAnalyticsService
{
    /// <summary>
    /// Получить специалистов со стажем не менее пяти лет
    /// </summary>
    public IReadOnlyList<SpecialistResponse> GetExperiencedSpecialists()
    {
        return specialists.GetAll()
            .Where(specialist => specialist.WorkExperience >= 5)
            .OrderBy(specialist => specialist.Id)
            .Select(specialist => new SpecialistResponse
            {
                Id = specialist.Id,
                LastName = specialist.LastName,
                FirstName = specialist.FirstName,
                Patronymic = specialist.Patronymic,
                Gender = specialist.Gender,
                DateOfBirth = specialist.DateOfBirth,
                PhoneNumber = specialist.PhoneNumber,
                PassportNumber = specialist.PassportNumber,
                Specialization = specialist.Specialization,
                WorkExperience = specialist.WorkExperience
            })
            .ToList();
    }

    /// <summary>
    /// Получить промежутки между записями специалиста за день
    /// </summary>
    /// <param name="specialistId">Идентификатор специалиста</param>
    /// <param name="date">День расписания</param>
    public IReadOnlyList<FreeWindowResponse> GetFreeWindows(
        int specialistId, DateOnly date)
    {
        if (specialists.GetById(specialistId) is null)
        {
            throw new KeyNotFoundException("Специалист не найден");
        }

        if (date == DateOnly.MinValue || date == DateOnly.MaxValue)
        {
            throw new ArgumentException("Дата расписания указана некорректно");
        }

        var dayStart = date.ToDateTime(TimeOnly.MinValue);
        var dayEnd = dayStart.AddDays(1);

        var schedule = appointments.GetAll()
            .Where(appointment => appointment.Specialist.Id == specialistId)
            .Select(appointment => new
            {
                Start = appointment.AppointmentDateTime,
                End = appointment.AppointmentDateTime.AddMinutes(
                    appointment.Service.DurationMinutes)
            })
            .Where(interval => interval.Start < dayEnd && interval.End > dayStart)
            .OrderBy(interval => interval.Start)
            .ToList();

        var result = new List<FreeWindowResponse>();
        if (schedule.Count < 2)
        {
            return result;
        }

        var occupiedUntil = schedule[0].End;

        foreach (var interval in schedule.Skip(1))
        {
            if (interval.Start > occupiedUntil)
            {
                result.Add(new FreeWindowResponse
                {
                    Start = occupiedUntil,
                    End = interval.Start
                });
            }

            if (interval.End > occupiedUntil)
            {
                occupiedUntil = interval.End;
            }
        }

        return result;
    }

    /// <summary>
    /// Получить пять услуг с наибольшим количеством записей
    /// </summary>
    public IReadOnlyList<PopularServiceResponse> GetPopularServices()
    {
        return appointments.GetAll()
            .GroupBy(appointment => appointment.Service.Id)
            .Select(group => new PopularServiceResponse
            {
                ServiceId = group.Key,
                Name = group.First().Service.Name,
                AppointmentCount = group.Count()
            })
            .OrderByDescending(service => service.AppointmentCount)
            .ThenBy(service => service.ServiceId)
            .Take(5)
            .ToList();
    }

    /// <summary>
    /// Посчитать повторные записи за месяц с учётом всей истории посетителя
    /// </summary>
    /// <param name="asOf">Конец периода по местному времени салона</param>
    public RepeatAppointmentsResponse GetRepeatAppointments(DateTime asOf)
    {
        if (asOf < DateTime.MinValue.AddMonths(1) ||
            asOf.Kind == DateTimeKind.Utc)
        {
            throw new ArgumentException("Конец периода указан некорректно");
        }

        var from = asOf.AddMonths(-1);

        var count = appointments.GetAll()
            .GroupBy(appointment => appointment.Customer.Id)
            .SelectMany(group => group
                .OrderBy(appointment => appointment.AppointmentDateTime)
                .ThenBy(appointment => appointment.Id)
                .Skip(1))
            .Count(appointment =>
                appointment.AppointmentDateTime >= from &&
                appointment.AppointmentDateTime < asOf);

        return new RepeatAppointmentsResponse
        {
            From = from,
            To = asOf,
            Count = count
        };
    }

    /// <summary>
    /// Получить посетителей нескольких специалистов по дате рождения
    /// </summary>
    public IReadOnlyList<CustomerResponse> GetCustomersWithMultipleSpecialists()
    {
        return appointments.GetAll()
            .GroupBy(appointment => appointment.Customer.Id)
            .Where(group => group
                .Select(appointment => appointment.Specialist.Id)
                .Distinct()
                .Count() > 1)
            .Select(group => group.First().Customer)
            .OrderBy(customer => customer.DateOfBirth)
            .ThenBy(customer => customer.Id)
            .Select(customer => new CustomerResponse
            {
                Id = customer.Id,
                LastName = customer.LastName,
                FirstName = customer.FirstName,
                Patronymic = customer.Patronymic,
                Gender = customer.Gender,
                DateOfBirth = customer.DateOfBirth,
                PhoneNumber = customer.PhoneNumber
            })
            .ToList();
    }
}