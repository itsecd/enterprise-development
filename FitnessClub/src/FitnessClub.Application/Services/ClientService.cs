using FitnessClub.Application.Exceptions;
using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;
using FitnessClub.Application.Validation;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Управление клиентами
/// </summary>
public class ClientService(
    IRepository<Client> clientRepository,
    IRepository<Booking> bookingRepository,
    ILogger<ClientService> logger,
    PassportNumberUniquenessValidator passportNumberValidator) : ICrudService<ClientDto, ClientEditDto>
{
    /// <summary>
    /// Получить список клиентов
    /// </summary>
    public async Task<IReadOnlyList<ClientDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var clients = await clientRepository.GetAllAsync(cancellationToken);
        return [.. clients.Select(c => c.ToDto())];
    }
    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    public async Task<ClientDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var client = await clientRepository.GetByIdAsync(id, cancellationToken);
        return client?.ToDto();
    }
    /// <summary>
    /// Добавить клиента
    /// </summary>
    public async Task<ClientDto> AddAsync(ClientEditDto editDto, CancellationToken cancellationToken = default)
    {
        await passportNumberValidator.EnsureUniqueAsync(editDto.PassportNumber, cancellationToken: cancellationToken);
        var client = await clientRepository.AddAsync(editDto.ToEntity(), cancellationToken);
        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation("Создан клиент {ClientId}: {FullName}", client.Id, client.FullName);
        return client.ToDto();
    }
    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    public async Task<ClientDto?> UpdateAsync(int id, ClientEditDto editDto, CancellationToken cancellationToken = default)
    {
        var client = await clientRepository.GetByIdAsync(id, cancellationToken);
        if (client is null)
        {
            logger.LogWarning("Клиент {ClientId} не найден для обновления", id);
            return null;
        }
        await passportNumberValidator.EnsureUniqueAsync(editDto.PassportNumber, client, cancellationToken);
        client.UpdateEntity(editDto);
        await clientRepository.UpdateAsync(client, cancellationToken);
        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation("Обновлен клиент {ClientId}: {FullName}", client.Id, client.FullName);
        return client.ToDto();
    }
    /// <summary>
    /// Удалить клиента при отсутствии брони
    /// </summary>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (await bookingRepository.AnyAsync(b => b.Client.Id == id, cancellationToken))
            throw new ConflictException("Невозможно удалить клиента, так как существуют бронирования для него.");
        var deleted = await clientRepository.DeleteAsync(id, cancellationToken);
        if (deleted)
        {    
            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation("Удален клиент {ClientId}", id);
        }
        else
            logger.LogWarning("Клиент {ClientId} не найден для удаления", id);
        return deleted;
    }
}
