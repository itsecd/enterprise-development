using System.ComponentModel.DataAnnotations;
using FitnessGym.Application.Exceptions;
using FitnessGym.Domain.Seeds;
using FitnessGym.Application.Services;
using FitnessGym.Contracts.Dto;
using FitnessGym.Domain.Enums;
using FitnessGym.Infrastructure.InMemory;

namespace FitnessGym.Tests.Services;

/// <summary>
/// Тесты CRUD-операций с тренерами и аналитических запросов по тренерам в сервисе.
/// </summary>
/// <param name="fixture">Фикстура с тестовыми данными фитнес-клуба.</param>
public class TrainerServiceTests(FitnessGymFixture fixture) : IClassFixture<FitnessGymFixture>
{
    /// <summary>
    /// Создаёт сервис на репозиториях фикстуры с детерминированными часами.
    /// </summary>
    private TrainerService CreateService()
    {
        var data = ClubDataSeeder.Create(fixture.Now);
        return new TrainerService(
            new InMemoryTrainerRepository(data.Trainers),
            new InMemorySpecializationRepository(data.Specializations),
            new InMemoryTrainingSessionRepository(data.Sessions));
    }

    /// <summary>
    /// Должен возвращать только тренеров, стаж работы которых не менее 5 лет.
    /// </summary>
    [Fact]
    public async Task GetExperiencedTrainersShouldReturnTrainersWithAtLeastFiveYearsExperience()
    {
        // arrange
        var service = CreateService();
        string[] expectedTrainerNames =
        [
            "Кузнецова Наталья Сергеевна",
            "Лебедева Дарья Александровна",
            "Новиков Артур Валерьевич",
            "Морозов Георгий Львович",
            "Волкова Ирина Дмитриевна",
            "Павлова Светлана Юрьевна",
            "Семёнов Кирилл Андреевич",
            "Голубева Вероника Игоревна",
        ];

        // act
        var actualTrainers = await service.GetExperiencedTrainersAsync(minYears: 5);

        // assert
        Assert.Equal(expectedTrainerNames, actualTrainers.Select(t => t.FullName));
    }

    /// <summary>
    /// Должен возвращать пять наиболее популярных тренеров, упорядоченных по убыванию популярности.
    /// </summary>
    [Fact]
    public async Task GetPopularTrainersShouldReturnTopFiveOrderedByPopularity()
    {
        // arrange
        var service = CreateService();
        string[] expectedTrainerNames =
        [
            "Волкова Ирина Дмитриевна",
            "Новиков Артур Валерьевич",
            "Семёнов Кирилл Андреевич",
            "Кузнецова Наталья Сергеевна",
            "Морозов Георгий Львович",
        ];

        // act
        var actualTrainers = await service.GetPopularTrainersAsync(count: 5);

        // assert
        Assert.Equal(expectedTrainerNames, actualTrainers.Select(t => t.FullName));
    }

    /// <summary>
    /// Должен добавлять тренера с существующей специализацией,
    /// присваивая ему следующий свободный идентификатор.
    /// </summary>
    [Fact]
    public async Task AddTrainerShouldAssignNextIdentifierAndStoreTrainer()
    {
        // arrange
        var service = CreateService();
        var expectedTrainersCount = fixture.Trainers.Count + 1;
        var newTrainer = new UpsertTrainerDto
        {
            PassportNumber = "4699 999999",
            FullName = "Тестова Анастасия Игоревна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1995, 5, 5),
            SpecializationId = fixture.Specializations[0].Id,
            ExperienceYears = 5,
        };

        // act
        var addedTrainer = await service.AddTrainerAsync(newTrainer);
        var storedTrainer = await service.GetTrainerAsync(addedTrainer.Id);

        // assert
        Assert.Equal(expectedTrainersCount, (await service.GetTrainersAsync()).Count);
        Assert.Equal(newTrainer.FullName, storedTrainer?.FullName);
        Assert.Equal("Йога", storedTrainer?.SpecializationName);
    }

    /// <summary>
    /// Должен запрещать добавлять тренера с несуществующей специализацией.
    /// </summary>
    [Fact]
    public async Task AddTrainerWithMissingSpecializationShouldThrow()
    {
        // arrange
        var service = CreateService();
        var newTrainer = new UpsertTrainerDto
        {
            PassportNumber = "4698 888888",
            FullName = "Несуществующий Артём Львович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1990, 1, 1),
            SpecializationId = 999,
            ExperienceYears = 3,
        };

        // act, assert
        await Assert.ThrowsAsync<ValidationException>(() => service.AddTrainerAsync(newTrainer));
        Assert.Equal(fixture.Trainers.Count, (await service.GetTrainersAsync()).Count);
    }

    /// <summary>
    /// Должен запрещать удалять тренера, у которого есть записи на занятия.
    /// </summary>
    [Fact]
    public async Task DeleteTrainerWithSessionsShouldThrow()
    {
        // arrange
        var service = CreateService();
        var trainerWithSessionsId = fixture.Sessions[0].Trainer.Id;

        // act, assert
        await Assert.ThrowsAsync<EntityInUseException>(() => service.DeleteTrainerAsync(trainerWithSessionsId));
        Assert.NotNull((await service.GetTrainerAsync(trainerWithSessionsId)));
    }
}
