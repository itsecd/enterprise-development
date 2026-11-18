using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dto;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// Аналитические запросы фитнес-клуба
/// </summary>
public class AnalyticsService(
    IClientRepository clientRepository,
    ITrainerRepository trainerRepository,
    ILessonRepository lessonRepository) : IAnalyticsService
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<TrainerDto>> GetTrainersWithExperienceAsync(int minYears = 5)
    {
        var trainers = await trainerRepository.ReadAll();

        return [.. trainers
            .Where(t => t.ExperienceYears >= minYears)
            .OrderBy(t => t.FullName)
            .Select(t => t.ToDto())];
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ClientDto>> GetClientsWithExpiredSubscriptionAsync()
    {
        var clients = await clientRepository.ReadAll();

        return [.. clients
            .Where(c => c.SubscriptionEnd < DateTime.Today)
            .OrderBy(c => c.FullName)
            .Select(c => c.ToDto())];
    }

    /// <inheritdoc />
    public async Task<HallAvailabilityDto> CheckHallAvailabilityAsync(string hall, DateTime time)
    {
        var lessons = await lessonRepository.ReadAll();

        var isAvailable = !lessons.Any(l =>
            l.Hall == hall &&
            l.DateStart <= time &&
            time < l.DateEnd);

        return new HallAvailabilityDto { Hall = hall, CheckedTime = time, IsAvailable = isAvailable };
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<LessonDto>> GetHallLessonsForCurrentMonthAsync(string hall)
    {
        var lessons = await lessonRepository.ReadAll();
        var today = DateTime.Today;

        return [.. lessons
            .Where(l => l.Hall == hall &&
                        l.DateStart.Year == today.Year &&
                        l.DateStart.Month == today.Month)
            .OrderBy(l => l.DateStart)
            .Select(l => l.ToDto())];
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<PopularTrainerDto>> GetTopPopularTrainersAsync(int count = 5)
    {
        var lessons = await lessonRepository.ReadAll();

        return [.. lessons
            .GroupBy(l => l.Trainer)
            .OrderByDescending(g => g.Count())
            .Take(count)
            .Select(g => new PopularTrainerDto
            {
                TrainerId = g.Key.Id,
                FullName = g.Key.FullName,
                LessonsCount = g.Count(),
            })];
    }
}