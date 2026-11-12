using FitnessGym.Application.Services;
using FitnessGym.Contracts.Dto;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Seeds;
using FitnessGym.Infrastructure.InMemory;

namespace FitnessGym.Tests;

/// <summary>
/// Проверки идентификаторов и связей между сущностями при обновлении в памяти.
/// </summary>
public class RepositoryTests
{
    /// <summary>
    /// Изменение тренера и его специализации видно в старых занятиях и в рейтинге.
    /// </summary>
    [Fact]
    public async Task UpdatingTrainerShouldPreserveSessionReferencesAndPopularity()
    {
        var data = ClubDataSeeder.Create(new DateTime(2026, 11, 1, 12, 0, 0));
        var trainerRepository = new InMemoryTrainerRepository(data.Trainers);
        var specializationRepository = new InMemorySpecializationRepository(data.Specializations);
        var sessionRepository = new InMemoryTrainingSessionRepository(data.Sessions);
        var service = new TrainerService(trainerRepository, specializationRepository, sessionRepository);
        var trainer = data.Trainers[7];
        var session = data.Sessions.First(item => item.Trainer.Id == trainer.Id);
        var oldCount = data.Sessions.Count(item => item.Trainer.Id == trainer.Id);
        var input = new UpsertTrainerDto
        {
            PassportNumber = trainer.PassportNumber,
            FullName = "Обновлённый тренер",
            Gender = trainer.Gender,
            BirthDate = trainer.BirthDate,
            SpecializationId = 0,
            ExperienceYears = trainer.ExperienceYears + 1,
        };
        await service.UpdateTrainerAsync(trainer.Id, input);
        await specializationRepository.UpdateAsync(0, new Specialization { Name = "Хатха-йога" });
        await sessionRepository.AddAsync(new TrainingSession
        {
            Client = data.Clients[0],
            Trainer = (await trainerRepository.GetByIdAsync(trainer.Id))!,
            StartsAt = new DateTime(2026, 11, 2),
            HallName = "Зал",
            IsTrial = false,
        });

        Assert.Same(trainer, await trainerRepository.GetByIdAsync(trainer.Id));
        Assert.Same(trainer, session.Trainer);
        Assert.Equal("Обновлённый тренер", session.Trainer.FullName);
        Assert.Equal("Хатха-йога", session.Trainer.Specialization.Name);
        var sessions = await sessionRepository.GetAllAsync();
        Assert.Equal(oldCount + 1, sessions.Count(item => ReferenceEquals(item.Trainer, trainer)));
        var popular = await service.GetPopularTrainersAsync(20);
        Assert.Single(popular, item => item.Id == trainer.Id);
        Assert.Equal(trainer.Id, popular[0].Id);
        Assert.Equal("Хатха-йога", popular[0].SpecializationName);
    }

    /// <summary>
    /// Обновление клиента не оставляет устаревший объект в занятиях.
    /// </summary>
    [Fact]
    public async Task UpdatingClientAndSessionShouldKeepExistingInstances()
    {
        var data = ClubDataSeeder.Create();
        var clients = new InMemoryClientRepository(data.Clients);
        var sessions = new InMemoryTrainingSessionRepository(data.Sessions);
        var original = data.Clients[0];
        var input = new Client
        {
            Id = 999,
            PassportNumber = original.PassportNumber,
            FullName = "Новое имя",
            Gender = original.Gender,
            BirthDate = original.BirthDate,
            Phone = "+79119999999",
            SubscriptionStart = original.SubscriptionStart,
            SubscriptionEnd = original.SubscriptionEnd,
        };
        Assert.True(await clients.UpdateAsync(original.Id, input));
        Assert.Same(original, await clients.GetByIdAsync(original.Id));
        Assert.Equal(0, original.Id);
        Assert.All(data.Sessions.Where(item => item.Client.Id == original.Id), item =>
        {
            Assert.Same(original, item.Client);
            Assert.Equal("Новое имя", item.Client.FullName);
        });

        var originalSession = data.Sessions[0];
        Assert.True(await sessions.UpdateAsync(originalSession.Id, new TrainingSession
        {
            Client = data.Clients[1],
            Trainer = data.Trainers[1],
            StartsAt = originalSession.StartsAt,
            EndsAt = originalSession.StartsAt.AddHours(1),
            HallName = "Новый зал",
            IsTrial = true,
        }));
        Assert.Same(originalSession, await sessions.GetByIdAsync(originalSession.Id));
        Assert.Equal("Новый зал", originalSession.HallName);
        Assert.Same(data.Clients[1], originalSession.Client);
    }

    /// <summary>
    /// Удалённые идентификаторы не переиспользуются, повторное добавление того же объекта отклоняется.
    /// </summary>
    [Fact]
    public async Task AddShouldNotReuseDeletedIdsOrAcceptTheSameInstanceTwice()
    {
        var repository = new InMemorySpecializationRepository([]);
        var first = await repository.AddAsync(new Specialization { Name = "Первая" });
        var firstId = first.Id;
        Assert.True(await repository.DeleteAsync(firstId));
        var second = await repository.AddAsync(new Specialization { Name = "Вторая" });
        Assert.True(second.Id > firstId);
        await Assert.ThrowsAsync<InvalidOperationException>(() => repository.AddAsync(second));
        Assert.Single(await repository.GetAllAsync());
        Assert.False(await repository.UpdateAsync(firstId, new Specialization { Name = "Нет записи" }));
    }

    /// <summary>
    /// Параллельное добавление не теряет записи и не создаёт одинаковые идентификаторы.
    /// </summary>
    [Fact]
    public async Task ConcurrentAddsShouldAssignUniqueIdentifiers()
    {
        var repository = new InMemorySpecializationRepository([]);
        await Task.WhenAll(Enumerable.Range(0, 100).Select(index => Task.Run(() =>
            repository.AddAsync(new Specialization { Name = $"Специализация {index}" }))));
        var items = await repository.GetAllAsync();
        Assert.Equal(100, items.Count);
        Assert.Equal(100, items.Select(item => item.Id).Distinct().Count());
    }
}
