using System.ComponentModel.DataAnnotations;
using FitnessGym.Application.Exceptions;
using FitnessGym.Domain.Seeds;
using FitnessGym.Application.Services;
using FitnessGym.Contracts.Dto;
using FitnessGym.Domain.Enums;
using FitnessGym.Infrastructure.InMemory;

namespace FitnessGym.Tests.Services;

/// <summary>
/// Тесты CRUD-операций с клиентами и запроса клиентов с просроченным абонементом в сервисе.
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными фитнес-клуба.</param>
public class ClientServiceTests(FitnessGymFixture fixture) : IClassFixture<FitnessGymFixture>
{
    /// <summary>
    /// Создаёт сервис на репозиториях фикстуры с детерминированными часами.
    /// </summary>
    private ClientService CreateService()
    {
        var data = ClubDataSeeder.Create(fixture.Now);
        return new ClientService(
            new InMemoryClientRepository(data.Clients),
            new InMemoryTrainingSessionRepository(data.Sessions),
            () => fixture.Now);
    }

    /// <summary>
    /// Должен возвращать только клиентов с просроченным абонементом, упорядоченных по ФИО.
    /// </summary>
    [Fact]
    public async Task GetClientsWithExpiredSubscriptionShouldReturnClientsOrderedByFullName()
    {
        // arrange
        var service = CreateService();
        string[] expectedClientNames =
        [
            "Андреев Алексей Сергеевич",
            "Борисова Марина Владимировна",
            "Васильев Дмитрий Олегович",
            "Гаврилова Елена Николаевна",
            "Дмитриев Павел Андреевич",
        ];

        // act
        var actualClients = await service.GetClientsWithExpiredSubscriptionAsync();

        // assert
        Assert.Equal(expectedClientNames, actualClients.Select(c => c.FullName));
    }

    /// <summary>
    /// Должен добавлять клиента, присваивая ему следующий свободный идентификатор.
    /// </summary>
    [Fact]
    public async Task AddClientShouldAssignNextIdentifierAndStoreClient()
    {
        // arrange
        var service = CreateService();
        var expectedClientsCount = fixture.Clients.Count + 1;
        var newClient = new UpsertClientDto
        {
            PassportNumber = "4599 999999",
            FullName = "Тестов Тест Тестович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(2000, 1, 1),
            Phone = "+7 (911) 999-99-99",
            SubscriptionStart = fixture.Today,
            SubscriptionEnd = fixture.Today.AddDays(30),
        };

        // act
        var addedClient = await service.AddClientAsync(newClient);
        var storedClient = await service.GetClientAsync(addedClient.Id);

        // assert
        Assert.Equal(expectedClientsCount, (await service.GetClientsAsync()).Count);
        Assert.Equal(newClient.FullName, storedClient?.FullName);
    }

    /// <summary>
    /// Должен заменять данные существующего клиента, сохраняя его идентификатор.
    /// </summary>
    [Fact]
    public async Task UpdateClientShouldReplaceExistingClientKeepingIdentifier()
    {
        // arrange
        var service = CreateService();
        var expectedClientsCount = fixture.Clients.Count;
        var updatedClient = new UpsertClientDto
        {
            PassportNumber = fixture.Clients[0].PassportNumber,
            FullName = fixture.Clients[0].FullName,
            Gender = fixture.Clients[0].Gender,
            BirthDate = fixture.Clients[0].BirthDate,
            Phone = "+7 (911) 555-55-55",
            SubscriptionStart = fixture.Clients[0].SubscriptionStart,
            SubscriptionEnd = fixture.Clients[0].SubscriptionEnd,
        };

        // act
        var isUpdated = await service.UpdateClientAsync(0, updatedClient);
        var storedClient = await service.GetClientAsync(0);

        // assert
        Assert.NotNull(isUpdated);
        Assert.Equal(expectedClientsCount, (await service.GetClientsAsync()).Count);
        Assert.Equal("+7 (911) 555-55-55", storedClient?.Phone);
    }

    /// <summary>
    /// Должен удалять клиента, у которого нет записей на занятия.
    /// </summary>
    [Fact]
    public async Task DeleteClientWithoutSessionsShouldRemoveClient()
    {
        // arrange
        var service = CreateService();
        var clientWithoutSessionsId = 10;
        var expectedClientsCount = fixture.Clients.Count - 1;

        // act
        var isDeleted = await service.DeleteClientAsync(clientWithoutSessionsId);

        // assert
        Assert.True(isDeleted);
        Assert.Null((await service.GetClientAsync(clientWithoutSessionsId)));
        Assert.Equal(expectedClientsCount, (await service.GetClientsAsync()).Count);
    }

    /// <summary>
    /// Должен запрещать удалять клиента, у которого есть записи на занятия.
    /// </summary>
    [Fact]
    public async Task DeleteClientWithSessionsShouldThrow()
    {
        // arrange
        var service = CreateService();
        var clientWithSessionsId = fixture.Sessions[0].Client.Id;

        // act, assert
        await Assert.ThrowsAsync<EntityInUseException>(() => service.DeleteClientAsync(clientWithSessionsId));
        Assert.NotNull((await service.GetClientAsync(clientWithSessionsId)));
    }
}
