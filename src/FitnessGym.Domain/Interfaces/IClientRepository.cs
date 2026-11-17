using FitnessGym.Domain.Entities;

namespace FitnessGym.Domain.Interfaces;

/// <summary>
/// Репозиторий клиентов фитнес-клуба.
/// </summary>
public interface IClientRepository : IRepository<Client>
{
    /// <summary>
    /// Возвращает клиентов с просроченным абонементом, упорядоченных по ФИО.
    /// </summary>
    public Task<IReadOnlyList<Client>> GetExpiredAsync(DateOnly today);
}
