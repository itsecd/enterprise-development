using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.Clients;
using FitnessClub.Application.Contracts.Trainers;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Cервис для аналитических запросов
/// </summary>
public class AnalyticsService(
    ITrainerRepository trainerRepository,
    IClientRepository clientRepository,
    IPersonalTrainingSessionRepository sessionRepository,
    ILogger<AnalyticsService> logger): IAnalyticsService
{
    /// <inheritdoc/>
    public async Task<IList<TrainerDto>> GetTrainersWithFiveOrMoreYearsOfExperience()
    {
        logger.LogInformation("Получение тренеров со стажем не менее 5 лет");
        var trainers = await trainerRepository.GetWithMinExperience(5);
        var result = trainers.Select(trainer=>new TrainerDto
            (
                trainer.Id,
                trainer.PassportNumber,
                trainer.LastName,
                trainer.FirstName,
                trainer.Patronymic,
                trainer.Gender,
                trainer.DateOfBirth,
                trainer.Specialization.Id,
                trainer.WorkYears ))     
            .ToList();
        logger.LogInformation("Получено тренеров со стажем не менее 5 лет: {Count}",result.Count);
        return result;
    }
    /// <inheritdoc/>
    public async Task<bool> CheckHallAvailability(string hallName, DateTime checkTime)
    {
        logger.LogInformation(
            "Проверка, является ли зал {hallName} доступным для записи в {checkTime}",
            hallName,
            checkTime);
        var hallExists = await sessionRepository.HallExists(hallName);
        if (!hallExists)
        {
            logger.LogWarning("Зал {HallName} не существует",hallName);
            throw new ArgumentException($"Зал {hallName} не существует");
        }
        var isAvailable = !await sessionRepository.HasSessionInHall(hallName, checkTime);
        logger.LogInformation(
            "Доступность зала {HallName} на {CheckTime}: {IsAvailable}",
            hallName,
            checkTime,
            isAvailable);
        return isAvailable;
    }
    /// <inheritdoc/>
    public async Task<IList<ClientDto>> GetClientsWithExpiredSubscription()
    {
        logger.LogInformation("Получение клиентов с просроченным абонементом");
        var today = DateOnly.FromDateTime(DateTime.Today);
        var clients = await clientRepository.GetExpiredSubscriptions(today);
        var result = clients
            .Select(client => new ClientDto
            (
                client.Id,
                client.PassportNumber,
                client.LastName,
                client.FirstName,
                client.Patronymic,
                client.Gender,
                client.DateOfBirth,
                client.PhoneNumber,
                client.StartSub,
                client.EndSub))
            .ToList();
        logger.LogInformation("Получено клиентов с просроченным абонементом: {Count}",result.Count);
        return result;
    }
    /// <inheritdoc/>
    public async Task<IList<PersonalTrainingSessionDto>> GetSessionsForCurrentMonthInSelectedHall(string hallName)
    {
        logger.LogInformation("Получение информации о занятиях за текущий месяц, проходящих в зале {hallName}",hallName);
        var today = DateTime.Today;
        var monthStart = new DateTime(today.Year, today.Month, 1);
        var nextMonth = monthStart.AddMonths(1);
        var sessions = await sessionRepository.GetByHallAndPeriod(hallName, monthStart, nextMonth);
        var result = sessions.Select(p =>new PersonalTrainingSessionDto
            (
                p.Id,
                p.Client.Id,
                p.Trainer.Id,
                p.TrainDay,
                p.HallName,
                p.IsTrial
                ))
            .ToList();
        logger.LogInformation("Получено занятий за текущий месяц в зале {HallName}: {Count}",hallName,result.Count);
        return result;
    }
    /// <inheritdoc/>
    public async Task<IList<TrainerDto>> GetTopFivePopularTrainers()
    {
        logger.LogInformation("Получение топ 5 наиболее популярных тренеров");
        var trainerIds = await sessionRepository.GetTopFiveTrainerIds();
        var allTrainers = await trainerRepository.GetByIds(trainerIds);
        var trainers = trainerIds
            .Select(id => allTrainers
                .First(trainer => trainer.Id == id))
            .Select(trainer=>new TrainerDto
            (
                trainer.Id,
                trainer.PassportNumber,
                trainer.LastName,
                trainer.FirstName,
                trainer.Patronymic,
                trainer.Gender,
                trainer.DateOfBirth,
                trainer.Specialization.Id,
                trainer.WorkYears ))   
            .ToList();
        logger.LogInformation("Получено популярных тренеров: {Count}",trainers.Count);
        return trainers;
    }
}
