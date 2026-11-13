using AutoService.Contracts.Dtos;

namespace AutoService.Contracts.Interfaces;

/// <summary>
/// Сервис для работы с клиентами.
/// </summary>
public interface IClientService
{
    public Task<IEnumerable<ClientDto>> GetAllAsync();
    public Task<ClientDto?> GetByIdAsync(Guid id);
    public Task<ClientDto> CreateAsync(ClientDto clientDto);
    public Task UpdateAsync(Guid id, ClientUpdateDto clientDto);
    public Task DeleteAsync(Guid id);
}