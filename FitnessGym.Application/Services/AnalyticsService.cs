using FitnessGym.Contracts.Dtos;
using FitnessGym.Contracts.Interfaces;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Enums;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Application.Services;

/// <summary>
/// Сервис для выполнения аналитических запросов
/// </summary>
public class AnalyticsService(
    IClientRepository clientRepository,
    ITrainerRepository trainerRepository,
    ITrainingSessionRepository sessionRepository) : IAnalyticsService
{
    /// <summary>
    /// Вывести информацию о всех тренерах, стаж работы которых не менее 5 лет
    /// </summary>
    public async Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(int minExperience)
    {
        var trainers = await trainerRepository.GetAllAsync();
        return trainers
            .Where(t => t.ExperienceYears >= minExperience)
            .OrderBy(t => t.FullName)
            .Select(ToTrainerDto)
            .ToList();
    }

    /// <summary>
    /// Проверить, является ли зал доступным для записи в данный момент
    /// </summary>
    public async Task<bool> IsHallAvailableAsync(string hallName, DateTime moment)
    {
        if (!Enum.TryParse<Hall>(hallName, true, out var hall))
            return false;

        var sessions = await sessionRepository.GetAllAsync();
        var isBusy = sessions.Any(s =>
            s.HallName == hall &&
            moment >= s.StartsTraining &&
            moment <= s.EndsTraining);

        return !isBusy;
    }

    /// <summary>
    /// Вывести информацию о клиентах, у которых просрочен абонемент
    /// </summary>
    public async Task<IReadOnlyList<ClientDto>> GetClientsWithExpiredSubscriptionAsync(DateTime currentDate)
    {
        var dateOnly = DateOnly.FromDateTime(currentDate);
        var clients = await clientRepository.GetAllAsync();
        return clients
            .Where(c => c.SubscriptionEnd < dateOnly)
            .OrderBy(c => c.FullName)
            .Select(c => new ClientDto(
                c.Id, c.PassportNumber, c.FullName, c.Gender.ToString(),
                c.BirthDate, c.Phone, c.SubscriptionStart, c.SubscriptionEnd))
            .ToList();
    }

    /// <summary>
    /// Вывести информацию о занятиях за текущий месяц, проходящих в выбранном зале
    /// </summary>
    public async Task<IReadOnlyList<TrainingSessionDto>> GetSessionsForMonthInHallAsync(int year, int month, string hallName)
    {
        if (!Enum.TryParse<Hall>(hallName, true, out var hall))
            return new List<TrainingSessionDto>();

        var sessions = await sessionRepository.GetAllAsync();
        return sessions
            .Where(s => s.StartsTraining.Year == year &&
                        s.StartsTraining.Month == month &&
                        s.HallName == hall)
            .Select(ToSessionDto)
            .ToList();
    }

    /// <summary>
    /// Вывести топ 5 наиболее популярных тренеров
    /// </summary>
    public async Task<IReadOnlyList<TrainerDto>> GetTop5PopularTrainersAsync()
    {
        var sessions = await sessionRepository.GetAllAsync();
        return sessions
            .GroupBy(s => s.Trainer)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => ToTrainerDto(g.Key))
            .ToList();
    }

    private static TrainerDto ToTrainerDto(Trainer t) => new(
        t.Id, 
        t.PassportNumber, 
        t.FullName, 
        t.Gender.ToString(),
        t.BirthDate, 
        t.Specialization.Id, 
        t.Specialization.Name, 
        t.ExperienceYears);

    private static TrainingSessionDto ToSessionDto(TrainingSession s) => new(
        s.Id, 
        s.Client.Id, 
        s.Client.FullName, 
        s.Trainer.Id, 
        s.Trainer.FullName,
        s.StartsTraining, 
        s.EndsTraining, 
        s.HallName.ToString(), 
        s.IsTrial);
}