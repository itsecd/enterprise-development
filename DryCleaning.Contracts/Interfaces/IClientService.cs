using DryCleaning.Contracts.Dto;

namespace DryCleaning.Contracts.Interfaces;

public interface IClientService
{
    Task<IReadOnlyList<ClientDto>> GetAllAsync();
    Task<ClientDto?> GetByIdAsync(int id);
    Task<ClientDto> AddAsync(ClientEditDto client);
    Task<bool> UpdateAsync(int id, ClientEditDto client);
    Task<bool> DeleteAsync(int id);
    Task<IReadOnlyList<TopClientDto>> GetTopClientsAsync(DateTime start, DateTime end);
    Task<TopSpenderDto?> GetTopSpenderAsync();
    Task<LongestProcessingDto?> GetLongestProcessingOrderAsync();
}
