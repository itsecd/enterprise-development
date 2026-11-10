using BeautySalon.Application.Exceptions;
using BeautySalon.Application.Mapping;
using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Application.Services;

/// <summary>
/// Сервис управления услугами салона
/// </summary>
public class SalonServiceService(IServiceRepository repository) : IServiceService
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<ServiceDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await repository.GetAllAsync(cancellationToken);
        return [.. entities.Select(e => e.ToDto())];
    }

    /// <inheritdoc />
    public async Task<ServiceDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Услуга", id);
        return entity.ToDto();
    }

    /// <inheritdoc />
    public async Task<ServiceDto> CreateAsync(ServiceEditDto dto, CancellationToken cancellationToken = default)
    {
        var created = await repository.AddAsync(dto.ToEntity(), cancellationToken);
        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<ServiceDto> UpdateAsync(int id, ServiceEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Услуга", id);

        entity.UpdateEntity(dto);
        await repository.UpdateAsync(entity, cancellationToken);
        return entity.ToDto();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (!await repository.DeleteAsync(id, cancellationToken))
        {
            throw new NotFoundException("Услуга", id);
        }
    }
}