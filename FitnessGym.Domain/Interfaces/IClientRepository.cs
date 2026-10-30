using FitnessGym.Domain.Entities;

namespace FitnessGym.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для работы с клиентами
/// </summary>
public interface IClientRepository
{
    public Task<IReadOnlyList<Client>> GetAllAsync();
    public Task<Client?> GetByIdAsync(int id);
    public Task<Client> AddAsync(Client client);
    public Task<bool> UpdateAsync(Client client);
    public Task<bool> DeleteAsync(int id);
}
