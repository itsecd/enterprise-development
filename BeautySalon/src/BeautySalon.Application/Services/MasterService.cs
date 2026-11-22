using BeautySalon.Application.Exceptions;
using BeautySalon.Application.Mapping;
using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Application.Services;

/// <summary>
/// Сервис управления мастерами
/// </summary>
public class MasterService(IMasterRepository repository) : IMasterService
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<MasterDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await repository.GetAllAsync(cancellationToken);
        return [.. entities.Select(e => e.ToDto())];
    }

    /// <inheritdoc />
    public async Task<MasterDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Мастер", id);
        return entity.ToDto();
    }

    /// <inheritdoc />
    public async Task<MasterDto> CreateAsync(MasterEditDto dto, CancellationToken cancellationToken = default)
    {
        var created = await repository.AddAsync(dto.ToEntity(), cancellationToken);
        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<MasterDto> UpdateAsync(int id, MasterEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await repository.GetByIdAsync(id, cancellationToken)
            ?? throw new NotFoundException("Мастер", id);

        entity.UpdateEntity(dto);
        await repository.UpdateAsync(entity, cancellationToken);
        return entity.ToDto();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        if (!await repository.DeleteAsync(id, cancellationToken))
        {
            throw new NotFoundException("Мастер", id);
        }
    }
}