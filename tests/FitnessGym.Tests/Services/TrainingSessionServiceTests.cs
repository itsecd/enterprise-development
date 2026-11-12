using System.ComponentModel.DataAnnotations;
using FitnessGym.Domain.Seeds;
using FitnessGym.Application.Services;
using FitnessGym.Contracts.Dto;
using FitnessGym.Infrastructure.InMemory;

namespace FitnessGym.Tests.Services;

/// <summary>
/// Тесты CRUD-операций с записями на занятия и аналитических запросов по занятиям и залам в сервисе.
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными фитнес-клуба.</param>
public class TrainingSessionServiceTests(FitnessGymFixture fixture) : IClassFixture<FitnessGymFixture>
{
    /// <summary>
    /// Создаёт сервис на репозиториях фикстуры с детерминированными часами.
    /// </summary>
    private TrainingSessionService CreateService()
    {
        var data = ClubDataSeeder.Create(fixture.Now);
        return new TrainingSessionService(
            new InMemoryTrainingSessionRepository(data.Sessions),
            new InMemoryClientRepository(data.Clients),
            new InMemoryTrainerRepository(data.Trainers),
            () => fixture.Now);
    }

    /// <summary>
    /// Должен возвращать только занятия, проходящие в выбранном зале в текущем месяце.
    /// </summary>
    [Fact]
    public async Task GetSessionsForCurrentMonthShouldReturnSessionsInSelectedHall()
    {
        // arrange
        var service = CreateService();
        var hallName = "Силовой зал";
        int[] expectedSessionIds = [0, 1, 2];

        // act
        var actualSessions = await service.GetSessionsForCurrentMonthAsync(hallName);

        // assert
        Assert.Equal(expectedSessionIds, actualSessions.Select(s => s.Id));
    }

    /// <summary>
    /// Должен добавлять запись на занятие по существующим клиенту и тренеру,
    /// присваивая ей следующий свободный идентификатор.
    /// </summary>
    [Fact]
    public async Task AddSessionShouldAssignNextIdentifierAndStoreSession()
    {
        // arrange
        var service = CreateService();
        var expectedSessionsCount = fixture.Sessions.Count + 1;
        var newSession = new UpsertTrainingSessionDto
        {
            ClientId = 5,
            TrainerId = 7,
            StartsAt = fixture.Now.AddDays(1),
            HallName = "Силовой зал",
            IsTrial = false,
        };

        // act
        var addedSession = await service.AddSessionAsync(newSession);
        var storedSession = await service.GetSessionAsync(addedSession.Id);

        // assert
        Assert.Equal(expectedSessionsCount, (await service.GetSessionsAsync()).Count);
        Assert.Equal(newSession.HallName, storedSession?.HallName);
        Assert.Null(storedSession?.EndsAt);
    }

    /// <summary>
    /// Должен запрещать добавлять запись на занятие с несуществующим тренером.
    /// </summary>
    [Fact]
    public async Task AddSessionWithMissingTrainerShouldThrow()
    {
        // arrange
        var service = CreateService();
        var newSession = new UpsertTrainingSessionDto
        {
            ClientId = 5,
            TrainerId = 999,
            StartsAt = fixture.Now.AddDays(1),
            HallName = "Силовой зал",
            IsTrial = false,
        };

        // act, assert
        await Assert.ThrowsAsync<ValidationException>(() => service.AddSessionAsync(newSession));
        Assert.Equal(fixture.Sessions.Count, (await service.GetSessionsAsync()).Count);
    }
}
