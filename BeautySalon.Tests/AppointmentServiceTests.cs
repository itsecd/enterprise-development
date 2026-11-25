using BeautySalon.Application.Services;
using BeautySalon.Contracts.Appointments;
using BeautySalon.Tests.Fixtures.InMemory;
using BeautySalon.Tests.Fixtures.InMemory.Repositories;

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
    public async Task SpecialistCannotHaveOverlappingAppointments()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var service = CreateService();
        var start = new DateTime(2035, 1, 15, 10, 0, 0);
        await service.CreateAsync(CreateRequest(start), cancellationToken);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.CreateAsync(CreateRequest(start.AddMinutes(30), customerId: 2), cancellationToken));
    }

    /// <summary>
    /// Посетитель не может одновременно записаться к двум специалистам
    /// </summary>
    [Fact]
    public async Task CustomerCannotHaveOverlappingAppointments()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var service = CreateService();
        var start = new DateTime(2035, 1, 15, 10, 0, 0);
        await service.CreateAsync(CreateRequest(start), cancellationToken);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            service.CreateAsync(CreateRequest(start.AddMinutes(30), specialistId: 3), cancellationToken));
    }

    /// <summary>
    /// Следующая запись может начинаться сразу после окончания предыдущей
    /// </summary>
    [Fact]
    public async Task AdjacentAppointmentsAreAllowed()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var service = CreateService();
        var start = new DateTime(2035, 1, 15, 10, 0, 0);
        await service.CreateAsync(CreateRequest(start), cancellationToken);

        var next = await service.CreateAsync(CreateRequest(start.AddHours(1)), cancellationToken);

        Assert.Equal(start.AddHours(1), next.AppointmentDateTime);
    }

    /// <summary>
    /// При обновлении запись не конфликтует сама с собой
    /// </summary>
    [Fact]
    public async Task UpdatingAppointmentExcludesItsOwnInterval()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var service = CreateService();
        var request = CreateRequest(new DateTime(2035, 1, 15, 10, 0, 0));
        var appointment = await service.CreateAsync(request, cancellationToken);

        await service.UpdateAsync(appointment.Id, request, cancellationToken);

        Assert.Equal(appointment.AppointmentDateTime,
            (await service.GetByIdAsync(appointment.Id, cancellationToken)).AppointmentDateTime);
    }

    /// <summary>
    /// Запись нельзя создать для отсутствующего посетителя
    /// </summary>
    [Fact]
    public async Task UnknownCustomerIsRejected()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var service = CreateService();

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            service.CreateAsync(CreateRequest(
                new DateTime(2035, 1, 15, 10, 0, 0),
                customerId: int.MaxValue), cancellationToken));
    }

    /// <summary>
    /// При изменении отсутствующей записи ошибка указывает на саму запись
    /// </summary>
    [Fact]
    public async Task UpdatingMissingAppointmentReportsAppointmentBeforeRelatedEntities()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var service = CreateService();
        var request = CreateRequest(
            new DateTime(2035, 1, 15, 10, 0, 0), customerId: int.MaxValue);

        var error = await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            service.UpdateAsync(int.MaxValue, request, cancellationToken));

        Assert.Equal("Запись не найдена", error.Message);
    }

    /// <summary>
    /// При одновременной записи одно время получает только один запрос
    /// </summary>
    [Fact]
    public async Task ConcurrentRequestsCannotReserveTheSameInterval()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var service = CreateService();
        var request = CreateRequest(new DateTime(2035, 1, 15, 10, 0, 0));
        var successes = 0;
        var conflicts = 0;

        await Parallel.ForEachAsync(Enumerable.Range(0, 8), cancellationToken, async (_, requestCancellation) =>
        {
            try
            {
                await service.CreateAsync(request, requestCancellation);
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
