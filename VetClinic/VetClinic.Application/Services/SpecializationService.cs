using VetClinic.Application.Mapping;
using VetClinic.Contracts.Dto.Specializations;
using VetClinic.Contracts.Services;
using VetClinic.Domain.Repositories;

namespace VetClinic.Application.Services;

/// <summary>
/// Сервис специализаций врачей, при удалении специализации у врачей она сбрасывается на стороне базы
/// </summary>
/// <param name="specializations">Репозиторий специализаций</param>
public class SpecializationService(
    ISpecializationRepository specializations) : ISpecializationService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<SpecializationDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var all = await specializations.GetAllAsync(cancellationToken);
        return [.. all.Select(s => s.ToDto())];
    }

    /// <inheritdoc/>
    public async Task<SpecializationDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await specializations.GetByIdAsync(id, cancellationToken);
        return entity?.ToDto();
    }

    /// <inheritdoc/>
    public async Task<SpecializationDto> CreateAsync(SpecializationEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        var created = await specializations.CreateAsync(entity, cancellationToken);
        return created.ToDto();
    }

    /// <inheritdoc/>
    public async Task<SpecializationDto?> UpdateAsync(int id, SpecializationEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await specializations.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Apply(dto);
        await specializations.SaveChangesAsync(cancellationToken);
        return entity.ToDto();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        await specializations.DeleteAsync(id, cancellationToken);
}
