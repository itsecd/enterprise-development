using Hotel.Contracts.Dto;
using Hotel.Domain.Enums;

namespace Hotel.Contracts.Interfaces;

/// <summary>
/// Описывает операции с клиентами гостиницы
/// </summary>
public interface IClientService
{
    public Task<IReadOnlyList<ClientDto>> GetAllAsync();
    public Task<ClientDto?> GetByIdAsync(int id);
    public Task<ClientDto> AddAsync(ClientEditDto client);
    public Task<bool> UpdateAsync(int id, ClientEditDto client);
    public Task<bool> DeleteAsync(int id);
    public Task<IReadOnlyList<ClientDto>> GetByRoomCategoryAsync(RoomCategory category);
    public Task<IReadOnlyList<TopClientSpendingDto>> GetTopSpendersAsync();
}
