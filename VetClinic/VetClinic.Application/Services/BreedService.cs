using VetClinic.Application.Exceptions;
using VetClinic.Application.Mapping;
using VetClinic.Contracts.Dto.Breeds;
using VetClinic.Contracts.Services;
using VetClinic.Domain.Repositories;

namespace VetClinic.Application.Services;

/// <summary>
/// Сервис пород, при удалении породы у питомцев она сбрасывается на стороне базы
/// </summary>
/// <param name="breeds">Репозиторий пород</param>
/// <param name="pets">Репозиторий питомцев</param>
public class BreedService(
    IBreedRepository breeds,
    IPetRepository pets) : IBreedService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<BreedDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var all = await breeds.GetAllAsync(cancellationToken);
        return [.. all.Select(b => b.ToDto())];
    }

    /// <inheritdoc/>
    public async Task<BreedDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await breeds.GetByIdAsync(id, cancellationToken);
        return entity?.ToDto();
    }

    /// <inheritdoc/>
    public async Task<BreedDto> CreateAsync(BreedEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var created = await breeds.CreateAsync(entity, cancellationToken);
        return created.ToDto();
    }

    /// <inheritdoc/>
    public async Task<BreedDto?> UpdateAsync(int id, BreedEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await breeds.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        if (entity.Species != dto.Species && await pets.AnyByBreedAsync(id, cancellationToken))
        {
            throw new ConflictException($"Вид породы «{entity.Name}» нельзя изменить: к ней привязаны питомцы");
        }

        entity.Apply(dto);
        await breeds.UpdateAsync(entity, cancellationToken);
        return entity.ToDto();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        await breeds.DeleteAsync(id, cancellationToken);
}
