using BeautySalon.Contracts.Interfaces;
using BeautySalon.Contracts.Appointments;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Application.Services;

/// <summary>
/// Управление записями посетителей
/// </summary>
/// <param name="appointments">Репозиторий записей</param>
/// <param name="customers">Репозиторий посетителей</param>
/// <param name="specialists">Репозиторий специалистов</param>
/// <param name="services">Репозиторий услуг</param>
public sealed class AppointmentService(
    IAppointmentRepository appointments,
    ICustomerRepository customers,
    ISpecialistRepository specialists,
    IServiceRepository services) : IAppointmentService
{
    /// <summary>
    /// Получить все записи
    /// </summary>
    public IReadOnlyList<AppointmentResponse> GetAll()
    {
        return appointments.GetAll().Select(ToResponse).ToList();
    }

    /// <summary>
    /// Получить запись по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public AppointmentResponse GetById(int id)
    {
        return ToResponse(appointments.GetById(id)
            ?? throw new KeyNotFoundException("Запись не найдена"));
    }

    /// <summary>
    /// Создать запись
    /// </summary>
    /// <param name="request">Данные записи</param>
    public AppointmentResponse Create(SaveAppointmentRequest request)
    {
        return ToResponse(appointments.Add(ToEntity(0, request)));
    }

    /// <summary>
    /// Изменить запись
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Новые данные записи</param>
    public void Update(int id, SaveAppointmentRequest request)
    {
        if (!appointments.Update(ToEntity(id, request)))
        {
            throw new KeyNotFoundException("Запись не найдена");
        }
    }

    /// <summary>
    /// Удалить запись
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public void Delete(int id)
    {
        if (!appointments.Delete(id))
        {
            throw new KeyNotFoundException("Запись не найдена");
        }
    }

    /// <summary>
    /// Получить записи посетителя
    /// </summary>
    /// <param name="id">Идентификатор посетителя</param>
    public IReadOnlyList<AppointmentResponse> GetByCustomer(int id)
    {
        if (customers.GetById(id) is null)
        {
            throw new KeyNotFoundException("Посетитель не найден");
        }

        return appointments.GetAll()
            .Where(appointment => appointment.Customer.Id == id)
            .Select(ToResponse)
            .ToList();
    }

    /// <summary>
    /// Получить записи специалиста
    /// </summary>
    /// <param name="id">Идентификатор специалиста</param>
    public IReadOnlyList<AppointmentResponse> GetBySpecialist(int id)
    {
        if (specialists.GetById(id) is null)
        {
            throw new KeyNotFoundException("Специалист не найден");
        }

        return appointments.GetAll()
            .Where(appointment => appointment.Specialist.Id == id)
            .Select(ToResponse)
            .ToList();
    }

    /// <summary>
    /// Получить записи на услугу
    /// </summary>
    /// <param name="id">Идентификатор услуги</param>
    public IReadOnlyList<AppointmentResponse> GetByService(int id)
    {
        if (services.GetById(id) is null)
        {
            throw new KeyNotFoundException("Услуга не найдена");
        }

        return appointments.GetAll()
            .Where(appointment => appointment.Service.Id == id)
            .Select(ToResponse)
            .ToList();
    }

    private Appointment ToEntity(int id, SaveAppointmentRequest request)
    {
        var start = request.AppointmentDateTime!.Value;

        if (start == DateTime.MinValue || start.Kind == DateTimeKind.Utc)
        {
            throw new ArgumentException("Укажите местные дату и время салона без суффикса Z");
        }

        return new Appointment
        {
            Id = id,
            Customer = customers.GetById(request.CustomerId!.Value)
                ?? throw new ArgumentException("Посетитель не найден"),
            Specialist = specialists.GetById(request.SpecialistId!.Value)
                ?? throw new ArgumentException("Специалист не найден"),
            Service = services.GetById(request.ServiceId!.Value)
                ?? throw new ArgumentException("Услуга не найдена"),
            AppointmentDateTime = start,
            IsRegularCustomer = request.IsRegularCustomer!.Value
        };
    }

    private static AppointmentResponse ToResponse(Appointment entity)
    {
        return new AppointmentResponse
        {
            Id = entity.Id,
            CustomerId = entity.Customer.Id,
            SpecialistId = entity.Specialist.Id,
            ServiceId = entity.Service.Id,
            AppointmentDateTime = entity.AppointmentDateTime,
            IsRegularCustomer = entity.IsRegularCustomer
        };
    }
}