using FitnessGym.Application.Mapping;
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
            .Select(DtoMapper.ToTrainerDto)
            .ToList();
    }

    /// <summary>
    /// Проверить, является ли зал доступным для записи в данный момент
    /// </summary>
    public async Task<bool> IsHallAvailableAsync(string hallName, DateTime moment)
    {
        if (!Enum.TryParse<Hall>(hallName, ignoreCase: true, out var hall))
            return false;

        var sessions = await sessionRepository.GetAllAsync();
        var isBusy = sessions.Any(s =>
            s.HallName == hall &&
            s.IsInProgressAt(moment));

        return !isBusy;
    }

    /// <summary>
    /// Вывести информацию о клиентах, у которых просрочен абонемент
    /// </summary>
    public async Task<IReadOnlyList<ClientDto>> GetClientsWithExpiredSubscriptionAsync(DateOnly currentDate)
    {
        var clients = await clientRepository.GetAllAsync();
        return clients
            .Where(c => c.SubscriptionEnd < currentDate)
            .OrderBy(c => c.FullName)
            .Select(c => new ClientDto(
                c.Id,
                c.PassportNumber,
                c.FullName,
                c.Gender.ToString(),
                c.BirthDate,
                c.Phone,
                c.SubscriptionStart,
                c.SubscriptionEnd))
            .ToList();
    }

    /// <summary>
    /// Вывести информацию о занятиях за текущий месяц, проходящих в выбранном зале
    /// </summary>
    public async Task<IReadOnlyList<TrainingSessionDto>> GetSessionsForMonthInHallAsync(int year, int month, string hallName)
    {
        if (!Enum.TryParse<Hall>(hallName, ignoreCase: true, out var hall))
            return new List<TrainingSessionDto>();

        var sessions = await sessionRepository.GetAllAsync();
        return sessions
            .Where(s =>
                s.StartsTraining.Year == year &&
                s.StartsTraining.Month == month &&
                s.HallName == hall)
            .Select(DtoMapper.ToSessionDto)
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
            .Select(g => DtoMapper.ToTrainerDto(g.Key))
            .ToList();
    }
}