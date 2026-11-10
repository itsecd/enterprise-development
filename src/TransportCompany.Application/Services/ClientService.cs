using TransportCompany.Application.Mappers;
using TransportCompany.Contracts.Dto;
using TransportCompany.Contracts.Interfaces;
using TransportCompany.Domain.Interfaces;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис для работы с клиентами транспортной компании
/// </summary>
public class ClientService(IClientRepository repository) : IClientService
{
    private readonly IClientRepository _repository = repository;

    /// <summary>
    /// Получить список всех клиентов
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список клиентов</returns>
    public async Task<IEnumerable<ClientDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var clients = await _repository.GetAllAsync(cancellationToken);
        return clients.Select(c => c.ToDto());
    }

    /// <summary>
    /// Получить клиента по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Клиент или null, если не найден</returns>
    public async Task<ClientDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var client = await _repository.GetByIdAsync(id, cancellationToken);
        return client?.ToDto();
    }

    /// <summary>
    /// Создать нового клиента
    /// </summary>
    /// <param name="dto">Данные клиента</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданный клиент</returns>
    public async Task<ClientDto> CreateAsync(ClientEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var createdEntity = await _repository.AddAsync(entity, cancellationToken);
        return createdEntity.ToDto();
    }

    /// <summary>
    /// Обновить данные клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="dto">Новые данные клиента</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<ClientDto?> UpdateAsync(int id, ClientEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        entity.Id = id;
        var updatedEntity = await _repository.UpdateAsync(entity, cancellationToken);
        return updatedEntity?.ToDto();
    }

    /// <summary>
    /// Удалить клиента
    /// </summary>
    /// <param name="id">Идентификатор клиента</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }
}