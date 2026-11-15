using BeautySalon.Application.Exceptions;
using BeautySalon.Application.Services;
using BeautySalon.Contracts.Dto;
using BeautySalon.Infrastructure.InMemory;
using BeautySalon.Shared.Enums;

namespace BeautySalon.Tests;

/// <summary>
/// Тесты CRUD-сервисов поверх in-memory репозиториев. У каждого теста своё хранилище.
/// </summary>
public class CrudServiceTests
{
    private static CancellationToken Ct => TestContext.Current.CancellationToken;

    private readonly BeautySalonDataStore _store = new();

    private ClientService CreateClientService() => new(new ClientRepository(_store));

    private AppointmentService CreateAppointmentService() => new(
        new AppointmentRepository(_store),
        new ClientRepository(_store),
        new MasterRepository(_store),
        new ServiceRepository(_store));

    private static ClientEditDto CreateClientDto(string fullName) => new()
    {
        FullName = fullName,
        Gender = Gender.Female,
        BirthDate = new DateOnly(2000, 1, 1),
        PhoneNumber = "+79005554433"
    };

    private static AppointmentEditDto CreateAppointmentDto(int clientId, int masterId, int serviceId) => new()
    {
        ClientId = clientId,
        MasterId = masterId,
        ServiceId = serviceId,
        DateTime = new DateTime(2024, 7, 1, 10, 0, 0)
    };

    [Fact]
    public async Task CreateClient_ValidDto_AssignsIdAndReturnsDto()
    {
        var service = CreateClientService();
        var dto = CreateClientDto("Сидорова Ольга Петровна");

        var created = await service.CreateAsync(dto, Ct);

        Assert.Equal(11, created.Id);
        Assert.Equal(dto.FullName, created.FullName);
        Assert.Equal(created, await service.GetByIdAsync(created.Id, Ct));
    }

    [Fact]
    public async Task UpdateClient_Existing_ReturnsUpdatedDto()
    {
        var service = CreateClientService();

        var updated = await service.UpdateAsync(1, CreateClientDto("Новое Имя"), Ct);

        Assert.Equal(1, updated.Id);
        Assert.Equal("Новое Имя", updated.FullName);
        Assert.Equal("Новое Имя", (await service.GetByIdAsync(1, Ct)).FullName);
    }

    [Fact]
    public async Task UpdateClient_NotFound_ThrowsNotFound()
    {
        var service = CreateClientService();

        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            service.UpdateAsync(999, CreateClientDto("Никто"), Ct));

        Assert.Equal(999, exception.EntityId);
    }

    [Fact]
    public async Task GetClientById_NotFound_ThrowsNotFound()
    {
        var service = CreateClientService();

        await Assert.ThrowsAsync<NotFoundException>(() => service.GetByIdAsync(999, Ct));
    }

    [Fact]
    public async Task DeleteClient_ExistingThenMissing_DeletesThenThrowsNotFound()
    {
        var service = CreateClientService();

        await service.DeleteAsync(10, Ct);

        await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(10, Ct));
        await Assert.ThrowsAsync<NotFoundException>(() => service.GetByIdAsync(10, Ct));
    }

    [Fact]
    public async Task CreateAppointment_ValidReferences_ReturnsDtoWithNames()
    {
        var service = CreateAppointmentService();

        var created = await service.CreateAsync(CreateAppointmentDto(2, 1, 1), Ct);

        Assert.Equal(11, created.Id);
        Assert.Equal("Иванова Инна Ивановна", created.ClientFullName);
        Assert.Equal("Петрова Анна Ивановна", created.MasterFullName);
        Assert.Equal("Стрижка", created.ServiceName);
    }

    [Fact]
    public async Task CreateAppointment_UnknownClient_ThrowsReferenceNotFound()
    {
        var service = CreateAppointmentService();

        var exception = await Assert.ThrowsAsync<ReferenceNotFoundException>(() =>
            service.CreateAsync(CreateAppointmentDto(999, 1, 1), Ct));

        Assert.Equal(999, exception.EntityId);
    }

    [Fact]
    public async Task UpdateAppointment_UnknownMaster_ThrowsAndKeepsOldData()
    {
        var service = CreateAppointmentService();

        await Assert.ThrowsAsync<ReferenceNotFoundException>(() =>
            service.UpdateAsync(1, CreateAppointmentDto(2, 999, 1), Ct));

        var unchanged = await service.GetByIdAsync(1, Ct);
        Assert.Equal(1, unchanged.ClientId);
        Assert.Equal(new DateTime(2024, 6, 1, 10, 0, 0), unchanged.DateTime);
    }
}
