using BeautySalon.Application.Exceptions;
using BeautySalon.Contracts.Dto;
using BeautySalon.Shared.Enums;

namespace BeautySalon.Tests;

/// <summary>
/// Тесты CRUD-сервисов поверх in-memory репозиториев
/// </summary>
public class CrudServiceTests(CrudServiceFixture fixture) : IClassFixture<CrudServiceFixture>
{
    private static CancellationToken Ct => TestContext.Current.CancellationToken;

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
        var service = fixture.CreateClientService();
        var dto = CreateClientDto("Сидорова Ольга Петровна");

        var created = await service.CreateAsync(dto, Ct);

        Assert.Equal(11, created.Id);
        Assert.Equal(dto.FullName, created.FullName);
        Assert.Equal(created, await service.GetByIdAsync(created.Id, Ct));
    }

    [Fact]
    public async Task UpdateClient_Existing_ReturnsUpdatedDto()
    {
        var service = fixture.CreateClientService();

        var updated = await service.UpdateAsync(1, CreateClientDto("Новое Имя"), Ct);

        Assert.Equal(1, updated.Id);
        Assert.Equal("Новое Имя", updated.FullName);
        Assert.Equal("Новое Имя", (await service.GetByIdAsync(1, Ct)).FullName);
    }

    [Fact]
    public async Task UpdateClient_NotFound_ThrowsNotFound()
    {
        var service = fixture.CreateClientService();

        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            service.UpdateAsync(999, CreateClientDto("Никто"), Ct));

        Assert.Equal(999, exception.EntityId);
    }

    [Fact]
    public async Task GetClientById_NotFound_ThrowsNotFound()
    {
        var service = fixture.CreateClientService();

        await Assert.ThrowsAsync<NotFoundException>(() => service.GetByIdAsync(999, Ct));
    }

    [Fact]
    public async Task DeleteClient_ExistingThenMissing_DeletesThenThrowsNotFound()
    {
        var service = fixture.CreateClientService();

        await service.DeleteAsync(10, Ct);

        await Assert.ThrowsAsync<NotFoundException>(() => service.DeleteAsync(10, Ct));
        await Assert.ThrowsAsync<NotFoundException>(() => service.GetByIdAsync(10, Ct));
    }

    [Fact]
    public async Task CreateAppointment_ValidReferences_ReturnsDtoWithNames()
    {
        var service = fixture.CreateAppointmentService();

        var created = await service.CreateAsync(CreateAppointmentDto(2, 1, 1), Ct);

        Assert.Equal(11, created.Id);
        Assert.Equal("Иванова Инна Ивановна", created.ClientFullName);
        Assert.Equal("Петрова Анна Ивановна", created.MasterFullName);
        Assert.Equal("Стрижка", created.ServiceName);
    }

    [Fact]
    public async Task CreateAppointment_UnknownClient_ThrowsReferenceNotFound()
    {
        var service = fixture.CreateAppointmentService();

        var exception = await Assert.ThrowsAsync<ReferenceNotFoundException>(() =>
            service.CreateAsync(CreateAppointmentDto(999, 1, 1), Ct));

        Assert.Equal(999, exception.EntityId);
    }

    [Fact]
    public async Task UpdateAppointment_UnknownMaster_ThrowsAndKeepsOldData()
    {
        var service = fixture.CreateAppointmentService();

        await Assert.ThrowsAsync<ReferenceNotFoundException>(() =>
            service.UpdateAsync(1, CreateAppointmentDto(2, 999, 1), Ct));

        var unchanged = await service.GetByIdAsync(1, Ct);
        Assert.Equal(1, unchanged.ClientId);
        Assert.Equal(new DateTime(2024, 6, 1, 10, 0, 0), unchanged.DateTime);
    }
}
