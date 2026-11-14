namespace DryCleaning.Domain.Interfaces;

public interface IClientRepository
{
    Task<IReadOnlyList<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(int id);
    Task<Client> AddAsync(Client client);
    Task<bool> UpdateAsync(Client client);
    Task<bool> DeleteAsync(int id);
}
