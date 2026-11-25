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
    public async Task<IReadOnlyList<SpecialistResponse>> GetExperiencedSpecialistsAsync(CancellationToken cancellationToken = default)
    {
        return (await specialists.GetByMinimumExperienceAsync(5, cancellationToken))
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
    public async Task<IReadOnlyList<FreeWindowResponse>> GetFreeWindowsAsync(
        int specialistId, DateOnly date, CancellationToken cancellationToken = default)
    {
        if (await specialists.GetByIdAsync(specialistId, cancellationToken) is null)
        {
            throw new KeyNotFoundException("Специалист не найден");
        }

        if (date == DateOnly.MinValue || date == DateOnly.MaxValue)
        {
            throw new ArgumentException("Дата расписания указана некорректно");
        }

        var dayStart = date.ToDateTime(TimeOnly.MinValue);
        var dayEnd = dayStart.AddDays(1);

        var windows = await appointments.GetFreeWindowsAsync(specialistId, dayStart, dayEnd, cancellationToken);
        return windows.Select(window => new FreeWindowResponse
        {
            Start = window.Start,
            End = window.End
        }).ToList();
    }

    /// <summary>
    /// Получить пять услуг с наибольшим количеством записей
    /// </summary>
    public async Task<IReadOnlyList<PopularServiceResponse>> GetPopularServicesAsync(CancellationToken cancellationToken = default)
    {
        return (await appointments.GetPopularServicesAsync(5, cancellationToken))
            .Select(service => new PopularServiceResponse
            {
                ServiceId = service.ServiceId,
                Name = service.Name,
                AppointmentCount = service.AppointmentCount
            })
            .ToList();
    }

    /// <summary>
    /// Посчитать повторные записи за месяц с учётом всей истории посетителя
    /// </summary>
    /// <param name="asOf">Конец периода по местному времени салона</param>
    public async Task<RepeatAppointmentsResponse> GetRepeatAppointmentsAsync(DateTime asOf, CancellationToken cancellationToken = default)
    {
        if (asOf < DateTime.MinValue.AddMonths(1) ||
            asOf.Kind == DateTimeKind.Utc)
        {
            throw new ArgumentException("Конец периода указан некорректно");
        }

        var from = asOf.AddMonths(-1);

        var count = await appointments.CountRepeatAppointmentsAsync(from, asOf, cancellationToken);

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
    public async Task<IReadOnlyList<CustomerResponse>> GetCustomersWithMultipleSpecialistsAsync(CancellationToken cancellationToken = default)
    {
        return (await appointments.GetCustomersWithMultipleSpecialistsAsync(cancellationToken))
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
