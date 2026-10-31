using CarWash.Contracts.Dto;

namespace CarWash.Contracts.Interfaces;

public interface IClientService
{
    public Task<IEnumerable<ClientDto>> GetAllAsync();
    public Task<ClientDto?> GetByIdAsync(int id);
    public Task<ClientDto> CreateAsync(ClientEditDto editDto);
    public Task UpdateAsync(int id, ClientEditDto editDto);
    public Task DeleteAsync(int id);
}