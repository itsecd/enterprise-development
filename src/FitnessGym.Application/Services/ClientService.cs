using System.ComponentModel.DataAnnotations;
using FitnessGym.Application.Exceptions;
using FitnessGym.Contracts.Dto;
using FitnessGym.Contracts.Interfaces;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Application.Services;

/// <summary>
/// Операции с клиентами и преобразование данных в DTO.
/// </summary>
public class ClientService(
    IClientRepository clientRepository,
    ITrainingSessionRepository sessionRepository,
    Func<DateTime>? clock = null) : IClientService
{
    private readonly Func<DateTime> _clock = clock ?? (() => DateTime.Now);

    /// <inheritdoc/>
    public async Task<IReadOnlyList<ClientDto>> GetClientsAsync() =>
        [.. (await clientRepository.GetAllAsync()).Select(MapToDto)];

    /// <inheritdoc/>
    public async Task<ClientDto?> GetClientAsync(int id)
    {
        var client = await clientRepository.GetByIdAsync(id);
        return client is null ? null : MapToDto(client);
    }

    /// <inheritdoc/>
    public async Task<ClientDto> AddClientAsync(UpsertClientDto client) =>
        MapToDto(await clientRepository.AddAsync(MapToEntity(client)));

    /// <inheritdoc/>
    public async Task<ClientDto?> UpdateClientAsync(int id, UpsertClientDto client)
    {
        if (await clientRepository.GetByIdAsync(id) is null)
        {
            return null;
        }

        var entity = MapToEntity(client);
        if (!await clientRepository.UpdateAsync(id, entity))
        {
            return null;
        }

        entity.Id = id;
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteClientAsync(int id)
    {
        if (await sessionRepository.HasClientAsync(id))
        {
            throw new EntityInUseException("У клиента есть записи на занятия — сначала удалите их.");
        }

        return await clientRepository.DeleteAsync(id);
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyList<ClientDto>> GetClientsWithExpiredSubscriptionAsync() =>
        [.. (await clientRepository.GetExpiredAsync(DateOnly.FromDateTime(_clock()))).Select(MapToDto)];

    private static ClientDto MapToDto(Client client) => new()
    {
        Id = client.Id,
        PassportNumber = client.PassportNumber,
        FullName = client.FullName,
        Gender = client.Gender,
        BirthDate = client.BirthDate,
        Phone = client.Phone,
        SubscriptionStart = client.SubscriptionStart,
        SubscriptionEnd = client.SubscriptionEnd,
    };

    private static Client MapToEntity(UpsertClientDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), validateAllProperties: true);
        return new Client
        {
            PassportNumber = dto.PassportNumber,
            FullName = dto.FullName,
            Gender = dto.Gender
                ?? throw new ValidationException("Пол обязателен."),
            BirthDate = dto.BirthDate
                ?? throw new ValidationException("Дата рождения обязательна."),
            Phone = dto.Phone,
            SubscriptionStart = dto.SubscriptionStart
                ?? throw new ValidationException("Дата начала абонемента обязательна."),
            SubscriptionEnd = dto.SubscriptionEnd
                ?? throw new ValidationException("Дата окончания абонемента обязательна."),
        };
    }
}
