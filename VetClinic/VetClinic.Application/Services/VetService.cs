using VetClinic.Application.Mapping;
using VetClinic.Contracts.Dto.Vets;
using VetClinic.Contracts.Services;
using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;

namespace VetClinic.Application.Services;

/// <summary>
/// Сервис ветеринаров, записи на прием удаляются вместе с врачом каскадом в базе
/// </summary>
/// <param name="vets">Репозиторий ветеринаров</param>
/// <param name="specializations">Репозиторий специализаций</param>
public class VetService(
    IVetRepository vets,
    ISpecializationRepository specializations) : IVetService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<VetDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var all = await vets.GetAllAsync(cancellationToken);
        return [.. all.Select(v => v.ToDto())];
    }

    /// <inheritdoc/>
    public async Task<VetDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await vets.GetByIdAsync(id, cancellationToken);
        return entity?.ToDto();
    }

    /// <inheritdoc/>
    public async Task<VetDto> CreateAsync(VetEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        entity.Specialization = await GetSpecializationAsync(dto.SpecializationId, cancellationToken);
        var created = await vets.CreateAsync(entity, cancellationToken);
        return created.ToDto();
    }

    /// <inheritdoc/>
    public async Task<VetDto?> UpdateAsync(int id, VetEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await vets.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Specialization = await GetSpecializationAsync(dto.SpecializationId, cancellationToken);
        entity.Apply(dto);
        await vets.UpdateAsync(entity, cancellationToken);
        return entity.ToDto();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        await vets.DeleteAsync(id, cancellationToken);

    /// <summary>
    /// Найти специализацию, если она указана, или сообщить, что её нет
    /// </summary>
    /// <param name="specializationId">Идентификатор специализации или null</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <exception cref="KeyNotFoundException">Специализация указана, но не найдена</exception>
    private async Task<Specialization?> GetSpecializationAsync(int? specializationId, CancellationToken cancellationToken)
    {
        if (specializationId is null)
        {
            return null;
        }

        return await specializations.GetByIdAsync(specializationId.Value, cancellationToken)
            ?? throw new KeyNotFoundException($"Специализация с идентификатором {specializationId} не найдена");
    }
}
