using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Interfaces;

/// <summary>Аналитические запросы, выполняемые хранилищем.</summary>
public interface IAnalyticsRepository
{
    Task<IReadOnlyCollection<Trainer>> GetExperiencedTrainersAsync();
    Task<bool> IsHallOccupiedAsync(int hallId, DateTime now);
    Task<IReadOnlyCollection<Client>> GetExpiredClientsAsync(DateTime today);
    Task<IReadOnlyCollection<Training>> GetHallTrainingsAsync(int hallId, DateTime start, DateTime end);
    Task<IReadOnlyCollection<Trainer>> GetTopTrainersAsync();
}
