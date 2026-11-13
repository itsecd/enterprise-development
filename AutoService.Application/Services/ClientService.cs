using AutoService.Contracts.Dtos;
using AutoService.Contracts.Interfaces;
using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;

namespace AutoService.Application.Services;

/// <summary>
/// Сервис для управления клиентами.
/// </summary>
public class ClientService(IClientRepository repository) : IClientService
{
    /// <summary>
    /// Получить список всех клиентов.
    /// </summary>
    public async Task<IEnumerable<ClientDto>> GetAllAsync()
    {
        var clients = await repository.GetAllAsync();
        return clients.Select(c => new ClientDto { Id = c.Id, FullName = c.FullName, Phone = c.Phone });
    }

    /// <summary>
    /// Получить клиента по идентификатору.
    /// </summary>
    public async Task<ClientDto?> GetByIdAsync(Guid id)
    {
        var client = await repository.GetByIdAsync(id);
        if (client == null) return null;
        return new ClientDto { Id = client.Id, FullName = client.FullName, Phone = client.Phone };
    }

    /// <summary>
    /// Добавить нового клиента.
    /// </summary>
    public async Task<ClientDto> CreateAsync(ClientDto clientDto)
    {
        var client = new Client
        {
            Id = Guid.NewGuid(),
            FullName = clientDto.FullName,
            Phone = clientDto.Phone
        };
        await repository.AddAsync(client);
        clientDto.Id = client.Id;
        return clientDto;
    }

    /// <summary>
    /// Обновить данные клиента.
    /// </summary>
    public async Task UpdateAsync(Guid id, ClientUpdateDto clientDto)
    {
        var client = await repository.GetByIdAsync(id);
        if (client == null) return;

        client.FullName = clientDto.FullName;
        client.Phone = clientDto.Phone;
        await repository.UpdateAsync(client);
    }

    /// <summary>
    /// Удалить клиента.
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var client = await repository.GetByIdAsync(id);
        if (client == null) return;
        await repository.DeleteAsync(client);
    }
}