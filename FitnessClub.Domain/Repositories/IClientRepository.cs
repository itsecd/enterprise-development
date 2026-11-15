using FitnessClub.Domain.Entities;

namespace FitnessClub.Domain.Repositories;

/// <summary>
/// Контракт репозитория клиентов
/// </summary>
public interface IClientRepository : IRepository<Client, int>
{
    /// <summary>
    /// Получает клиентов с просроченными абонементами
    /// </summary>
    Task<IReadOnlyList<Client>> GetExpiredSubscriptions(DateOnly today);
}