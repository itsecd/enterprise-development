using TransportCompany.Application.Mappers;
using TransportCompany.Contracts.Dto;
using TransportCompany.Contracts.Interfaces;
using TransportCompany.Domain.Interfaces;

namespace TransportCompany.Application.Services;

/// <summary>
/// Сервис для работы с моделями транспортных средств
/// </summary>
public class VehicleModelService(IVehicleModelRepository repository) : IVehicleModelService
{
    private readonly IVehicleModelRepository _repository = repository;

    /// <summary>
    /// Получить список всех моделей транспортных средств
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список моделей ТС</returns>
    public async Task<IEnumerable<VehicleModelDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        return entities.Select(e => e.ToDto());
    }

    /// <summary>
    /// Получить модель транспортного средства по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор модели ТС</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Модель ТС или null, если не найдена</returns>
    public async Task<VehicleModelDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity?.ToDto();
    }

    /// <summary>
    /// Создать новую модель транспортного средства
    /// </summary>
    /// <param name="dto">Данные модели ТС</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданная модель ТС</returns>
    public async Task<VehicleModelDto> CreateAsync(VehicleModelEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var createdEntity = await _repository.AddAsync(entity, cancellationToken);
        return createdEntity.ToDto();
    }

    /// <summary>
    /// Обновить данные модели транспортного средства
    /// </summary>
    /// <param name="id">Идентификатор модели ТС</param>
    /// <param name="dto">Новые данные модели ТС</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<VehicleModelDto?> UpdateAsync(int id, VehicleModelEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        entity.Id = id;
        var updatedEntity = await _repository.UpdateAsync(entity, cancellationToken);
        return updatedEntity?.ToDto();
    }

    /// <summary>
    /// Удалить модель транспортного средства
    /// </summary>
    /// <param name="id">Идентификатор модели ТС</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _repository.DeleteAsync(id, cancellationToken);
    }
}