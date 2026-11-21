using TransportCompany.Application.Mappers;
using TransportCompany.Contracts.Dto;
using TransportCompany.Contracts.Interfaces;
using TransportCompany.Domain.Interfaces;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис для работы с транспортными средствами
/// </summary>
public class VehicleService(IVehicleRepository repository) : IVehicleService
{
    private readonly IVehicleRepository _repository = repository;

    /// <summary>
    /// Получить список всех транспортных средств
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список транспортных средств</returns>
    public async Task<IEnumerable<VehicleDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => e.ToDto());
    }

    /// <summary>
    /// Получить транспортное средство по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор транспортного средства</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Транспортное средство или null, если не найдено</returns>
    public async Task<VehicleDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity?.ToDto();
    }

    /// <summary>
    /// Создать новое транспортное средство
    /// </summary>
    /// <param name="dto">Данные транспортного средства</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданное транспортное средство</returns>
    public async Task<VehicleDto> CreateAsync(VehicleEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var createdEntity = await _repository.AddAsync(entity, cancellationToken);
        return createdEntity.ToDto();
    }

    /// <summary>
    /// Обновить данные транспортного средства
    /// </summary>
    /// <param name="id">Идентификатор транспортного средства</param>
    /// <param name="dto">Новые данные транспортного средства</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<VehicleDto?> UpdateAsync(int id, VehicleEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        entity.Id = id;
        var updatedEntity = await _repository.UpdateAsync(entity, cancellationToken);
        return updatedEntity?.ToDto();
    }

    /// <summary>
    /// Удалить транспортное средство
    /// </summary>
    /// <param name="id">Идентификатор транспортного средства</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }
}