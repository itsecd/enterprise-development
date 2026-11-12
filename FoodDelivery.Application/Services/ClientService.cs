using Microsoft.Extensions.Logging;
using FoodDelivery.Contracts.Clients;
using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Сервис для управления клиентами.
/// </summary>
public class ClientService(IRepository<Client> clientRepo, ILogger<ClientService> logger) : IClientService
{
    public IReadOnlyList<ClientDto> GetAll() =>
        [.. clientRepo.GetAll().Select(MapToDto)];

    public ClientDto? GetById(int id)
    {
        var client = clientRepo.GetById(id);
        return client is null ? null : MapToDto(client);
    }

    public ClientDto Create(ClientCreateUpdateDto dto)
    {
        var entity = new Client
        {
            FullName = dto.FullName,
            Phone = dto.Phone,
            DeliveryAddress = dto.DeliveryAddress
        };
        var created = clientRepo.Create(entity);
        logger.LogInformation("Создан клиент с Id = {Id}", created.Id);
        return MapToDto(created);
    }

    public bool Update(int id, ClientCreateUpdateDto dto)
    {
        var existing = clientRepo.GetById(id);
        if (existing is null) return false;

        existing.FullName = dto.FullName;
        existing.Phone = dto.Phone;
        existing.DeliveryAddress = dto.DeliveryAddress;
        var result = clientRepo.Update(existing);
        logger.LogInformation("Обновлён клиент с Id = {Id}", id);
        return result;
    }

    public void Delete(int id)
    {
        clientRepo.Delete(id);
        logger.LogInformation("Удалён клиент с Id = {Id}", id);
    }

    private static ClientDto MapToDto(Client c) =>
        new(c.Id, c.FullName, c.Phone, c.DeliveryAddress);
}