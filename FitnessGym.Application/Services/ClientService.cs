using FitnessGym.Contracts.Dtos;
using FitnessGym.Contracts.Interfaces;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Enums;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Application.Services;

/// <summary>
/// Сервис для управления клиентами
/// </summary>
public class ClientService(IClientRepository repository) : IClientService
{
    /// <summary>
    /// Получить список всех клиентов
    /// </summary>
    public async Task<IReadOnlyList<ClientDto>> GetAllAsync()
    {
        var clients = await repository.GetAllAsync();
        return clients.Select(ToDto).ToList();
    }

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    public async Task<ClientDto?> GetByIdAsync(int id)
    {
        var client = await repository.GetByIdAsync(id);
        return client is null ? null : ToDto(client);
    }

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    public async Task<ClientDto> CreateAsync(ClientEditDto dto)
    {
        var client = new Client
        {
            PassportNumber = dto.PassportNumber,
            FullName = dto.FullName,
            Gender = Enum.Parse<Gender>(dto.Gender),
            BirthDate = dto.BirthDate,
            Phone = dto.Phone,
            SubscriptionStart = dto.SubscriptionStart,
            SubscriptionEnd = dto.SubscriptionEnd
        };

        var created = await repository.AddAsync(client);
        return ToDto(created);
    }

    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    public async Task<bool> UpdateAsync(int id, ClientEditDto dto)
    {
        var client = await repository.GetByIdAsync(id);
        if (client is null) return false;

        client.PassportNumber = dto.PassportNumber;
        client.FullName = dto.FullName;
        client.Gender = Enum.Parse<Gender>(dto.Gender);
        client.BirthDate = dto.BirthDate;
        client.Phone = dto.Phone;
        client.SubscriptionStart = dto.SubscriptionStart;
        client.SubscriptionEnd = dto.SubscriptionEnd;

        return await repository.UpdateAsync(client);
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    public Task<bool> DeleteAsync(int id) => repository.DeleteAsync(id);

    private static ClientDto ToDto(Client c) => new(
        c.Id, 
        c.PassportNumber, 
        c.FullName, 
        c.Gender.ToString(),
        c.BirthDate, 
        c.Phone, 
        c.SubscriptionStart, 
        c.SubscriptionEnd);
}