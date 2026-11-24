using HotelSystem.Application.Contracts.Clients;
using HotelSystem.Application.Mapping;
using HotelSystem.Domain.Models;
using HotelSystem.Domain.Repositories;

namespace HotelSystem.Application.Services;

/// <summary>
/// Сервис для выполнения CRUD-операций с клиентами гостиницы
/// </summary>
public class ClientService(IRepository<Client> clientRepository)
    : ICrudService<ClientRequest, ClientResponse>
{
    /// <inheritdoc />
    public async Task<List<ClientResponse>> GetAllAsync() =>
        (await clientRepository.GetAllAsync())
            .Select(client => client.ToResponse())
            .ToList();

    /// <inheritdoc />
    public async Task<ClientResponse?> GetByIdAsync(int id) =>
        (await clientRepository.GetByIdAsync(id))?.ToResponse();

    /// <inheritdoc />
    public async Task<ClientResponse> AddAsync(ClientRequest request) =>
        (await clientRepository.AddAsync(request.ToEntity())).ToResponse();

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, ClientRequest request) =>
        await clientRepository.UpdateAsync(id, request.ToEntity());

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id) =>
        await clientRepository.DeleteAsync(id);
}