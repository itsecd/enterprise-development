using TransportCompany.Application.Mappers;
using TransportCompany.Contracts.Dto;
using TransportCompany.Contracts.Interfaces;
using TransportCompany.Domain.Interfaces;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис для работы с рейсами транспортной компании
/// </summary>
public class TripService(ITripRepository repository) : ITripService
{
    private readonly ITripRepository _repository = repository;

    /// <summary>
    /// Получить список всех рейсов
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список рейсов</returns>
    public async Task<IEnumerable<TripDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => e.ToDto());
    }

    /// <summary>
    /// Получить рейс по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор рейса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Рейс или null, если не найден</returns>
    public async Task<TripDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity?.ToDto();
    }

    /// <summary>
    /// Создать новый рейс
    /// </summary>
    /// <param name="dto">Данные рейса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданный рейс</returns>
    public async Task<TripDto> CreateAsync(TripEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var createdEntity = await _repository.AddAsync(entity, cancellationToken);
        return createdEntity.ToDto();
    }

    /// <summary>
    /// Обновить данные рейса
    /// </summary>
    /// <param name="id">Идентификатор рейса</param>
    /// <param name="dto">Новые данные рейса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<TripDto?> UpdateAsync(int id, TripEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        entity.Id = id;
        var updatedEntity = await _repository.UpdateAsync(entity, cancellationToken);
        return updatedEntity?.ToDto();
    }

    /// <summary>
    /// Удалить рейс
    /// </summary>
    /// <param name="id">Идентификатор рейса</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }
}