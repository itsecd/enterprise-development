using VetClinic.Application.Mapping;
using VetClinic.Contracts.Dto.Owners;
using VetClinic.Contracts.Services;
using VetClinic.Domain.Repositories;

namespace VetClinic.Application.Services;

/// <summary>
/// Сервис владельцев, питомцы и их записи на прием удаляются вместе с владельцем каскадом в базе
/// </summary>
/// <param name="owners">Репозиторий владельцев</param>
public class OwnerService(
    IOwnerRepository owners) : IOwnerService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<OwnerDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var all = await owners.GetAllAsync(cancellationToken);
        return [.. all.Select(o => o.ToDto())];
    }

    /// <inheritdoc/>
    public async Task<OwnerDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await owners.GetByIdAsync(id, cancellationToken);
        return entity?.ToDto();
    }

    /// <inheritdoc/>
    public async Task<OwnerDto> CreateAsync(OwnerEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var created = await owners.CreateAsync(entity, cancellationToken);
        return created.ToDto();
    }

    /// <inheritdoc/>
    public async Task<OwnerDto?> UpdateAsync(int id, OwnerEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await owners.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Apply(dto);
        await owners.UpdateAsync(entity, cancellationToken);
        return entity.ToDto();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        await owners.DeleteAsync(id, cancellationToken);
}
