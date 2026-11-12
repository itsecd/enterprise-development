using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.InMemory.Repositories;

/// <summary>
/// Хранение сущностей Appointment в памяти
/// </summary>
/// <param name="storage">Общее хранилище салона</param>
public sealed class AppointmentRepository(SalonStorage storage) : IAppointmentRepository
{
    /// <inheritdoc />
    public IReadOnlyList<Appointment> GetAll()
    {
        lock (storage.SyncRoot)
        {
            return storage.Appointments.Select(EntityCopies.Copy).ToList();
        }
    }

    /// <inheritdoc />
    public Appointment? GetById(int id)
    {
        lock (storage.SyncRoot)
        {
            var entity = storage.Appointments.FirstOrDefault(item => item.Id == id);
            return entity is null ? null : EntityCopies.Copy(entity);
        }
    }

    /// <inheritdoc />
    public Appointment Add(Appointment entity)
    {
        lock (storage.SyncRoot)
        {
            var saved = EntityCopies.Copy(entity);
            saved.Customer = storage.Customers.FirstOrDefault(item => item.Id == saved.Customer.Id)
                ?? throw new ArgumentException("Посетитель не найден");
            saved.Specialist = storage.Specialists.FirstOrDefault(item => item.Id == saved.Specialist.Id)
                ?? throw new ArgumentException("Специалист не найден");
            saved.Service = storage.Services.FirstOrDefault(item => item.Id == saved.Service.Id)
                ?? throw new ArgumentException("Услуга не найдена");
            ValidateSchedule(saved, null);
            saved.Id = storage.NextId<Appointment>();
            storage.Appointments.Add(saved);
            return EntityCopies.Copy(saved);
        }
    }

    /// <inheritdoc />
    public bool Update(Appointment entity)
    {
        lock (storage.SyncRoot)
        {
            var current = storage.Appointments.FirstOrDefault(item => item.Id == entity.Id);
            if (current is null)
            {
                return false;
            }

            var saved = EntityCopies.Copy(entity);
            saved.Customer = storage.Customers.FirstOrDefault(item => item.Id == saved.Customer.Id)
                ?? throw new ArgumentException("Посетитель не найден");
            saved.Specialist = storage.Specialists.FirstOrDefault(item => item.Id == saved.Specialist.Id)
                ?? throw new ArgumentException("Специалист не найден");
            saved.Service = storage.Services.FirstOrDefault(item => item.Id == saved.Service.Id)
                ?? throw new ArgumentException("Услуга не найдена");
            ValidateSchedule(saved, entity.Id);
            current.Customer = saved.Customer;
            current.Specialist = saved.Specialist;
            current.Service = saved.Service;
            current.AppointmentDateTime = saved.AppointmentDateTime;
            current.IsRegularCustomer = saved.IsRegularCustomer;
            return true;
        }
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        lock (storage.SyncRoot)
        {

            return storage.Appointments.RemoveAll(item => item.Id == id) > 0;
        }
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

        var hasConflict = storage.Appointments.Any(existing =>
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
}