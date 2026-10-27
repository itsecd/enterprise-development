using FitnessGym.Contracts.Dtos;

namespace FitnessGym.Contracts.Interfaces;

/// <summary>
/// Сервис для выполнения аналитических запросов по данным фитнес-клуба
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Вывести информацию о всех тренерах, стаж работы которых не менее заданного значения
    /// </summary>
    public Task<IReadOnlyList<TrainerDto>> GetExperiencedTrainersAsync(int minExperience);

    /// <summary>
    /// Проверить, является ли указанный зал доступным для записи в заданный момент времени
    /// </summary>
    public Task<bool> IsHallAvailableAsync(string hallName, DateTime moment);

    /// <summary>
    /// Вывести информацию о клиентах, у которых абонемент просрочен на указанную дату
    /// </summary>
    public Task<IReadOnlyList<ClientDto>> GetClientsWithExpiredSubscriptionAsync(DateTime currentDate);

    /// <summary>
    /// Вывести информацию о занятиях за указанный месяц, проходящих в выбранном зале
    /// </summary>
    public Task<IReadOnlyList<TrainingSessionDto>> GetSessionsForMonthInHallAsync(int year, int month, string hallName);

    /// <summary>
    /// Вывести топ-5 наиболее популярных тренеров по количеству проведённых занятий
    /// </summary>
    public Task<IReadOnlyList<TrainerDto>> GetTop5PopularTrainersAsync();
}