using TransportCompany.Application.Mappers;
using TransportCompany.Contracts.Dto;
using TransportCompany.Contracts.Interfaces;
using TransportCompany.Domain.Interfaces;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис для работы с водителями транспортной компании
/// </summary>
public class DriverService(IDriverRepository repository) : IDriverService
{
    private readonly IDriverRepository _repository = repository;

    /// <summary>
    /// Получить список всех водителей
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список водителей</returns>
    public async Task<IEnumerable<DriverDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => e.ToDto());
    }

    /// <summary>
    /// Получить водителя по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор водителя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Водитель или null, если не найден</returns>
    public async Task<DriverDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity?.ToDto();
    }

    /// <summary>
    /// Создать нового водителя
    /// </summary>
    /// <param name="dto">Данные водителя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданный водитель</returns>
    public async Task<DriverDto> CreateAsync(DriverEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var createdEntity = await _repository.AddAsync(entity, cancellationToken);
        return createdEntity.ToDto();
    }

    /// <summary>
    /// Обновить данные водителя
    /// </summary>
    /// <param name="id">Идентификатор водителя</param>
    /// <param name="dto">Новые данные водителя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<DriverDto?> UpdateAsync(int id, DriverEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        entity.Id = id;
        var updatedEntity = await _repository.UpdateAsync(entity, cancellationToken);
        return updatedEntity?.ToDto();
    }

    /// <summary>
    /// Удалить водителя
    /// </summary>
    /// <param name="id">Идентификатор водителя</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }
}