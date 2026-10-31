using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;

namespace CarWash.Application.Services;

public class ClientService(IClientRepository repository) : IClientService
{
    public async Task<IEnumerable<ClientDto>> GetAllAsync()
    {
        var clients = await repository.GetAllAsync();
        return clients.Select(c => new ClientDto
        {
            Id = c.Id,
            FullName = c.FullName,
            Phone = c.Phone
        });
    }

    public async Task<ClientDto?> GetByIdAsync(int id)
    {
        var client = await repository.GetByIdAsync(id);
        if (client == null) return null;

        return new ClientDto
        {
            Id = client.Id,
            FullName = client.FullName,
            Phone = client.Phone
        };
    }

    public async Task<ClientDto> CreateAsync(ClientEditDto editDto)
    {
        var client = new Client
        {
            FullName = editDto.FullName,
            Phone = editDto.Phone
        };

        var created = await repository.AddAsync(client);

        return new ClientDto
        {
            Id = created.Id,
            FullName = created.FullName,
            Phone = created.Phone
        };
    }

    public async Task UpdateAsync(int id, ClientEditDto editDto)
    {
        var client = await repository.GetByIdAsync(id);
        if (client == null) throw new KeyNotFoundException("Клиент не найден");

        client.FullName = editDto.FullName;
        client.Phone = editDto.Phone;

        await repository.UpdateAsync(client);
    }

    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}