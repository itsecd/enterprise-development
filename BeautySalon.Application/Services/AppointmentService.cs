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
    public async Task<IReadOnlyList<AppointmentResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return (await appointments.GetAllAsync(cancellationToken)).Select(ToResponse).ToList();
    }

    /// <summary>
    /// Получить запись по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public async Task<AppointmentResponse> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return ToResponse(await appointments.GetByIdAsync(id, cancellationToken)
            ?? throw new KeyNotFoundException("Запись не найдена"));
    }

    /// <summary>
    /// Создать запись
    /// </summary>
    /// <param name="request">Данные записи</param>
    public async Task<AppointmentResponse> CreateAsync(SaveAppointmentRequest request, CancellationToken cancellationToken = default)
    {
        return ToResponse(await appointments.AddAsync(ToDetails(request), cancellationToken));
    }

    /// <summary>
    /// Изменить запись
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="request">Новые данные записи</param>
    public async Task UpdateAsync(int id, SaveAppointmentRequest request, CancellationToken cancellationToken = default)
    {
        if (!await appointments.UpdateAsync(id, ToDetails(request), cancellationToken))
        {
            throw new KeyNotFoundException("Запись не найдена");
        }
    }

    /// <summary>
    /// Удалить запись
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (!await appointments.DeleteAsync(id, cancellationToken))
        {
            throw new KeyNotFoundException("Запись не найдена");
        }
    }

    /// <summary>
    /// Получить записи посетителя
    /// </summary>
    /// <param name="id">Идентификатор посетителя</param>
    public async Task<IReadOnlyList<AppointmentResponse>> GetByCustomerAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await customers.GetByIdAsync(id, cancellationToken) is null)
        {
            throw new KeyNotFoundException("Посетитель не найден");
        }

        return (await appointments.GetByCustomerAsync(id, cancellationToken))
            .Select(ToResponse)
            .ToList();
    }

    /// <summary>
    /// Получить записи специалиста
    /// </summary>
    /// <param name="id">Идентификатор специалиста</param>
    public async Task<IReadOnlyList<AppointmentResponse>> GetBySpecialistAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await specialists.GetByIdAsync(id, cancellationToken) is null)
        {
            throw new KeyNotFoundException("Специалист не найден");
        }

        return (await appointments.GetBySpecialistAsync(id, cancellationToken))
            .Select(ToResponse)
            .ToList();
    }

    /// <summary>
    /// Получить записи на услугу
    /// </summary>
    /// <param name="id">Идентификатор услуги</param>
    public async Task<IReadOnlyList<AppointmentResponse>> GetByServiceAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await services.GetByIdAsync(id, cancellationToken) is null)
        {
            throw new KeyNotFoundException("Услуга не найдена");
        }

        return (await appointments.GetByServiceAsync(id, cancellationToken))
            .Select(ToResponse)
            .ToList();
    }

    private static AppointmentDetails ToDetails(SaveAppointmentRequest request)
    {
        var start = request.AppointmentDateTime!.Value;

        if (start == DateTime.MinValue || start.Kind == DateTimeKind.Utc)
        {
            throw new ArgumentException("Укажите местные дату и время салона без суффикса Z");
        }

        return new AppointmentDetails(
            request.CustomerId!.Value,
            request.SpecialistId!.Value,
            request.ServiceId!.Value,
            start,
            request.IsRegularCustomer!.Value);
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
