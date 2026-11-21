using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// Реализует операции для работы с клиентами фитнес-клуба.
/// </summary>
/// <param name="clientRepository">Репозиторий клиентов.</param>
public class ClientService(IClientRepository clientRepository) : IClientService
{
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<ClientDto>> GetAllAsync()
    {
        var clients = await clientRepository.GetAllAsync();

        return [.. clients.Select(client => client.ToDto())];
    }

    /// <inheritdoc />
    public async Task<ClientDto?> GetByIdAsync(int id)
    {
        var client = await clientRepository.GetByIdAsync(id);

        return client?.ToDto();
    }

    /// <inheritdoc />
    public async Task<ClientDto> CreateAsync(CreateClientDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var client = new Client
        {
            PassportNumber = dto.PassportNumber,
            FullName = dto.FullName,
            Gender = (Gender)(dto.Gender ?? throw new ArgumentException("Пол обязателен.", nameof(dto))),
            BirthDate = dto.BirthDate,
            Phone = dto.Phone,
            SubscriptionStartDate = dto.SubscriptionStartDate,
            SubscriptionEndDate = dto.SubscriptionEndDate
        };

        var createdClient = await clientRepository.AddAsync(client);

        return createdClient.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, UpdateClientDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var existingClient = new Client
        {
            Id = id,
            PassportNumber = dto.PassportNumber,
            FullName = dto.FullName,
            Gender = (Gender)(dto.Gender ?? throw new ArgumentException("Пол обязателен.", nameof(dto))),
            BirthDate = dto.BirthDate,
            Phone = dto.Phone,
            SubscriptionStartDate = dto.SubscriptionStartDate,
            SubscriptionEndDate = dto.SubscriptionEndDate,
        };

        return await clientRepository.UpdateAsync(id, existingClient);
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int id)
    {
        return clientRepository.DeleteAsync(id);
    }
}