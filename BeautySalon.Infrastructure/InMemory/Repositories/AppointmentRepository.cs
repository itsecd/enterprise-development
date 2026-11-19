using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий сущностей Appointment
/// </summary>
/// <param name="storage">Общее хранилище салона</param>
public sealed class AppointmentRepository(SalonStorage storage)
    : InMemoryRepository<Appointment>(storage, storage.Appointments, EntityCopies.Copy), IAppointmentRepository
{
    /// <inheritdoc />
    public Appointment Add(AppointmentDetails details)
    {
        return AddPrepared(() => ResolveAppointment(details, null));
    }

    /// <inheritdoc />
    public bool Update(int id, AppointmentDetails details)
    {
        return UpdatePrepared(id, current => ResolveAppointment(details, current.Id));
    }

    /// <inheritdoc />
    protected override void PrepareForSave(Appointment saved, Appointment? current)
    {
        var details = new AppointmentDetails(
            saved.Customer.Id,
            saved.Specialist.Id,
            saved.Service.Id,
            saved.AppointmentDateTime,
            saved.IsRegularCustomer);

        ApplyChanges(saved, ResolveAppointment(details, current?.Id));
    }

    private Appointment ResolveAppointment(AppointmentDetails details, int? existingId)
    {
        var appointment = new Appointment
        {
            Id = existingId ?? 0,
            Customer = Storage.Customers.FirstOrDefault(item => item.Id == details.CustomerId)
                ?? throw new KeyNotFoundException("Посетитель не найден"),
            Specialist = Storage.Specialists.FirstOrDefault(item => item.Id == details.SpecialistId)
                ?? throw new KeyNotFoundException("Специалист не найден"),
            Service = Storage.Services.FirstOrDefault(item => item.Id == details.ServiceId)
                ?? throw new KeyNotFoundException("Услуга не найдена"),
            AppointmentDateTime = details.Start,
            IsRegularCustomer = details.IsRegularCustomer
        };

        ValidateSchedule(appointment, existingId);
        return appointment;
    }

    /// <inheritdoc />
    protected override void ApplyChanges(Appointment current, Appointment saved)
    {
        current.Customer = saved.Customer;
        current.Specialist = saved.Specialist;
        current.Service = saved.Service;
        current.AppointmentDateTime = saved.AppointmentDateTime;
        current.IsRegularCustomer = saved.IsRegularCustomer;
    }

    private void ValidateSchedule(Appointment appointment, int? excludedId)
    {
        var start = appointment.AppointmentDateTime;
        var duration = appointment.Service.DurationMinutes;

        if (start == DateTime.MinValue || duration <= 0 ||
            start > DateTime.MaxValue.AddMinutes(-duration))
        {
            throw new ArgumentException("Дата или длительность услуги указана некорректно");
        }

        var end = start.AddMinutes(duration);

        var hasConflict = Storage.Appointments.Any(existing =>
            existing.Id != excludedId &&
            (existing.Specialist.Id == appointment.Specialist.Id ||
             existing.Customer.Id == appointment.Customer.Id) &&
            start < existing.AppointmentDateTime.AddMinutes(existing.Service.DurationMinutes) &&
            existing.AppointmentDateTime < end);

        if (hasConflict)
        {
            throw new InvalidOperationException("Посетитель или специалист занят в выбранное время");
        }
    }

    /// <inheritdoc />
    public IReadOnlyList<Appointment> GetByCustomer(int customerId)
    {
        lock (Storage.SyncRoot)
        {
            return Storage.Appointments
                .Where(appointment => appointment.Customer.Id == customerId)
                .Select(EntityCopies.Copy)
                .ToList();
        }
    }

    /// <inheritdoc />
    public IReadOnlyList<Appointment> GetBySpecialist(int specialistId)
    {
        lock (Storage.SyncRoot)
        {
            return Storage.Appointments
                .Where(appointment => appointment.Specialist.Id == specialistId)
                .Select(EntityCopies.Copy)
                .ToList();
        }
    }

    /// <inheritdoc />
    public IReadOnlyList<Appointment> GetByService(int serviceId)
    {
        lock (Storage.SyncRoot)
        {
            return Storage.Appointments
                .Where(appointment => appointment.Service.Id == serviceId)
                .Select(EntityCopies.Copy)
                .ToList();
        }
    }

    /// <inheritdoc />
    public IReadOnlyList<Appointment> GetSchedule(
        int specialistId, DateTime from, DateTime to)
    {
        lock (Storage.SyncRoot)
        {
            return Storage.Appointments
                .Where(appointment =>
                    appointment.Specialist.Id == specialistId &&
                    appointment.AppointmentDateTime < to &&
                    appointment.AppointmentDateTime.AddMinutes(
                        appointment.Service.DurationMinutes) > from)
                .OrderBy(appointment => appointment.AppointmentDateTime)
                .Select(EntityCopies.Copy)
                .ToList();
        }
    }

    /// <inheritdoc />
    public IReadOnlyList<ServicePopularity> GetPopularServices(int limit)
    {
        lock (Storage.SyncRoot)
        {
            return Storage.Appointments
                .GroupBy(appointment => appointment.Service.Id)
                .Select(group => new ServicePopularity(
                    group.Key, group.First().Service.Name, group.Count()))
                .OrderByDescending(service => service.AppointmentCount)
                .ThenBy(service => service.ServiceId)
                .Take(limit)
                .ToList();
        }
    }

    /// <inheritdoc />
    public int CountRepeatAppointments(DateTime from, DateTime to)
    {
        lock (Storage.SyncRoot)
        {
            return Storage.Appointments
                .GroupBy(appointment => appointment.Customer.Id)
                .SelectMany(group => group
                    .OrderBy(appointment => appointment.AppointmentDateTime)
                    .ThenBy(appointment => appointment.Id)
                    .Skip(1))
                .Count(appointment =>
                    appointment.AppointmentDateTime >= from &&
                    appointment.AppointmentDateTime < to);
        }
    }

    /// <inheritdoc />
    public IReadOnlyList<Customer> GetCustomersWithMultipleSpecialists()
    {
        lock (Storage.SyncRoot)
        {
            return Storage.Appointments
                .GroupBy(appointment => appointment.Customer.Id)
                .Where(group => group
                    .Select(appointment => appointment.Specialist.Id)
                    .Distinct()
                    .Count() > 1)
                .Select(group => group.First().Customer)
                .OrderBy(customer => customer.DateOfBirth)
                .ThenBy(customer => customer.Id)
                .Select(EntityCopies.Copy)
                .ToList();
        }
    }
}
