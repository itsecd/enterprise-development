using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.SqlServer.Repositories;

public sealed class AppointmentRepository(SalonDbContext context)
    : SqlRepository<Appointment>(context), IAppointmentRepository
{
    protected override IQueryable<Appointment> ReadQuery => base.ReadQuery
        .Include(item => item.Customer)
        .Include(item => item.Specialist)
        .Include(item => item.Service);

    public override Task<Appointment> AddAsync(
        Appointment entity, CancellationToken cancellationToken = default)
    {
        return AddAsync(ToDetails(entity), cancellationToken);
    }

    public override Task<bool> UpdateAsync(Appointment entity, CancellationToken cancellationToken = default)
    {
        return UpdateAsync(entity.Id, ToDetails(entity), cancellationToken);
    }

    public async Task<Appointment> AddAsync(AppointmentDetails details, CancellationToken cancellationToken = default)
    {
        await using var transaction = await Context.LockScheduleAsync(cancellationToken);
        var entity = await ResolveAsync(details, 0, cancellationToken);
        await base.AddAsync(entity, cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return entity;
    }

    public async Task<bool> UpdateAsync(int id, AppointmentDetails details, CancellationToken cancellationToken = default)
    {
        await using var transaction = await Context.LockScheduleAsync(cancellationToken);
        var current = await Entities.FindAsync([id], cancellationToken);
        if (current is null)
        {
            return false;
        }

        var entity = await ResolveAsync(details, id, cancellationToken);
        Context.Entry(current).CurrentValues.SetValues(entity);
        current.Customer = entity.Customer;
        current.Specialist = entity.Specialist;
        current.Service = entity.Service;

        var updated = await SaveAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return updated;
    }

    private async Task<Appointment> ResolveAsync(
        AppointmentDetails details, int id, CancellationToken cancellationToken)
    {
        var customer = await Context.Customers.FindAsync([details.CustomerId], cancellationToken)
            ?? throw new KeyNotFoundException("Посетитель не найден");
        var specialist = await Context.Specialists.FindAsync([details.SpecialistId], cancellationToken)
            ?? throw new KeyNotFoundException("Специалист не найден");
        var service = await Context.Services.FindAsync([details.ServiceId], cancellationToken)
            ?? throw new KeyNotFoundException("Услуга не найдена");

        var start = details.Start;
        var duration = service.DurationMinutes;
        if (start == DateTime.MinValue || duration <= 0 || start > DateTime.MaxValue.AddMinutes(-duration))
        {
            throw new ArgumentException("Дата или длительность услуги указана некорректно");
        }

        var end = start.AddMinutes(duration);
        var hasConflict = await Entities.AnyAsync(item =>
            item.Id != id &&
            (EF.Property<int>(item, "SpecialistId") == details.SpecialistId ||
             EF.Property<int>(item, "CustomerId") == details.CustomerId) &&
            item.AppointmentDateTime < end &&
            item.AppointmentDateTime.AddMinutes(item.Service.DurationMinutes) > start,
            cancellationToken);

        if (hasConflict)
        {
            throw new InvalidOperationException("Посетитель или специалист занят в выбранное время");
        }

        return new Appointment
        {
            Id = id,
            Customer = customer,
            Specialist = specialist,
            Service = service,
            AppointmentDateTime = start,
            IsRegularCustomer = details.IsRegularCustomer
        };
    }

    public async Task<IReadOnlyList<Appointment>> GetByCustomerAsync(
        int customerId, CancellationToken cancellationToken = default)
    {
        return await ReadQuery.Where(item => EF.Property<int>(item, "CustomerId") == customerId)
            .OrderBy(item => item.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Appointment>> GetBySpecialistAsync(
        int specialistId, CancellationToken cancellationToken = default)
    {
        return await ReadQuery.Where(item => EF.Property<int>(item, "SpecialistId") == specialistId)
            .OrderBy(item => item.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Appointment>> GetByServiceAsync(
        int serviceId, CancellationToken cancellationToken = default)
    {
        return await ReadQuery.Where(item => EF.Property<int>(item, "ServiceId") == serviceId)
            .OrderBy(item => item.Id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ScheduleWindow>> GetFreeWindowsAsync(
        int specialistId, DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        // Текущее занятое время учитывает все предыдущие записи специалиста за период
        return await Context.Database.SqlQuery<ScheduleWindow>(
            $"""
            WITH Schedule AS (
                SELECT a.Id, a.AppointmentDateTime AS StartsAt,
                    DATEADD(minute, s.DurationMinutes, a.AppointmentDateTime) AS EndsAt
                FROM Appointments AS a
                JOIN Services AS s ON s.Id = a.ServiceId
                WHERE a.SpecialistId = {specialistId} AND a.AppointmentDateTime < {to}
                    AND DATEADD(minute, s.DurationMinutes, a.AppointmentDateTime) > {from}
            ),
            Boundaries AS (
                SELECT MAX(EndsAt) OVER (ORDER BY StartsAt, Id ROWS UNBOUNDED PRECEDING) AS OccupiedUntil,
                    LEAD(StartsAt) OVER (ORDER BY StartsAt, Id) AS NextStart
                FROM Schedule
            )
            SELECT OccupiedUntil AS [Start], NextStart AS [End]
            FROM Boundaries
            WHERE NextStart > OccupiedUntil
            ORDER BY OccupiedUntil
            """).ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ServicePopularity>> GetPopularServicesAsync(
        int limit, CancellationToken cancellationToken = default)
    {
        return await Entities
            .GroupBy(item => new { ServiceId = EF.Property<int>(item, "ServiceId"), item.Service.Name })
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key.ServiceId)
            .Take(limit)
            .Select(group => new ServicePopularity(group.Key.ServiceId, group.Key.Name, group.Count()))
            .ToListAsync(cancellationToken);
    }

    public Task<int> CountRepeatAppointmentsAsync(DateTime from, DateTime to, CancellationToken cancellationToken = default)
    {
        return Entities.CountAsync(item =>
            item.AppointmentDateTime >= from && item.AppointmentDateTime < to &&
            Entities.Any(earlier =>
                EF.Property<int>(earlier, "CustomerId") == EF.Property<int>(item, "CustomerId") &&
                (earlier.AppointmentDateTime < item.AppointmentDateTime ||
                 (earlier.AppointmentDateTime == item.AppointmentDateTime && earlier.Id < item.Id))),
            cancellationToken);
    }

    public async Task<IReadOnlyList<Customer>> GetCustomersWithMultipleSpecialistsAsync(
        CancellationToken cancellationToken = default)
    {
        return await Context.Customers.AsNoTracking()
            .Where(customer => Entities
                .Where(item => EF.Property<int>(item, "CustomerId") == customer.Id)
                .Select(item => EF.Property<int>(item, "SpecialistId"))
                .Distinct()
                .Count() > 1)
            .OrderBy(customer => customer.DateOfBirth)
            .ThenBy(customer => customer.Id)
            .ToListAsync(cancellationToken);
    }

    private static AppointmentDetails ToDetails(Appointment entity)
    {
        return new AppointmentDetails(entity.Customer.Id, entity.Specialist.Id, entity.Service.Id,
            entity.AppointmentDateTime, entity.IsRegularCustomer);
    }
}
