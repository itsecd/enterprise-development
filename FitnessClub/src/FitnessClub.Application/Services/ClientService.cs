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
    public async Task<IReadOnlyList<ClientDto>> GetAllAsync()
    {
        var clients = await clientRepository.GetAllAsync();
        return [.. clients.Select(c => c.ToDto())];
    }
    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    public async Task<ClientDto?> GetByIdAsync(int id)
    {
        var client = await clientRepository.GetByIdAsync(id);
        return client?.ToDto();
    }
    /// <summary>
    /// Добавить клиента
    /// </summary>
    public async Task<ClientDto> AddAsync(ClientEditDto editDto)
    {
        await passportNumberValidator.EnsureUniqueAsync(editDto.PassportNumber);
        var client = await clientRepository.AddAsync(editDto.ToEntity());
        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation("Создан клиент {ClientId}: {FullName}", client.Id, client.FullName);
        return client.ToDto();
    }
    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    public async Task<ClientDto?> UpdateAsync(int id, ClientEditDto editDto)
    {
        var client = await clientRepository.GetByIdAsync(id);
        if (client is null)
        {
            logger.LogWarning("Клиент {ClientId} не найден для обновления", id);
            return null;
        }
        await passportNumberValidator.EnsureUniqueAsync(editDto.PassportNumber, client);
        client.UpdateEntity(editDto);
        await clientRepository.UpdateAsync(client);
        if (logger.IsEnabled(LogLevel.Information))
            logger.LogInformation("Обновлен клиент {ClientId}: {FullName}", client.Id, client.FullName);
        return client.ToDto();
    }
    /// <summary>
    /// Удалить клиента при отсутствии брони
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var allBookings = await bookingRepository.GetAllAsync();
        if (allBookings.Any(b => b.Client.Id == id))
            throw new ConflictException("Невозможно удалить клиента, так как существуют бронирования для него.");
        var deleted = await clientRepository.DeleteAsync(id);
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
