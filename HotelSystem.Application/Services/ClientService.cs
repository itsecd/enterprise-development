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
    public List<ClientResponse> GetAll() =>
        clientRepository.GetAll().Select(client => client.ToResponse()).ToList();

    /// <inheritdoc />
    public ClientResponse? GetById(int id) =>
        clientRepository.GetById(id)?.ToResponse();

    /// <inheritdoc />
    public ClientResponse Add(ClientRequest request) =>
        clientRepository.Add(request.ToEntity()).ToResponse();

    /// <inheritdoc />
    public bool Update(int id, ClientRequest request) =>
        clientRepository.Update(id, request.ToEntity());

    /// <inheritdoc />
    public bool Delete(int id) =>
        clientRepository.Delete(id);
}