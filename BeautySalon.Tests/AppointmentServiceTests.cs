using BeautySalon.Application.Services;
using BeautySalon.Contracts.Appointments;
using BeautySalon.Infrastructure.InMemory;
using BeautySalon.Infrastructure.InMemory.Repositories;

namespace BeautySalon.Tests;

/// <summary>
/// Проверки создания и изменения записей
/// </summary>
public class AppointmentServiceTests
{
    private static AppointmentService CreateService()
    {
        var storage = new SalonStorage();
        return new AppointmentService(
            new AppointmentRepository(storage),
            new CustomerRepository(storage),
            new SpecialistRepository(storage),
            new ServiceRepository(storage));
    }

    private static SaveAppointmentRequest CreateRequest(
        DateTime start, int customerId = 1, int specialistId = 2)
    {
        return new SaveAppointmentRequest
        {
            CustomerId = customerId,
            SpecialistId = specialistId,
            ServiceId = 2,
            AppointmentDateTime = start,
            IsRegularCustomer = false
        };
    }

    /// <summary>
    /// Занятое время специалиста нельзя забронировать повторно
    /// </summary>
    [Fact]
    public void SpecialistCannotHaveOverlappingAppointments()
    {
        var service = CreateService();
        var start = new DateTime(2035, 1, 15, 10, 0, 0);
        service.Create(CreateRequest(start));

        Assert.Throws<InvalidOperationException>(() =>
            service.Create(CreateRequest(start.AddMinutes(30), customerId: 2)));
    }

    /// <summary>
    /// Посетитель не может одновременно записаться к двум специалистам
    /// </summary>
    [Fact]
    public void CustomerCannotHaveOverlappingAppointments()
    {
        var service = CreateService();
        var start = new DateTime(2035, 1, 15, 10, 0, 0);
        service.Create(CreateRequest(start));

        Assert.Throws<InvalidOperationException>(() =>
            service.Create(CreateRequest(start.AddMinutes(30), specialistId: 3)));
    }

    /// <summary>
    /// Следующая запись может начинаться сразу после окончания предыдущей
    /// </summary>
    [Fact]
    public void AdjacentAppointmentsAreAllowed()
    {
        var service = CreateService();
        var start = new DateTime(2035, 1, 15, 10, 0, 0);
        service.Create(CreateRequest(start));

        var next = service.Create(CreateRequest(start.AddHours(1)));

        Assert.Equal(start.AddHours(1), next.AppointmentDateTime);
    }

    /// <summary>
    /// При обновлении запись не конфликтует сама с собой
    /// </summary>
    [Fact]
    public void UpdatingAppointmentExcludesItsOwnInterval()
    {
        var service = CreateService();
        var request = CreateRequest(new DateTime(2035, 1, 15, 10, 0, 0));
        var appointment = service.Create(request);

        service.Update(appointment.Id, request);

        Assert.Equal(appointment.AppointmentDateTime,
            service.GetById(appointment.Id).AppointmentDateTime);
    }

    /// <summary>
    /// Запись нельзя создать для отсутствующего посетителя
    /// </summary>
    [Fact]
    public void UnknownCustomerIsRejected()
    {
        var service = CreateService();

        Assert.Throws<ArgumentException>(() =>
            service.Create(CreateRequest(
                new DateTime(2035, 1, 15, 10, 0, 0),
                customerId: int.MaxValue)));
    }

    /// <summary>
    /// При одновременной записи одно время получает только один запрос
    /// </summary>
    [Fact]
    public void ConcurrentRequestsCannotReserveTheSameInterval()
    {
        var service = CreateService();
        var request = CreateRequest(new DateTime(2035, 1, 15, 10, 0, 0));
        var successes = 0;
        var conflicts = 0;

        Parallel.For(0, 8, _ =>
        {
            try
            {
                service.Create(request);
                Interlocked.Increment(ref successes);
            }
            catch (InvalidOperationException)
            {
                Interlocked.Increment(ref conflicts);
            }
        });

        Assert.Equal(1, successes);
        Assert.Equal(7, conflicts);
    }
}