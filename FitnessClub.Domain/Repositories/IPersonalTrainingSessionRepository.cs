using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

/// <summary>
/// Контракт репозитория персональных тренировок
/// </summary>
public interface IPersonalTrainingSessionRepository : IRepository<PersonalTrainingSession, int>
{
    /// <summary>
    /// Проверяет наличие тренировок у клиента
    /// </summary>
    Task<bool> HasSessionsForClient(int clientId);
    /// <summary>
    /// Проверяет наличие тренировок у тренера
    /// </summary>
    Task<bool> HasSessionsForTrainer(int trainerId);
    /// <summary>
    /// Получает тренировки клиента
    /// </summary>
    Task<IReadOnlyList<PersonalTrainingSession>> GetByClient(int clientId);
    /// <summary>
    /// Получает тренировки тренера
    /// </summary>
    Task<IReadOnlyList<PersonalTrainingSession>> GetByTrainer(int trainerId);
    /// <summary>
    /// Проверяет наличие тренировок в указанном зале
    /// </summary>
    Task<bool> HallExists(string hallName);
    /// <summary>
    /// Проверяет занятость зала в указанное время
    /// </summary>
    Task<bool> HasSessionInHall(string hallName, DateTime checkTime);
    /// <summary>
    /// Получает тренировки зала за указанный период
    /// </summary>
    Task<IReadOnlyList<PersonalTrainingSession>> GetByHallAndPeriod(string hallName, DateTime start, DateTime end);
    /// <summary>
    /// Получает идентификаторы пяти наиболее популярных тренеров
    /// </summary>
    Task<IReadOnlyList<int>> GetTopFiveTrainerIds();
}